using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer.Mappers;
using DBSQLSuite;

namespace Seminabit.Sanita.OrderEntry.RIS.DataAccessLayer
{
    public partial class RISDAL
    {        
        public IDAL.VO.RichiestaRISVO GetRichiestaById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RichiestaRISVO rich = null;
            try
            {
                string connectionString = this.GRConnectionString;
                                
                string table = this.RichiestaRISTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "id",
                            Op = DBSQL.Op.Equal,
                            Value = id,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        rich = RichiestaRISMapper.RichMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(rich), LibString.TypeName(rich)));
                    }                    
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public IDAL.VO.RichiestaRISVO GetRichiestaByIdExt(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RichiestaRISVO rich = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.RichiestaRISTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "richidid",
                            Op = DBSQL.Op.Equal,
                            Value = richid,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        rich = RichiestaRISMapper.RichMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(rich), LibString.TypeName(rich)));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public List<IDAL.VO.RichiestaRISVO> GetRichiesteByEpisodio(string episid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RichiestaRISTabName;

            List<IDAL.VO.RichiestaRISVO> richs = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long episid_ = long.Parse(episid);

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "episodioid",
                        new DBSQL.QueryCondition() {
                            Key = "episodioid",
                            Op = DBSQL.Op.Equal,
                            Value = episid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    richs = RichiestaRISMapper.RichMapper(data); 
                    if(richs!=null && richs.Count>0)
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(richs), LibString.TypeName(richs)));
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return richs;
        }
        public int SetRichiesta(IDAL.VO.RichiestaRISVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RichiestaRISTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string id = data.id.HasValue ? data.id.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "id" };

                if (id == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    long id_ = long.Parse(id);
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "idCentrale",
                            new DBSQL.QueryCondition()
                            {
                                Key = "id",
                                Value = id_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions, new List<string>() { "id" });
                    log.Info(string.Format("Updated {0} records!", result));
                }
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
        public IDAL.VO.RichiestaRISVO NewRichiesta(IDAL.VO.RichiestaRISVO data)
        {
            IDAL.VO.RichiestaRISVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RichiestaRISTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "ID" };
                List<string> autoincrement = new List<string>() { "iD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if (res.Rows.Count > 0)
                    {
                        result = RichiestaRISMapper.RichMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.id));
                    }                    
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
        public int DeleteRichiestaById(string id)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RichiestaRISTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long id_ = long.Parse(id);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "id",
                                Value = id_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                result = DBSQL.DeleteOperation(connectionString, table, conditions);
                log.Info(string.Format("Deleted {0} records!", result));
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