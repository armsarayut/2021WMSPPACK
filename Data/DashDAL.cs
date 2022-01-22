using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Das;
using GoWMS.Server.Models.Wcs;
using GoWMS.Server.Models.Public;
using NpgsqlTypes;
using System.Text;
using Serilog;

namespace GoWMS.Server.Data
{
    public class DashDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<Vrpt_shelfsummary> GetAllLocationSummary()
        {
            List<Vrpt_shelfsummary> lstobj = new List<Vrpt_shelfsummary>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select srm_name, srm_no, locavl, locemp, plemp, plerr, prohloc, total, percen " +
                    "FROM wcs.vrpt_shelfsummary " +
                    "ORDER BY srm_no", con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Vrpt_shelfsummary objrd = new Vrpt_shelfsummary
                        {
                            Srm_Name = rdr["srm_name"].ToString(),
                            Srm_No = rdr["srm_no"] == DBNull.Value ? null : (Int32?)rdr["srm_no"],
                            Locavl = rdr["locavl"] == DBNull.Value ? null : (Int64?)rdr["locavl"],
                            Locemp = rdr["locemp"] == DBNull.Value ? null : (Int64?)rdr["locemp"],
                            Plemp = rdr["plemp"] == DBNull.Value ? null : (Int64?)rdr["plemp"],
                            Plerr = rdr["plerr"] == DBNull.Value ? null : (Int64?)rdr["plerr"],
                            Prohloc = rdr["prohloc"] == DBNull.Value ? null : (Int64?)rdr["prohloc"],
                            Total = rdr["total"] == DBNull.Value ? null : (Int64?)rdr["total"],
                            Percen = rdr["percen"] == DBNull.Value ? null : (decimal?)rdr["percen"]
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (NpgsqlException ex)
                {
                    Log.Error(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            return lstobj;
        }

        public IEnumerable<VLocationDash> GetAllTasworkofday()
        {
            List<VLocationDash> lstobj = new List<VLocationDash>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT  work_code ,count(lpncode)as clpncode " +
                    "FROM wcs.rpt_works " +
                    "WHERE etime::DATE = now()::DATE " +
                    "AND work_status='COM' " +
                    "GROUP BY work_code", con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        VLocationDash objrd = new VLocationDash
                        {
                            Work_Code = rdr["work_code"].ToString(),
                            Clpncode = rdr["clpncode"] == DBNull.Value ? null : (Int64?)rdr["clpncode"]
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (NpgsqlException ex)
                {
                    Log.Error(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            return lstobj;
        }


        public IEnumerable<DashTaskTime> GetASRSDashboardComplete()

        {
            List<DashTaskTime> lstobj = new List<DashTaskTime>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT checkday, tasktime, counttask");
                    sql.AppendLine("FROM wcs.vrptqueuecompleteasrs");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DashTaskTime objrd = new DashTaskTime
                        {
                            Checkday = rdr["checkday"].ToString(),
                            Tasktime = rdr["tasktime"].ToString(),
                            Counttask = rdr["counttask"] == DBNull.Value ? null : (Int64?)rdr["counttask"]
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (NpgsqlException ex)
                {
                    Log.Error(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            return lstobj;
        }

        public IEnumerable<AsrsTaskSummary> GetTaskofday()
        {
            List<AsrsTaskSummary> lstobj = new List<AsrsTaskSummary>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT w_date, w01 , w101 ,  coalesce(w01, 0) + coalesce(w101, 0) as sumin");
                    sql.AppendLine(", w05, w102 , coalesce(w05, 0) + coalesce(w102, 0) as sumout");
                    sql.AppendLine(", wtotal");
                    sql.AppendLine("FROM wcs.vrpt_workendofday");
                    sql.AppendLine("where w_date = now()::date");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        AsrsTaskSummary objrd = new AsrsTaskSummary
                        {
                            W_date = rdr["w_date"] == DBNull.Value ? null : (DateTime?)rdr["w_date"],
                            W01 = rdr["w01"] == DBNull.Value ? null : (long?)rdr["w01"],
                            W101 = rdr["w101"] == DBNull.Value ? null : (long?)rdr["w101"],
                            Sumin = rdr["sumin"] == DBNull.Value ? null : (long?)rdr["sumin"],
                            W05 = rdr["w05"] == DBNull.Value ? null : (long?)rdr["w05"],
                            W102 = rdr["w102"] == DBNull.Value ? null : (long?)rdr["w102"],
                            Sumout = rdr["sumout"] == DBNull.Value ? null : (long?)rdr["sumout"],
                            Wtotal = rdr["wtotal"] == DBNull.Value ? null : (long?)rdr["wtotal"]
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (NpgsqlException ex)
                {
                    Log.Error(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            return lstobj;
        }



    }

   

}
