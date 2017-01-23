using AutoMapper;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers
{
    public class PazienteMapper
    {
        private static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public static IBLL.DTO.PazienteDTO PaziMapper(IDAL.VO.PazienteVO raw)
        {
            IBLL.DTO.PazienteDTO pazi = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.PazienteVO, IBLL.DTO.PazienteDTO>());
                Mapper.AssertConfigurationIsValid();
                pazi = Mapper.Map<IBLL.DTO.PazienteDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return pazi;
        }
        public static List<IBLL.DTO.PazienteDTO> PaziMapper(List<IDAL.VO.PazienteVO> raws)
        {
            List<IBLL.DTO.PazienteDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.PazienteDTO>();
                foreach (IDAL.VO.PazienteVO raw in raws)
                {
                    res.Add(PaziMapper(raw));
                }
            }

            return res;
        }
        public static IDAL.VO.PazienteVO PaziMapper(IBLL.DTO.PazienteDTO data)
        {
            IDAL.VO.PazienteVO pazi = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.PazienteDTO, IDAL.VO.PazienteVO>());
                Mapper.AssertConfigurationIsValid();
                pazi = Mapper.Map<IDAL.VO.PazienteVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return pazi;
        }

    }
}
