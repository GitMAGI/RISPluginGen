using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer;
using Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer;
using Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO;

namespace TestBLL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting .... ");

            RISDAL dal = new RISDAL();
            RISBLL bll = new RISBLL(dal);

            RichiestaRISDTO rich = new RichiestaRISDTO();

            rich.episodioid = 100202;

            rich.paziid = "8475512";
            rich.paziidunico = 0;

            rich.pazicofi = "TSSTTS83C15R345J";
            rich.pazicogn = "TestCognome";
            rich.pazinome = "TestNome";
            rich.pazidata = Convert.ToDateTime("1983-03-15");
            rich.pazisess = "M";

            rich.richidid = "e9re0vkv904gm9bm";

            rich.accedatetime = Convert.ToDateTime("2017-01-09 11:25:00.000");
            rich.evendata = DateTime.Now;
            rich.prendata = Convert.ToDateTime("2017-02-01 12:05:00.000");
            rich.quesmed = "Interrogarsi per vivere";
            rich.repaid = "43";
            rich.repanome = "Medicina Generale";

            rich.tiporicovero = "Incognito";
            rich.urgente = 1;

            rich.medicofi = "TSTSTS56E12L498F";
            rich.medicogn = "PersTestCognomne";
            rich.mediid = "4552";
            rich.medinome = "PersTestNome";

            rich.paziteam = "10121200054548465";
            rich.pazitele = null;

            rich.paziAsl_cod = null;
            rich.paziAsl_txt = null;
            rich.paziCoDo_cap = null;
            rich.paziCoDo_cod = null;
            rich.paziCoDo_txt = null;
            rich.paziCoDo_via = null;
            rich.paziCoNa_cap = null;
            rich.paziCoNa_cod = null;
            rich.paziCoNa_txt = null;
            rich.paziCoRe_cap = null;
            rich.paziCoRe_cod = null;
            rich.paziCoRe_txt = null;
            rich.paziCoRe_via = null;
            rich.paziCoRf_cap = null;
            rich.paziCoRf_cod = null;
            rich.paziCoRf_txt = null;
            rich.paziCoRf_via = null;
            rich.paziNaDo_cod = null;
            rich.paziNaDo_txt = null;
            rich.paziNaNa_cod = null;
            rich.paziNaNa_txt = null;
            rich.paziNaRe_cod = null;
            rich.paziNaRe_txt = null;
            rich.paziNaRf_cod = null;
            rich.paziNaRf_txt = null;
            rich.paziPrDo_cod = null;
            rich.paziPrDo_txt = null;
            rich.paziPrNa_cod = null;
            rich.paziPrNa_txt = null;
            rich.paziPrRe_cod = null;
            rich.paziPrRe_txt = null;
            rich.paziPrRf_cod = null;
            rich.paziPrRf_txt = null;

            List<RadioDTO> radios = new List<RadioDTO>()
            {
                new RadioDTO()
                {
                    radiorich = rich.richidid,
                    radiotipo = 1505,
                    radiodesc = "Rx torace",
                },
                new RadioDTO()
                {
                    radiorich = rich.richidid,
                    radiotipo = 1560,
                    radiodesc = "Rx ginocchio sn",
                }
            };

            string err = null;

            bool res = bll.StoreNewRequest(rich, radios, ref err);

            Console.WriteLine("Press a Key to Complete!");
            Console.ReadKey();
            Console.WriteLine("Completed!");
        }
    }
}
