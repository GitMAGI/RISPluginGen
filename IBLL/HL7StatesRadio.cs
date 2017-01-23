﻿namespace Seminabit.Sanita.OrderEntry.RIS.IBLL
{
    public sealed class HL7StatesRadio
    {
        public static readonly string Idle = "IDLE";
        public static readonly string Sending = "SENDING";
        public static readonly string Sent = "SENT";
        public static readonly string Accepted = "ACCEPTED";
        public static readonly string Executed = "EXECUTED";
        public static readonly string Deleting = "DELETING";
        public static readonly string Deleted = "DELETED";
        public static readonly string Errored = "ERRORED";
    }
}
