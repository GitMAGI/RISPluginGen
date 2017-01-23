using System.Configuration;

namespace Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer
{
    public partial class RISDAL : IDAL.IRISDAL
    {
        public static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public string GRConnectionString = ConfigurationManager.ConnectionStrings["GR"].ConnectionString;

        public string RadioTabName = ConfigurationManager.AppSettings["tbn_radio"];        
        public string RichiestaRISTabName = ConfigurationManager.AppSettings["tbn_richiestaris"];
        public string PazienteTabName = ConfigurationManager.AppSettings["tbn_paziente"];
    }
}
