using System;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers
{
    public class RefertoMapper
    {
        private static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public static IBLL.DTO.RefertoDTO RefeMapper(IDAL.VO.RadioVO raw)
        {
            IBLL.DTO.RefertoDTO refe = null;
            try
            {
                if(raw == null)
                {
                    return refe;
                }
                refe = new IBLL.DTO.RefertoDTO();
                refe.radioass1 = raw.radioass1;
                refe.radioass2 = raw.radioass2;
                refe.radioass3 = raw.radioass3;
                refe.radioass4 = raw.radioass4;
                refe.radiolink = raw.radiolink;
                refe.radiolink2 = raw.radiolink2;
            }            
            catch (Exception ex)
            {
                log.Error(string.Format("Mapping Error!\n{0}", ex.Message));
                refe = null;
            }

            return refe;
        }        
    }
}
