using GeneralPurposeLib;
using Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Seminabit.Sanita.OrderEntry.RIS.Plugin
{
    public class RIS : IPlugin.IRIS
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataAccessLayer.RISDAL dal;
        private BusinessLogicLayer.RISBLL bll;

        public RIS()
        {
            dal = new DataAccessLayer.RISDAL();
            bll = new BusinessLogicLayer.RISBLL(dal);
        }
                
        public MirthResponseDTO NewRequest(RichiestaRISDTO rich, List<RadioDTO> radios,  ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            if (errorString == null)
                errorString = "";

            try
            {
                if(!bll.StoreNewRequest(rich, radios, ref errorString))
                {
                    throw new Exception(errorString);
                }
                data = bll.SubmitNewRequest(rich.richidid, ref errorString);
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
                        
            return data;
        }
                
        public List<RadioDTO> GetRadiosByRichIdExt(string richid_)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RadioDTO> anals = null;

            RichiestaRISDTO rich = bll.GetRichiestaRISByIdExt(richid_);
            if (rich != null)
            {
                string richid = rich.id.ToString();
                log.Info("External Request ID " + richid_ + " - Internal Request ID " + richid);
                anals = bll.GetRadiosByRichiestaExt(richid);                
            }
            else
            {
                string msg = string.Format("No Rich with ID: {0} found. The operation will be aborted!", richid_);
                log.Info(msg);
            }            

            rich = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anals;
        }        
        public List<RefertoDTO> GetReportByRichIdExt(string richid_)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RefertoDTO> refes = null;

            List<RadioDTO> radios = bll.GetRadiosByRichiestaExt(richid_);
            if (radios != null)
            {
                foreach (RadioDTO radio in radios)
                {
                    RefertoDTO refe = null;                                        
                    string radioid = radio.radioidid.ToString();
                    refe = bll.GetRefertoByRadioId(radioid);
                    log.Info("Radio ID: " + radioid);
                    if (refe != null)
                        refes.Add(refe);
                }             
            }
            else
            {
                string msg = string.Format("No RADIO with RADIORICH: {0} found. The operation will be aborted!", richid_);
                log.Info(msg);
            }
            
            radios = null;            

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return refes;
        }
        
        public RichiestaRISDTO GetRichiestaByIdExt(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RichiestaRISDTO rich = null;

            rich = bll.GetRichiestaRISByIdExt(richid);
            if (rich == null)
            {
                string msg = string.Format("No Rich with ID: {0} found. The operation will be aborted!", richid);
                log.Info(msg);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public List<RichiestaRISDTO> GetRichiesteByEpisodio(string episid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RichiestaRISDTO> richs = null;

            richs = bll.GetRichiesteByEpisodio(episid);
            if (richs == null)
            {
                string msg = string.Format("No Rich with EpisodioID: {0} found. The operation will be aborted!", episid);
                log.Info(msg);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return richs;
        }

        public MirthResponseDTO CancelRequest(string richid_, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            try
            {                
                // 1. Check if Canceling is allowed
                if (bll.CheckIfCancelingIsAllowed(richid_, ref errorString))
                {
                    string msg = string.Format("Canceling of the request with id {0} is denied! errorString: {1}", richid_, errorString);
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 2. Check if RICH and RADIOL exist
                RichiestaRISDTO chkRich = bll.GetRichiestaRISByIdExt(richid_);
                List<RadioDTO> chkRadios = bll.GetRadiosByRichiestaExt(richid_);
                if (chkRich == null || chkRadios == null || (chkRadios != null && chkRadios.Count == 0))
                {
                    string msg = "Error! No Rich or Radio records found referring to RichID (IDExt) " + richid_ + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 3. Settare Stato a "DELETNG"
                int res = bll.ChangeHL7StatusAndMessageAll(richid_, IBLL.HL7StatesRichiestaRIS.Deleting);

                // 4. Invio a Mirth
                string hl7orl = bll.SendMirthRequest(richid_);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 4.e1 Cambiare stato in errato
                    int err = bll.ChangeHL7StatusAndMessageAll(richid_, IBLL.HL7StatesRichiestaRIS.Errored, msg);
                    // 4.e2 Restituire null
                    return null;
                }

                // 5. Estrarre i dati dalla risposta di Mirth
                data = bll.ACKParser(hl7orl);

                // 6. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaRIS.Deleted;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaRIS.Errored;
                RichiestaRISDTO RichUpdt = bll.ChangeHL7StatusAndMessageRichiestaRIS(richid_, status, data.ACKDesc);                
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return data;
        }
        public bool CheckIfCancelingIsAllowed(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool res = bll.CheckIfCancelingIsAllowed(richid, ref errorString);            

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
    }
}
