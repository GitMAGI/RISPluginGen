using Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer
{
    public partial class RISBLL
    {
        public IBLL.DTO.PazienteDTO GetPazienteById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PazienteDTO pazi = null;

            try
            {
                IDAL.VO.PazienteVO pazi_ = this.dal.GetPazienteById(id);
                pazi = PazienteMapper.PaziMapper(pazi_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(pazi), LibString.TypeName(pazi_), LibString.TypeName(pazi)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pazi;
        }
        public List<IBLL.DTO.PazienteDTO> GetPazienteBy5IdentityFields(string pazicogn, string pazinome, string pazisess, DateTime pazidata, string pazicofi)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.PazienteDTO> pazis = null;

            try
            {
                List<IDAL.VO.PazienteVO> pazis_ = this.dal.GetPazienteBy5IdentityFields(pazicogn, pazinome, pazisess, pazidata, pazicofi);
                pazis = PazienteMapper.PaziMapper(pazis_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(pazis), LibString.TypeName(pazis_), LibString.TypeName(pazis)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pazis;
        }
        public IBLL.DTO.PazienteDTO AddPaziente(IBLL.DTO.PazienteDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PazienteDTO toReturn = null;

            try
            {
                data.paziidid = null;
                IDAL.VO.PazienteVO data_ = PazienteMapper.PaziMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                IDAL.VO.PazienteVO stored = dal.NewPaziente(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = PazienteMapper.PaziMapper(stored);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(toReturn), LibString.TypeName(stored), LibString.TypeName(toReturn)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return toReturn;
        }
    }
}
