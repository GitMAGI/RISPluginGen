using System;
using System.Collections.Generic;
using System.Data;

namespace Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer.Mappers
{
    public class RichiestaRISMapper
    {
        private static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public static List<IDAL.VO.RichiestaRISVO> RichMapper(DataTable rows)
        {
            List<IDAL.VO.RichiestaRISVO> rich = null;
            if (rows != null)
            {
                rich = new List<IDAL.VO.RichiestaRISVO>();
                foreach (DataRow row in rows.Rows)
                {
                    rich.Add(RichMapper(row));
                }
            }
            return rich;
        }
        public static IDAL.VO.RichiestaRISVO RichMapper(DataRow row)
        {
            IDAL.VO.RichiestaRISVO rich = new IDAL.VO.RichiestaRISVO();

            rich.id = row["id"] != DBNull.Value ? (long)row["id"] : (long?)null;
            rich.richidid = row["richidid"] != DBNull.Value ? (string)row["richidid"] : null;
            rich.evendata = row["evendata"] != DBNull.Value ? (DateTime)row["evendata"] : (DateTime?)null;
            rich.prendata = row["prendata"] != DBNull.Value ? (DateTime)row["prendata"] : (DateTime?)null;
            rich.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            rich.hl7_msg = row["hl7_msg"] != DBNull.Value ? (string)row["hl7_msg"] : null;
            rich.quesmed = row["quesmed"] != DBNull.Value ? (string)row["quesmed"] : null;
            rich.urgente = row["urgente"] != DBNull.Value ? (int)row["urgente"] : (int?)null;
            rich.paziidunico = row["paziidunico"] != DBNull.Value ? (long)row["paziidunico"] : (long?)null;
            rich.paziid = row["paziid"] != DBNull.Value ? (string)row["paziid"] : null;
            rich.pazinome = row["pazinome"] != DBNull.Value ? (string)row["pazinome"] : null;
            rich.pazicogn = row["pazicogn"] != DBNull.Value ? (string)row["pazicogn"] : null;
            rich.pazidata = row["pazidata"] != DBNull.Value ? (DateTime)row["pazidata"] : (DateTime?)null;
            rich.pazicofi = row["pazicofi"] != DBNull.Value ? (string)row["pazicofi"] : null;
            rich.pazisess = row["pazisess"] != DBNull.Value ? (string)row["pazisess"] : null;
            rich.paziNaNa_cod = row["paziNaNa_cod"] != DBNull.Value ? (string)row["paziNaNa_cod"] : null;
            rich.paziPrNa_cod = row["paziPrNa_cod"] != DBNull.Value ? (string)row["paziPrNa_cod"] : null;
            rich.paziCoNa_cod = row["paziCoNa_cod"] != DBNull.Value ? (string)row["paziCoNa_cod"] : null;
            rich.paziNaNa_txt = row["paziNaNa_txt"] != DBNull.Value ? (string)row["paziNaNa_txt"] : null;
            rich.paziPrNa_txt = row["paziPrNa_txt"] != DBNull.Value ? (string)row["paziPrNa_txt"] : null;
            rich.paziCoNa_txt = row["paziCoNa_txt"] != DBNull.Value ? (string)row["paziCoNa_txt"] : null;
            rich.paziCoNa_cap = row["paziCoNa_cap"] != DBNull.Value ? (string)row["paziCoNa_cap"] : null;
            rich.paziNaRe_cod = row["paziNaRe_cod"] != DBNull.Value ? (string)row["paziNaRe_cod"] : null;
            rich.paziPrRe_cod = row["paziPrRe_cod"] != DBNull.Value ? (string)row["paziPrRe_cod"] : null;
            rich.paziCoRe_cod = row["paziCoRe_cod"] != DBNull.Value ? (string)row["paziCoRe_cod"] : null;
            rich.paziNaRe_txt = row["paziNaRe_txt"] != DBNull.Value ? (string)row["paziNaRe_txt"] : null;
            rich.paziPrRe_txt = row["paziPrRe_txt"] != DBNull.Value ? (string)row["paziPrRe_txt"] : null;
            rich.paziCoRe_txt = row["paziCoRe_txt"] != DBNull.Value ? (string)row["paziCoRe_txt"] : null;
            rich.paziCoRe_cap = row["paziCoRe_cap"] != DBNull.Value ? (string)row["paziCoRe_cap"] : null;
            rich.paziNaDo_cod = row["paziNaDo_cod"] != DBNull.Value ? (string)row["paziNaDo_cod"] : null;
            rich.paziPrDo_cod = row["paziPrDo_cod"] != DBNull.Value ? (string)row["paziPrDo_cod"] : null;
            rich.paziCoDo_cod = row["paziCoDo_cod"] != DBNull.Value ? (string)row["paziCoDo_cod"] : null;
            rich.paziNaDo_txt = row["paziNaDo_txt"] != DBNull.Value ? (string)row["paziNaDo_txt"] : null;
            rich.paziPrDo_txt = row["paziPrDo_txt"] != DBNull.Value ? (string)row["paziPrDo_txt"] : null;
            rich.paziCoDo_txt = row["paziCoDo_txt"] != DBNull.Value ? (string)row["paziCoDo_txt"] : null;
            rich.paziCoDo_cap = row["paziCoDo_cap"] != DBNull.Value ? (string)row["paziCoDo_cap"] : null;
            rich.paziNaRf_cod = row["paziNaRf_cod"] != DBNull.Value ? (string)row["paziNaRf_cod"] : null;
            rich.paziPrRf_cod = row["paziPrRf_cod"] != DBNull.Value ? (string)row["paziPrRf_cod"] : null;
            rich.paziCoRf_cod = row["paziCoRf_cod"] != DBNull.Value ? (string)row["paziCoRf_cod"] : null;
            rich.paziNaRf_txt = row["paziNaRf_txt"] != DBNull.Value ? (string)row["paziNaRf_txt"] : null;
            rich.paziPrRf_txt = row["paziPrRf_txt"] != DBNull.Value ? (string)row["paziPrRf_txt"] : null;
            rich.paziCoRf_txt = row["paziCoRf_txt"] != DBNull.Value ? (string)row["paziCoRf_txt"] : null;
            rich.paziCoRf_cap = row["paziCoRf_cap"] != DBNull.Value ? (string)row["paziCoRf_cap"] : null;
            rich.paziCoRe_via = row["paziCoRe_via"] != DBNull.Value ? (string)row["paziCoRe_via"] : null;
            rich.paziCoDo_via = row["paziCoDo_via"] != DBNull.Value ? (string)row["paziCoDo_via"] : null;
            rich.paziCoRf_via = row["paziCoRf_via"] != DBNull.Value ? (string)row["paziCoRf_via"] : null;
            rich.pazitele = row["pazitele"] != DBNull.Value ? (string)row["pazitele"] : null;
            rich.paziAsl_cod = row["paziAsl_cod"] != DBNull.Value ? (string)row["paziAsl_cod"] : null;
            rich.paziAsl_txt = row["paziAsl_txt"] != DBNull.Value ? (string)row["paziAsl_txt"] : null;
            rich.paziteam = row["paziteam"] != DBNull.Value ? (string)row["paziteam"] : null;
            rich.mediid = row["mediid"] != DBNull.Value ? (string)row["mediid"] : null;
            rich.medinome = row["medinome"] != DBNull.Value ? (string)row["medinome"] : null;
            rich.medicogn = row["medicogn"] != DBNull.Value ? (string)row["medicogn"] : null;
            rich.medicofi = row["medicofi"] != DBNull.Value ? (string)row["medicofi"] : null;
            rich.repaid = row["repaid"] != DBNull.Value ? (string)row["repaid"] : null;
            rich.repanome = row["repanome"] != DBNull.Value ? (string)row["repanome"] : null;
            rich.accedatetime = row["accedatetime"] != DBNull.Value ? (DateTime)row["accedatetime"] : (DateTime?)null;
            rich.tiporicovero = row["tiporicovero"] != DBNull.Value ? (string)row["tiporicovero"] : null;
            rich.episodioid = row["episodioid"] != DBNull.Value ? (long)row["episodioid"] : (long?)null;

            return rich; ;
        }
    }
}