namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer
{
    public partial class RISBLL : IBLL.IRISBLL
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDAL.IRISDAL dal;

        public RISBLL(IDAL.IRISDAL IDAL)
        {
            this.dal = IDAL;
        }

    }
}
