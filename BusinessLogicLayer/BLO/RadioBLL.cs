using System;
using System.Collections.Generic;
using Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers;
using System.Diagnostics;
using GeneralPurposeLib;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer
{
    public partial class RISBLL
    {
        public List<IBLL.DTO.RadioDTO> GetRadiosByRichiestaExt(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RadioDTO> radios = null;

            try
            {
                List<IDAL.VO.RadioVO> dalRes = dal.GetRadiosByRichiestaExt(id);
                radios = RadioMapper.RadMapper(dalRes);
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(radios), LibString.TypeName(radios)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return radios;
        }
        public IBLL.DTO.RadioDTO GetRadioById(string analidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RadioDTO radio = null;

            try
            {
                IDAL.VO.RadioVO dalRes = this.dal.GetRadioById(analidid);
                radio = RadioMapper.RadMapper(dalRes);
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(radio), LibString.TypeName(radio)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return radio;
        }
        public List<IBLL.DTO.RadioDTO> GetRadiosByIds(List<string> radioids)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RadioDTO> radios = null;

            try
            {
                List<IDAL.VO.RadioVO> anals_ = dal.GetRadiosByIds(radioids);
                radios = RadioMapper.RadMapper(anals_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(radios), LibString.TypeName(anals_), LibString.TypeName(radios)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return radios;
        }
        public IBLL.DTO.RadioDTO UpdateRadio(IBLL.DTO.RadioDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int stored = 0;
            IBLL.DTO.RadioDTO toReturn = null;

            try
            {
                IDAL.VO.RadioVO data_ = RadioMapper.RadMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));                
                stored = dal.SetRadio(data_);                
                toReturn = GetRadioById(data.radioidid.ToString());
                log.Info(string.Format("{0} {1} items added and {2} {3} retrieved back!", stored, LibString.TypeName(data_), LibString.ItemsNumber(toReturn), LibString.TypeName(toReturn)));
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
        public IBLL.DTO.RadioDTO AddRadio(IBLL.DTO.RadioDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RadioDTO toReturn = null;

            try
            {
                data.radioidid = null;
                IDAL.VO.RadioVO data_ = RadioMapper.RadMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));                
                IDAL.VO.RadioVO stored = dal.NewRadio(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = RadioMapper.RadMapper(stored);
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
        public List<IBLL.DTO.RadioDTO> AddRadios(List<IBLL.DTO.RadioDTO> data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RadioDTO> toReturn = null;

            try
            {
                data.ForEach(p => p.radioidid = null);
                List<IDAL.VO.RadioVO> data_ = RadioMapper.RadMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                List<IDAL.VO.RadioVO> stored = dal.NewRadio(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = RadioMapper.RadMapper(stored);
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
        public int DeleteRadioById(string analidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteRadioById(analidid);
                log.Info(string.Format("{0} items Deleted!", result));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public int DeleteRadioByIdRichiestaExt(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteRadioByIdRichiestaExt(richidid);
                log.Info(string.Format("{0} items Deleted!", result));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
    }
}
