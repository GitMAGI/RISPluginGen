using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleLoggers
{
    class Program
    {
        public static readonly log4net.ILog logL =
            log4net.LogManager.GetLogger("LIS");

        public static readonly log4net.ILog logR =
            log4net.LogManager.GetLogger("RIS");

        static void Main(string[] args)
        {
            Console.WriteLine("Staring ... ");

            string msg = "Scrivo il Log a pompa!";

            logL.Info(msg);
            logL.Error(msg);
            logL.Warn(msg);
            
            logR.Info(msg);
            logR.Error(msg);
            logR.Warn(msg);

            Console.WriteLine("Completed!");
        }
    }
}
