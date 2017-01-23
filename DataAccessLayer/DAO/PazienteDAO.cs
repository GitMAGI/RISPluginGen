﻿using GeneralPurposeLib;
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
        public IDAL.VO.PazienteVO GetPazienteById(string pazidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.PazienteVO pazi = null;

            try
            {
                string connectionString = this.GRConnectionString;
                string table = this.PazienteTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "PAZIIDID",
                            Op = DBSQL.Op.Equal,
                            Value = pazidid,
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
                        pazi = PazienteMapper.PaziMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(pazi), LibString.TypeName(pazi)));
                    }
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

            return pazi;
        }
        public List<IDAL.VO.PazienteVO> GetPazienteBy5IdentityFields(string pazicogn, string pazinome, string pazisess, DateTime pazidata, string pazicofi)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.PazienteVO> pazis = null;

            try
            {
                string connectionString = this.GRConnectionString;
                string table = this.PazienteTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "cognome",
                        new DBSQL.QueryCondition() {
                            Key = "PAZICOGN",
                            Op = DBSQL.Op.Equal,
                            Value = pazicogn,
                            Conj = DBSQL.Conj.And
                        }
                    },
                    {
                        "nome",
                        new DBSQL.QueryCondition() {
                            Key = "PAZINOME",
                            Op = DBSQL.Op.Equal,
                            Value = pazinome,
                            Conj = DBSQL.Conj.And
                        }
                    },
                    {
                        "dataNascita",
                        new DBSQL.QueryCondition() {
                            Key = "PAZIDATA",
                            Op = DBSQL.Op.Equal,
                            Value = pazidata,
                            Conj = DBSQL.Conj.And
                        }
                    },
                    {
                        "codFiscale",
                        new DBSQL.QueryCondition() {
                            Key = "PAZICOFI",
                            Op = DBSQL.Op.Equal,
                            Value = pazicofi,
                            Conj = DBSQL.Conj.And
                        }
                    },
                    {
                        "sesso",
                        new DBSQL.QueryCondition() {
                            Key = "PAZISESS",
                            Op = DBSQL.Op.Equal,
                            Value = pazisess,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    pazis = PazienteMapper.PaziMapper(data);
                    log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(pazis), LibString.TypeName(pazis)));
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

            return pazis;
        }
        public int SetPaziente(IDAL.VO.PazienteVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PazienteTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string paziidid = data.paziidid.HasValue ? data.paziidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "paziidid" };

                if (paziidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "PAZIIDID",
                                Value = paziidid,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions);
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
        public IDAL.VO.PazienteVO NewPaziente(IDAL.VO.PazienteVO data)
        {
            IDAL.VO.PazienteVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PazienteTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "PAZIIDID" };
                List<string> autoincrement = new List<string>() { "PaZiIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if (res.Rows.Count > 0)
                    {
                        result = PazienteMapper.PaziMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.paziidid));
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
        public int DeletePazienteById(string pazidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PazienteTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long pazidid_ = long.Parse(pazidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "pazidid",
                                Value = pazidid_,
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
