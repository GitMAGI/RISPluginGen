using System;
using Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer.Mappers;
using System.Diagnostics;
using GeneralPurposeLib;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.BusinessLogicLayer
{
    public partial class RISBLL
    {        
        public IBLL.DTO.RichiestaRISDTO GetRichiestaRISByIdExt(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RichiestaRISDTO rich = null;

            try
            {
                IDAL.VO.RichiestaRISVO rich_ = this.dal.GetRichiestaByIdExt(richidid);
                rich = RichiestaRISMapper.RichMapper(rich_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(rich), LibString.TypeName(rich_), LibString.TypeName(rich)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public IBLL.DTO.RichiestaRISDTO GetRichiestaRISById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RichiestaRISDTO rich = null;

            try
            {
                IDAL.VO.RichiestaRISVO rich_ = this.dal.GetRichiestaById(id);
                rich = RichiestaRISMapper.RichMapper(rich_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(rich), LibString.TypeName(rich_), LibString.TypeName(rich)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public List<IBLL.DTO.RichiestaRISDTO> GetRichiesteByEpisodio(string episid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RichiestaRISDTO> richs = null;

            try
            {
                List<IDAL.VO.RichiestaRISVO> dalRes = this.dal.GetRichiesteByEpisodio(episid);
                richs = RichiestaRISMapper.RichMapper(dalRes);
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(richs), LibString.TypeName(richs)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return richs;
        }
        public IBLL.DTO.RichiestaRISDTO AddRichiestaRIS(IBLL.DTO.RichiestaRISDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RichiestaRISDTO toReturn = null;

            try
            {
                data.id = null;
                IDAL.VO.RichiestaRISVO data_ = RichiestaRISMapper.RichMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                IDAL.VO.RichiestaRISVO stored = dal.NewRichiesta(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = RichiestaRISMapper.RichMapper(stored);
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
        public IBLL.DTO.RichiestaRISDTO UpdateRichiestaRIS(IBLL.DTO.RichiestaRISDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int stored = 0;
            IBLL.DTO.RichiestaRISDTO toReturn = null;
            string id = data.id.ToString();

            try
            {
                if (id == null || GetRichiestaRISById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                IDAL.VO.RichiestaRISVO data_ = RichiestaRISMapper.RichMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                stored = dal.SetRichiesta(data_);
                toReturn = GetRichiestaRISById(id);
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
        public int DeleteRichiestaRISById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteRichiestaById(id);
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
