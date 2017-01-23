using System;

namespace Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO
{
    public class RichiestaRISDTO
    {
        public long? id { get; set; }
        public string richidid { get; set; }
        public DateTime? evendata { get; set; }
        public DateTime? prendata { get; set; }
        public string hl7_stato { get; set; }
        public string hl7_msg { get; set; }
        public string quesmed { get; set; }
        public int? urgente { get; set; }
        public long? paziidunico { get; set; }
        public string paziid { get; set; }
        public string pazinome { get; set; }
        public string pazicogn { get; set; }
        public DateTime? pazidata { get; set; }
        public string pazicofi { get; set; }
        public string pazisess { get; set; }
        public string paziNaNa_cod { get; set; }
        public string paziPrNa_cod { get; set; }
        public string paziCoNa_cod { get; set; }
        public string paziNaNa_txt { get; set; }
        public string paziPrNa_txt { get; set; }
        public string paziCoNa_txt { get; set; }
        public string paziCoNa_cap { get; set; }
        public string paziNaRe_cod { get; set; }
        public string paziPrRe_cod { get; set; }
        public string paziCoRe_cod { get; set; }
        public string paziNaRe_txt { get; set; }
        public string paziPrRe_txt { get; set; }
        public string paziCoRe_txt { get; set; }
        public string paziCoRe_cap { get; set; }
        public string paziCoRe_via { get; set; }
        public string paziNaDo_cod { get; set; }
        public string paziPrDo_cod { get; set; }
        public string paziCoDo_cod { get; set; }
        public string paziNaDo_txt { get; set; }
        public string paziPrDo_txt { get; set; }
        public string paziCoDo_txt { get; set; }
        public string paziCoDo_cap { get; set; }
        public string paziCoDo_via { get; set; }
        public string paziNaRf_cod { get; set; }
        public string paziPrRf_cod { get; set; }
        public string paziCoRf_cod { get; set; }
        public string paziNaRf_txt { get; set; }
        public string paziPrRf_txt { get; set; }
        public string paziCoRf_txt { get; set; }
        public string paziCoRf_cap { get; set; }
        public string paziCoRf_via { get; set; }
        public string pazitele { get; set; }
        public string paziAsl_cod { get; set; }
        public string paziAsl_txt { get; set; }
        public string paziteam { get; set; }
        public string mediid { get; set; }
        public string medinome { get; set; }
        public string medicogn { get; set; }
        public string medicofi { get; set; }
        public string repaid { get; set; }
        public string repanome { get; set; }
        public DateTime? accedatetime { get; set; }
        public string tiporicovero { get; set; }
        public long? episodioid { get; set; }
    }
}
