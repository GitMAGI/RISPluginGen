using System;
using System.Collections.Generic;
using Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO;
using System.Diagnostics;
using GeneralPurposeLib;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer
{
    public partial class RISBLL
    {
        public MirthResponseDTO ACKParser(string raw)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = new MirthResponseDTO();

            log.Info(string.Format("HL7 Message To Process:\n{0}", raw));

            log.Info(string.Format("HL7 Message Processing ... "));

            try
            {   
                log.Info(string.Format("MSA Recovering ..."));
                // 1. Get MSA Segment
                string msa = null;
                try
                {
                    msa = LibString.GetAllValuesSegments(raw, "MSA")[0];
                }
                catch(Exception)
                {
                    string msg = "Unable to find MSA segment!";
                    log.Info(msg);
                    log.Error(msg);
                    data.ACKDesc = "Mirth returned a Bad Response!";
                    data.ACKCode = "--error--";
                    data.Errored = true;
                    data.Accepted = false;
                    data.Refused = false;
                    throw new Exception(msg);
                }
                string[] msaobj = msa.Split('|');
                data.ACKCode = msaobj[1];
                data.MsgID = msaobj[2];
                data.ACKDesc = null;
                /*
                if (msaobj.Length > 3)
                {
                    data.ACKDesc = msaobj[2];
                    try
                    {
                        string[] tmp = msaobj[2].Split('\n');
                        data.ACKDesc = tmp[0];
                    }
                    catch (Exception)
                    {
                        log.Warn(string.Format("Unable to extract AckDesc!"));
                    }
                }
                */
                switch (data.ACKCode)
                {
                    case "AA":
                        data.Errored = false;
                        data.Accepted = true;
                        data.Refused = false;
                        data.ACKDesc = "Server Responded with 'AA'! Request successfully Accepted!";
                        break;
                    case "AE":
                        data.Errored = true;
                        data.Accepted = false;
                        data.Refused = false;
                        data.ACKDesc = "Server Responded with 'AE'! Errors in the Request!";
                        break;
                    case "AR":
                        data.Errored = false;
                        data.Accepted = false;
                        data.Refused = true;
                        data.ACKDesc = "Server Responded with 'AR'! Request has been Refused!";
                        break;
                }
                log.Info(string.Format("MSA Recovered"));
                
                log.Info(string.Format("HL7 Message processing Complete! A DTO object has been built!"));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }
        public string SendMirthRequest(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string data = null;

            try
            {
                // 0. Check if rich exists and radios do
                RichiestaRISDTO rich = GetRichiestaRISByIdExt(richidid);
                if (rich == null)
                {                    
                    string msg = string.Format("An Error occured! No RICH idext {0} found into the DB. Operation Aborted!", richidid);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                string id = rich.id.Value.ToString();
                log.Info("External Request ID " + richidid + " - Internal Request ID " + id);

                List<RadioDTO> radios = GetRadiosByRichiestaExt(richidid);

                if (radios == null || (radios != null && radios.Count == 0))
                {
                    string msg = string.Format("An Error occured! No RADIO related to idExt {0} found into the DB. Operation Aborted!", richidid);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
            
                // 1. Call DAL.SendMirthREquest()
                data = this.dal.SendRISRequest(richidid);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }
        
        public int ChangeHL7StatusAndMessageAll(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int res = 0;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato' -> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg' -> " + hl7_msg;
            log.Info(string.Format(msg_));
            log.Info(string.Format("Updating RICH ..."));

            RichiestaRISDTO got = GetRichiestaRISByIdExt(richidid);

            if (got == null)
            {
                log.Info(string.Format("An Error occurred. Rich bot found! IDExt: {0}", richidid));
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
                return 0;
            }

            //string id = got.id.Value.ToString();

            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            RichiestaRISDTO updt = UpdateRichiestaRIS(got);

            int richres = 0;
            if (updt != null)
                richres++;
            else
                log.Info(string.Format("An Error occurred. Record not updated! ID: {0}", got.id));
            res = richres;

            log.Info(string.Format("Updated {0}/{1} record!", richres, 1));

            log.Info(string.Format("Updating RADIO ..."));
            List<RadioDTO> gots = GetRadiosByRichiestaExt(richidid);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int Radiosres = 0;
            foreach (RadioDTO got_ in gots)
            {
                RadioDTO updt_ = UpdateRadio(got_);
                if (updt_ != null)
                    Radiosres++;
                else
                    log.Info(string.Format("An Error occurred. Record not updated! RADIOIDID: {0}", got_.radioidid));
            }
            res += Radiosres;
            log.Info(string.Format("Updated {0}/{1} record!", Radiosres, gots.Count));

            log.Info(string.Format("Updated {0} record overall!", res));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
        public List<RadioDTO> ChangeHL7StatusAndMessageRadios(List<string> radioidids, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            List<RadioDTO> updateds = null;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato' -> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg' -> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating RADIO ..."));
            List<RadioDTO> gots = GetRadiosByIds(radioidids);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (RadioDTO got_ in gots)
            {
                RadioDTO updt_ = UpdateRadio(got_);
                if (updt_ != null)
                {
                    if(updateds==null)
                        updateds = new List<RadioDTO>();
                    updateds.Add(updt_);
                    analsres++;
                }                    
                else
                    log.Info(string.Format("An Error occurred. Record not updated! RADIOIDID: {0}", got_.radioidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", analsres, gots.Count));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updateds;
        }
        public RichiestaRISDTO ChangeHL7StatusAndMessageRichiestaRIS(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            RichiestaRISDTO updated = new RichiestaRISDTO();

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato' -> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg' -> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating RICH ..."));

            RichiestaRISDTO got = GetRichiestaRISByIdExt(richidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            updated = UpdateRichiestaRIS(got);

            int res = 0;
            if (updated != null)
            {
                res++;                
            }
            else
            {
                log.Info(string.Format("An Error occurred. Record not updated! ID: {0}", got.id));
            }
            log.Info(string.Format("Updated {0}/{1} record!", res, 1));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updated;
        }

        public bool ValidateRich(RichiestaRISDTO rich, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;            

            if (errorString == null)
                errorString = "";

            /*
            string richid = esam.esamidid.ToString();
            if (esam.esamidid == null)
            {
                string msg = "ESAMIDID is Null!";
                validate = false;
                if(errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }
            if (esam.esameven == null)
            {
                string msg = "ESAMEVEN is Null!";
                validate = false;
                if (errorString != "")                    
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            if(esam.esamtipo == null)
            {
                string msg = "ESAMTIPO is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            if(esam.esampren == null)
            {
                string msg = "ESAMPREN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            */
            
            if (rich.episodioid == null)
            {
                string msg = "EPISODIOID is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "RichiestaLIS error: " + msg;
                else
                    errorString += "RichiestaLIS error: " + msg;
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
        public bool ValidateRadios(List<RadioDTO> radios, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";
            
            int count = 0;
            foreach (RadioDTO radio in radios)
            {
                count++;
                /*
                string analid = radio.analidid.ToString();                
                if (radio.analidid == null)
                {
                    string msg = "";
                    validate = false;                    
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                */
                if (radio.radiorich == null)
                {
                    string msg = "RADIORICH is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
                if (radio.radiotipo == null)
                {
                    string msg = "RADIOTIPO is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
                if (radio.radiodesc == null)
                {
                    string msg = "RADIODESC is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }

        public bool StoreNewRequest(RichiestaRISDTO rich, List<RadioDTO> radios, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string hl7_stato = IBLL.HL7StatesRichiestaRIS.Idle;
            bool stored = true;

            string res = null;
            string resExt = null;

            RichiestaRISDTO richInserted = null;
            List<RadioDTO> radiosInserted = null;

            if (errorString == null)
                errorString = "";

            try
            {
                // Validation of Rich!!!!
                if (!this.ValidateRich(rich, ref errorString))
                {
                    string msg = "Validation of Rich Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                // Unique ID of Paziente Checking and Inserting
                log.Info(string.Format("PAZI's existence checking ..."));
                string pLName = rich.pazinome != null ? rich.pazinome.Trim() : null;
                string pFName = rich.pazicogn != null ? rich.pazicogn.Trim() : null;
                string pSex = rich.pazisess != null ? rich.pazisess.Trim() : null;
                string pBDate = rich.pazidata != null && rich.pazidata.HasValue ? rich.pazidata.Value.ToShortDateString() : null;
                string pCofi = rich.pazicofi != null ? rich.pazicofi.Trim() : null;
                log.Info(string.Format("Nome: {0} - Cognome: {1} - Sesso: {2} - Data di Nascita: {3} - Codice Fiscale: {4}", pLName, pFName, pSex, pBDate, pCofi));
                List<PazienteDTO> pazis = this.GetPazienteBy5IdentityFields(pFName, pLName, pSex, rich.pazidata.Value, pCofi);
                int paziInsertedRows = 0;
                int paziunico = 0;
                if (pazis != null && pazis.Count > 0)
                {
                    log.Info(string.Format("PAZI: {0} {1} exists! {2} related record(s) found!", pFName, pLName, pazis.Count));
                    foreach (PazienteDTO pazi in pazis)
                    {
                        log.Info(string.Format("PAZIIDID: {0}", pazi.paziidid));
                        if (pazi.paziidid.Value > paziunico)
                            paziunico = pazi.paziidid.Value;
                    }
                    log.Info(string.Format("Paziunico chose is: {0}!", paziunico));
                }
                else
                {
                    log.Info(string.Format("PAZI not found! Inserting of the requested PAZI. Processing ..."));
                    PazienteDTO paziToInsert = new PazienteDTO();
                    paziToInsert.pazicogn = rich.pazicogn;
                    paziToInsert.pazinome = rich.pazinome;
                    paziToInsert.pazisess = rich.pazisess;
                    paziToInsert.pazidata = rich.pazidata;
                    paziToInsert.pazicofi = rich.pazicofi;
                    paziToInsert.pazitele = rich.pazitele;
                    paziToInsert.paziteam = rich.paziteam;
                    paziToInsert.paziviaa = rich.paziCoRe_via;
                    paziToInsert.paziprov = rich.paziPrRe_txt;
                    paziToInsert.pazicomu = rich.paziCoRe_txt;
                    paziToInsert.pazictnz = rich.paziNaNa_cod;
                    paziToInsert.paziasll = rich.paziAsl_cod;
                    paziToInsert.nominativo = rich.pazicogn + ", " + rich.pazinome;
                    PazienteDTO paziInserted = this.AddPaziente(paziToInsert);
                    if (paziInserted == null)
                        throw new Exception("Error during PAZI writing into the DB.");
                    paziunico = paziInserted.paziidid.Value;
                    log.Info(string.Format("Paziunico got is: {0}!", paziunico));
                    log.Info(string.Format("PAZI Inserted. Got {0} ID!", paziunico));
                    paziToInsert = null;
                    paziInserted = null;
                }

                // Rich Inserting
                rich.hl7_stato = hl7_stato;
                rich.paziidunico = paziunico;
                log.Info(string.Format("RICH Inserting ..."));
                richInserted = this.AddRichiestaRIS(rich);
                if (richInserted == null)
                    throw new Exception("Error during RICH writing into the DB.");
                log.Info(string.Format("RICH Inserted. Got {0} as ID and {1} as IDExt!", richInserted.id, richInserted.richidid));

                res = richInserted.id.ToString();
                resExt = richInserted.richidid;

                //radios.ForEach(p => { p.analesam = int.Parse(res); p.hl7_stato = hl7_stato; });
                radios.ForEach(p => { p.radiorich = resExt; p.hl7_stato = hl7_stato; });

                // Validation of Radios!!!!
                if (!this.ValidateRadios(radios, ref errorString))
                {
                    string msg = "Validation of Radios Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                log.Info(string.Format("Inserting of {0} RADIO requested. Processing ...", radios.Count));
                radiosInserted = this.AddRadios(radios);
                if ((radiosInserted == null) || (radiosInserted != null && radiosInserted.Count != radios.Count))
                    throw new Exception("Error during RADIOs writing into the DB.");
                log.Info(string.Format("Inserted {0} RADIO successfully!", radiosInserted.Count));

                log.Info(string.Format("Inserted {0} records successfully!", radiosInserted.Count + 1 + paziInsertedRows));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);

                if (errorString == "")
                    errorString = msg + "\r\n" + ex.Message;
                else
                    errorString += "\r\n" + msg + "\r\n" + ex.Message;

                int richRB = 0;
                int radiosRB = 0;

                log.Info(string.Format("Rolling Back of the Insertings due an error occured ..."));
                // Rolling Back
                if (res != null)
                {
                    richRB = this.DeleteRichiestaRISById(res);
                    log.Info(string.Format("Rolled Back {0} RICH record. ID was {1} and RICHIDID was {2}!", richRB, res, resExt));
                    radiosRB = this.DeleteRadioByIdRichiestaExt(resExt);
                    log.Info(string.Format("Rolled Back {0} ANAL records. ANALRICH was {1}!", radiosRB, resExt));
                }
                log.Info(string.Format("Rolled Back {0} records of {1} requested!", richRB + radiosRB, radios.Count + 1));
                stored = false;
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            if (errorString == "")
                errorString = null;

            return stored;
        }
        public MirthResponseDTO SubmitNewRequest(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            if (errorString == null)
                errorString = "";

            try
            {                
                // 1. Check if Rich and RADIO exist
                RichiestaRISDTO chkRich = this.GetRichiestaRISByIdExt(richid);
                if (chkRich == null)
                {
                    string msg = "Error! No Rich record found referring to IDExt " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                List<RadioDTO> chkRadios = this.GetRadiosByRichiestaExt(richid);
                if (chkRadios == null || (chkRadios != null && chkRadios.Count == 0))
                {
                    string msg = "Error! No Anal records found referring to RadioRich " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 2. Settare Stato a "SEDNING"
                int res = this.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Sending, "");

                // 3. Invio a Mirth
                string hl7orl = this.SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 3.e1 Cambiare stato in errato
                    int err = this.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Errored, msg);
                    // 3.e2 Restituire null
                    return null;
                }
                // 3.1 Settare a SENT
                int snt = this.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Sent, "");

                // 4. Estrarre i dati dalla risposta di Mirth                
                log.Info("Mirth Data Response Extraction ...");
                data = this.ACKParser(hl7orl);
                if (data == null)
                {
                    string emsg = "Mirth Data Response Extraction failed!";
                    if (errorString == "")
                        errorString = emsg;
                    else
                        errorString += "\n\r" + emsg;
                    log.Info(emsg);
                    log.Error(emsg);

                }
                else
                    log.Info("Mirth Data Response Successfully extracted!");

                // 5. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaRIS.Sent;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaRIS.Errored;

                int snt2 = this.ChangeHL7StatusAndMessageAll(richid, status, data.ACKDesc);                
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            // 7. Restituire il DTO
            return data;
        }

        public bool CheckIfCancelingIsAllowed(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool res = true;

            if (errorString == null)
                errorString = "";

            RichiestaRISDTO rich = this.GetRichiestaRISByIdExt(richid);           

            if (rich == null)
            {
                if (errorString == "")
                    errorString = null;

                string msg = string.Format("Error! No Rich found with IDExt: {0}", richid);

                log.Info(string.Format(msg));
                log.Error(string.Format(msg));

                if (errorString != "")
                    errorString += "\r\n" + msg;
                else
                    errorString += msg;

                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

                return false;
            }

            string id = rich.id.Value.ToString();
            log.Info("External Request ID " + richid + " - Internal Request ID " + id);
            
            List<RadioDTO> radios = this.GetRadiosByRichiestaExt(id);
            foreach (RadioDTO radio in radios)
            {
                string report = string.Format("Radio {0} già refertato! Impossibile Cancellare!", radio.radioidid);
                res = false;
                if (errorString != "")
                    errorString += "\r\n" + report;
                else
                    errorString += report;
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
    }
}
