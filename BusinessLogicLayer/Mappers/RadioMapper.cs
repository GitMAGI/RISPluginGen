using AutoMapper;
using System;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers
{
    public class RadioMapper
    {
        private static readonly log4net.ILog log =
            //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.LogManager.GetLogger("RIS");

        public static IBLL.DTO.RadioDTO RadMapper(IDAL.VO.RadioVO raw)
        {
            IBLL.DTO.RadioDTO esam = null;
                       
            if (raw != null)
            {
                esam = new IBLL.DTO.RadioDTO();
                try
                {
                    esam.data_verifica = raw.data_verifica;
                    esam.esito_verifica = raw.esito_verifica;
                    esam.es_data = raw.es_data;
                    esam.es_data_referto = raw.es_data_referto;
                    esam.es_data_validazione_referto = raw.es_data_validazione_referto;
                    esam.es_dett_key = raw.es_dett_key;
                    esam.es_ref = raw.es_ref;
                    esam.hl7_msg = raw.hl7_msg;
                    esam.hl7_stato = raw.hl7_stato;
                    esam.radiodesc = raw.radiodesc;
                    esam.radioidid = raw.radioidid;
                    esam.radiopres = raw.radiopres;
                    esam.radiorefe = raw.radiorefe;
                    esam.radiorefestat = raw.radiorefestat;
                    esam.radiorich = raw.radiorich;
                    esam.radiotipo = raw.radiotipo;
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("Mapping Error!\n{0}", ex.Message));
                }
            }

            return esam;
        }
        public static List<IBLL.DTO.RadioDTO> RadMapper(List<IDAL.VO.RadioVO> raws)
        {
            List<IBLL.DTO.RadioDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.RadioDTO>();
                foreach (IDAL.VO.RadioVO raw in raws)
                {
                    res.Add(RadMapper(raw));
                }
            }

            return res;
        }
        public static IDAL.VO.RadioVO RadMapper(IBLL.DTO.RadioDTO data)
        {
            IDAL.VO.RadioVO esam = null;

            if (data != null)
            {
                esam = new IDAL.VO.RadioVO();
                try
                {
                    esam.data_verifica = data.data_verifica;
                    esam.esito_verifica = data.esito_verifica;
                    esam.es_data = data.es_data;
                    esam.es_data_referto = data.es_data_referto;
                    esam.es_data_validazione_referto = data.es_data_validazione_referto;
                    esam.es_dett_key = data.es_dett_key;
                    esam.es_ref = data.es_ref;
                    esam.hl7_msg = data.hl7_msg;
                    esam.hl7_stato = data.hl7_stato;
                    esam.radiodesc = data.radiodesc;
                    esam.radioidid = data.radioidid;
                    esam.radiopres = data.radiopres;
                    esam.radiorefe = data.radiorefe;
                    esam.radiorefestat = data.radiorefestat;
                    esam.radiorich = data.radiorich;
                    esam.radiotipo = data.radiotipo;
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("Mapping Error!\n{0}", ex.Message));
                }
            }

            return esam;
        }
        public static List<IDAL.VO.RadioVO> RadMapper(List<IBLL.DTO.RadioDTO> dtos)
        {
            List<IDAL.VO.RadioVO> res = null;

            if (dtos != null)
            {
                res = new List<IDAL.VO.RadioVO>();
                foreach (IBLL.DTO.RadioDTO raw in dtos)
                {
                    res.Add(RadMapper(raw));
                }
            }

            return res;
        }

    }
}
