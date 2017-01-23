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
        // NO ENTITYFRAMEWORK
        public IDAL.VO.RadioVO GetRadioById(string radioidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RadioVO radio = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long esamidid_ = long.Parse(radioidid);
                string table = this.RadioTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "radioidid",
                            Op = DBSQL.Op.Equal,
                            Value = esamidid_,
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
                        radio = RadioMapper.RadMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(radio), LibString.TypeName(radio)));
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

            return radio;
        }
        public List<IDAL.VO.RadioVO> GetRadiosByIds(List<string> radiodids)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.RadioVO> radios = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.RadioTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>();
                int i = 0;
                foreach(string radioidid in radiodids)
                {
                    long radioidid_ = long.Parse(radioidid);
                    DBSQL.QueryCondition tmp = new DBSQL.QueryCondition()
                    {
                        Key = "radioidid",
                        Op = DBSQL.Op.Equal,
                        Value = radioidid_,
                        Conj = i < radiodids.Count-1 ? DBSQL.Conj.Or : DBSQL.Conj.None
                    };
                    conditions.Add("id" + i, tmp);
                    i++;
                }

                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        radios = RadioMapper.RadMapper(data);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(radios), LibString.TypeName(radios)));
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

            return radios;
        }
        public List<IDAL.VO.RadioVO> GetRadiosByRichiestaExt(string radioidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.RadioVO> radios = null;
            try
            {
                string connectionString = this.GRConnectionString;
                
                string table = this.RadioTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "radiorich",
                            Op = DBSQL.Op.Equal,
                            Value = radioidid,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    radios = RadioMapper.RadMapper(data);
                    log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(radios), LibString.TypeName(radios)));
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

            return radios;
        }
        public int SetRadio(IDAL.VO.RadioVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string analidid = data.radioidid.HasValue ? data.radioidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "radioidid" };

                if (analidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    long analidid_ = long.Parse(analidid);                    
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radioidid",
                                Value = analidid_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions, new List<string>() { "radioidid" });
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
        public IDAL.VO.RadioVO NewRadio(IDAL.VO.RadioVO data)
        {
            IDAL.VO.RadioVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "radioidid" };
                List<string> autoincrement = new List<string>() { "radioidid" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if(res.Rows.Count > 0)
                    {
                        result = RadioMapper.RadMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.radioidid));
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
        public List<IDAL.VO.RadioVO> NewRadio(List<IDAL.VO.RadioVO> data)
        {
            List<IDAL.VO.RadioVO> results = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "radioidid" };
                List<string> autoincrement = new List<string>() { "radioidid" };
                // INSERT NUOVA
                DataTable res = DBSQL.MultiInsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                {
                    results = RadioMapper.RadMapper(res);
                }                
                if(results!=null)
                {
                    if (results.Count > 0)
                    {
                        string tmp = "";
                        int o = 0;
                        foreach (IDAL.VO.RadioVO tmp_ in results)
                        {
                            tmp += tmp_.radioidid.Value.ToString();
                            if (o < results.Count - 1)
                                tmp += ", ";
                            o++;
                        }
                        log.Info(string.Format("Inserted {0} new records with IDs: {1}!", LibString.ItemsNumber(results), tmp));
                    }
                }
                else
                {
                    log.Info(string.Format("No records Inserted!"));
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

            return results;
        }
        public int DeleteRadioById(string radioidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long radioidid_ = long.Parse(radioidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radioidid",
                                Value = radioidid_,
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
        public int DeleteRadioByIdRichiestaExt(string richid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long radiorich_ = long.Parse(richid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radiopres",
                                Value = radiorich_,
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
        public int DeleteRadioByIdRichiesta(string id)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RadioTabName;

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
                                Key = "radiopres",
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