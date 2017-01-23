using System;
using System.Collections.Generic;
using System.Data;

namespace Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer.Mappers
{
    public class RadioMapper
    {
        private static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public static IDAL.VO.RadioVO RadMapper(DataRow row)
        {
            IDAL.VO.RadioVO radio = new IDAL.VO.RadioVO();

            radio.radioidid = row["radioidid"] != DBNull.Value ? (int)row["radioidid"] : (int?)null;
            radio.radiopres = row["radiopres"] != DBNull.Value ? (int)row["radiopres"] : (int?)null;
            radio.radiodesc = row["radiodesc"] != DBNull.Value ? (string)row["radiodesc"] : null;
            radio.radiotipo = row["radiotipo"] != DBNull.Value ? (int)row["radiotipo"] : (int?)null;
            radio.radiodass = row["radiodass"] != DBNull.Value ? (DateTime)row["radiodass"] : (DateTime?)null;
            radio.es_dett_key = row["es_dett_key"] != DBNull.Value ? (string)row["es_dett_key"] : null;
            radio.es_data = row["es_data"] != DBNull.Value ? (DateTime)row["es_data"] : (DateTime?)null;
            radio.es_stato = row["es_stato"] != DBNull.Value ? (string)row["es_stato"] : null;
            radio.es_ref = row["es_ref"] != DBNull.Value ? (string)row["es_ref"] : null;
            radio.data_verifica = row["data_verifica"] != DBNull.Value ? (DateTime)row["data_verifica"] : (DateTime?)null;
            radio.esito_verifica = row["esito_verifica"] != DBNull.Value ? (string)row["esito_verifica"] : null;
            radio.es_data_referto = row["es_data_referto"] != DBNull.Value ? (DateTime)row["es_data_referto"] : (DateTime?)null;
            radio.es_data_validazione_referto = row["es_data_validazione_referto"] != DBNull.Value ? (DateTime)row["es_data_validazione_referto"] : (DateTime?)null;
            radio.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            radio.hl7_msg = row["hl7_msg"] != DBNull.Value ? (string)row["hl7_msg"] : null;
            radio.radioass2 = row["radioass2"] != DBNull.Value ? (string)row["radioass2"] : null;
            radio.radioass3 = row["radioass3"] != DBNull.Value ? (string)row["radioass3"] : null;
            radio.radioass1 = row["radioass1"] != DBNull.Value ? (string)row["radioass1"] : null;
            radio.radiolink = row["radiolink"] != DBNull.Value ? (string)row["radiolink"] : null;
            radio.radioass4 = row["radioass4"] != DBNull.Value ? (string)row["radioass4"] : null;
            radio.radiolink2 = row["radiolink2"] != DBNull.Value ? (string)row["radiolink2"] : null;
            radio.radiorefe = row["radiorefe"] != DBNull.Value ? (string)row["radiorefe"] : null;
            radio.radiorefestat = row["radiorefestat"] != DBNull.Value ? (string)row["radiorefestat"] : null;
            radio.radiorich = row["radiorich"] != DBNull.Value ? (string)row["radiorich"] : null;

            return radio;
        }
        public static List<IDAL.VO.RadioVO> RadMapper(DataTable rows)
        {
            List<IDAL.VO.RadioVO> data = new List<IDAL.VO.RadioVO>();

            if (rows != null)
            {
                if(rows.Rows.Count > 0)
                {
                    foreach(DataRow row in rows.Rows)
                    {
                        IDAL.VO.RadioVO tmp = RadMapper(row);
                        data.Add(tmp);
                    }
                }
            }

            return data;
        }
    }
}
