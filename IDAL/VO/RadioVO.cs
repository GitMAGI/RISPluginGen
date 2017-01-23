using System;

namespace Seminabit.Sanita.OrderEntry.RIS.IDAL.VO
{
    public class RadioVO
    {
        public int? radioidid { get; set; }
        public int? radiopres { get; set; }
        public string radiodesc { get; set; }
        public int? radiotipo { get; set; }
        public DateTime? radiodass { get; set; }
        public string es_dett_key { get; set; }
        public DateTime? es_data { get; set; }
        public string es_stato { get; set; }
        public string es_ref { get; set; }
        public DateTime? data_verifica { get; set; }
        public string esito_verifica { get; set; }
        public DateTime? es_data_referto { get; set; }
        public DateTime? es_data_validazione_referto { get; set; }
        public string hl7_stato { get; set; }
        public string hl7_msg { get; set; }
        public string radioass2 { get; set; }
        public string radioass3 { get; set; }
        public string radioass1 { get; set; }
        public string radiolink { get; set; }
        public string radioass4 { get; set; }
        public string radiolink2 { get; set; }
        public string radiorefe { get; set; }
        public string radiorefestat { get; set; }
        public string radiorich { get; set; }
    }
}
