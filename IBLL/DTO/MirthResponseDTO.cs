using System;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO
{
    public class MirthResponseDTO
    {
        public bool Errored { get; set; }
        public bool Accepted { get; set; }
        public bool Refused { get; set; }
        public string ACKCode { get; set; }
        public string ACKDesc { get; set; }
        public string MsgID { get; set; }
        public string ERRMsg { get; set; }
    }
}
