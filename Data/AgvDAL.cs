using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Wcs;
using GoWMS.Server.Models.Hagv;
using GoWMS.Server.DataAccess;
using Serilog;


namespace GoWMS.Server.Data
{
    public class AgvDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<Tas_Agvworks> GetAllTaskAgv()
        {
            List<Tas_Agvworks> lstobj = new List<Tas_Agvworks>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM wcs.tas_agvworks");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Tas_Agvworks objrd = new Tas_Agvworks
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (int?)rdr["entity_lock"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (long?)rdr["client_id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Lpncode = rdr["lpncode"].ToString(),
                            Work_Code = rdr["work_code"].ToString(),
                            Work_Status = rdr["work_status"] == DBNull.Value ? null : (Int32?)rdr["work_status"],
                            Work_Id = rdr["work_id"] == DBNull.Value ? null : (Int64?)rdr["work_id"],
                            Agv_Name = rdr["agv_name"].ToString(),
                            Gate_Source = rdr["gate_source"].ToString(),
                            Gate_Dest = rdr["gate_dest"].ToString(),
                            Ctime = rdr["ctime"] == DBNull.Value ? null : (DateTime?)rdr["ctime"],
                            Stime = rdr["stime"] == DBNull.Value ? null : (DateTime?)rdr["stime"],
                            Etime = rdr["etime"] == DBNull.Value ? null : (DateTime?)rdr["etime"],
                            Work_Priority = rdr["work_priority"] == DBNull.Value ? null : (Int32?)rdr["work_priority"]
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

        public IEnumerable<Set_Agv_Gate> GetAllGateAgv()
        {
            List<Set_Agv_Gate> lstobj = new List<Set_Agv_Gate>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM hagv.vset_agv_gate ");
                    sql.AppendLine("ORDER BY gate_name");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Set_Agv_Gate objrd = new Set_Agv_Gate
                        {
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (long?)rdr["client_id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Gate_Name = rdr["gate_name"].ToString(),
                            Position_Code = rdr["position_code"].ToString(),
                            Area = rdr["area"].ToString(),
                            Gate_type = rdr["gate_type"] == DBNull.Value ? null : (Int32?)rdr["gate_type"],

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

        public IEnumerable<Functionreturn> CreateAvgworks(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority )
        {
            List<Functionreturn> lstobj = new List<Functionreturn>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT _retchk, _retmsg FROM");
                    sql.AppendLine("wcs.fuc_create_avgworks(");
                    sql.AppendLine("@_lpncode, @_worktype, @_gate_source, @_gate_dest,@_work_priority)");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();

                    cmd.Parameters.AddWithValue("@_lpncode", pallet);
                    cmd.Parameters.AddWithValue("@_worktype", worktype);
                    cmd.Parameters.AddWithValue("@_gate_source", gatesource);
                    cmd.Parameters.AddWithValue("@_gate_dest", gatedest);
                    cmd.Parameters.AddWithValue("@_work_priority", workpriority);
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Functionreturn objrd = new Functionreturn
                        {

                            Retchk = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"],
                            Retmsg = rdr["_retmsg"].ToString()
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

        public void CreateAvgwork(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority)
        {
            Int32? iRet = 0;
            string sRet = "Calling";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT _retchk, _retmsg FROM");
                    sql.AppendLine("wcs.fuc_create_avgworks(");
                    sql.AppendLine("@lpncode, @worktype, @gate_source, @gate_dest,@work_priority)");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue("@lpncode", pallet);
                    cmd.Parameters.AddWithValue("@worktype", worktype);
                    cmd.Parameters.AddWithValue("@gate_source", gatesource);
                    cmd.Parameters.AddWithValue("@gate_dest", gatedest);
                    cmd.Parameters.AddWithValue("@work_priority", workpriority);
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        iRet = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"];
                        sRet = rdr["_retchk"].ToString();

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
        }


        public IEnumerable<Vrptqueueloadtimeagv> GetAllReportTaskAgv()
        {
            List<Vrptqueueloadtimeagv> lstobj = new List<Vrptqueueloadtimeagv>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM wcs.vrptqueueloadtimeagv");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Vrptqueueloadtimeagv objrd = new Vrptqueueloadtimeagv
                        {
                            Lpncode = rdr["lpncode"].ToString(),
                            Work_code = rdr["work_code"].ToString(),
                            Work_status = rdr["work_status"] == DBNull.Value ? null : (Int32?)rdr["work_status"],
                            Agv_name = rdr["agv_name"].ToString(),
                            Gate_source = rdr["gate_source"].ToString(),
                            Gate_dest = rdr["gate_dest"].ToString(),
                            Ctime = rdr["ctime"] == DBNull.Value ? null : (DateTime?)rdr["ctime"],
                            Stime = rdr["stime"] == DBNull.Value ? null : (DateTime?)rdr["stime"],
                            Etime = rdr["etime"] == DBNull.Value ? null : (DateTime?)rdr["etime"],
                            Loadtime = rdr["loadtime"].ToString()
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


        public IEnumerable<Functionreturn> CancleTaskAGV(string pallet)
        {
            List<Functionreturn> lstobj = new List<Functionreturn>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT _retchk, _retmsg FROM");
                    sql.AppendLine("wcs.fuc_cancel_avgtask(");
                    sql.AppendLine("@_lpncode)");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();

                    cmd.Parameters.AddWithValue("@_lpncode", pallet);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Functionreturn objrd = new Functionreturn
                        {

                            Retchk = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"],
                            Retmsg = rdr["_retmsg"].ToString()
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

        public IEnumerable<RptTaskHourCount> GetAllReportTaskPerHourAgv()
        {
            List<RptTaskHourCount> lstobj = new List<RptTaskHourCount>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM wcs.vrptqueueperhouragv");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        RptTaskHourCount objrd = new RptTaskHourCount
                        {
                            W_hour = rdr["w_hour"] == DBNull.Value ? null : (DateTime?)rdr["w_hour"],
                            W_count = rdr["w_count"] == DBNull.Value ? null : (Int64?)rdr["w_count"]
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

        public IEnumerable<DashAgvTime> GetAllDashboardComplete()
            
        {
            List<DashAgvTime> lstobj = new List<DashAgvTime>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT checkday, agvtime, counttask");
                    sql.AppendLine("FROM wcs.vrptqueuecompleteagv");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DashAgvTime objrd = new DashAgvTime
                        {
                            Checkday = rdr["checkday"].ToString(),
                            Agvtime = rdr["agvtime"].ToString(),
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

        public DataTable GetQueryAgvStatusApiName()
        {
            DataTable vApiName = new DataTable();
            
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT base_url::text || api_name::text AS full_api_name, map_short_name");
                    sql.AppendLine("FROM hagv.set_agv_api");
                    sql.AppendLine("WHERE api_name=@api_name");
                    sql.AppendLine(";");

                    DataSet _ds = new DataSet();
                    DataTable _dt = new DataTable();
                    using (NpgsqlCommand vCmd = new NpgsqlCommand())
                    {
                        con.Open();
                        vCmd.Connection = con;
                        vCmd.CommandType = CommandType.Text;   
                        vCmd.CommandText = sql.ToString();
                        vCmd.Parameters.AddWithValue("@api_name", "queryAgvStatus");
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(vCmd);
                        da.Fill(_ds);
                        _dt = _ds.Tables[0];
                        vApiName = _dt;
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
            return vApiName;
        }
        public List<Agv_Status> GetAllAgvStatusDesc()
        {
            List<Agv_Status> lstobj = new List<Agv_Status>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT status, description");
                    sql.AppendLine("FROM hagv.agv_status");
                    sql.AppendLine(";");

                    DataSet _ds = new DataSet();
                    DataTable _dt = new DataTable();
                    using (NpgsqlCommand vCmd = new NpgsqlCommand())
                    {
                        con.Open();
                        vCmd.Connection = con;
                        vCmd.CommandType = CommandType.Text;
                        vCmd.CommandText = sql.ToString();
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(vCmd);
                        da.Fill(_ds);
                        _dt = _ds.Tables[0];
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            lstobj.Add(new Agv_Status() { Status = Convert.ToInt16(_dt.Rows[i]["status"].ToString()), Description = _dt.Rows[i]["description"].ToString() });
                        }
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

        public IEnumerable<AgvRptEODStation> GetEndofDayStation()
        {
            List<AgvRptEODStation> lstobj = new List<AgvRptEODStation>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendLine("SELECT date_trunc('day'::text, etime) AS w_date");
                    sql.AppendLine(", gate_source, gate_dest");
                    sql.AppendLine(", count(loadtime) AS w_count");
                    sql.AppendLine("FROM wcs.vrptqueueloadtimeagv");
                    sql.AppendLine("where etime is not null");
                    sql.AppendLine("group by date_trunc('day'::text, etime), gate_source, gate_dest ");
                    sql.AppendLine("order by w_date, gate_source, gate_dest");
                    sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        AgvRptEODStation objrd = new AgvRptEODStation
                        {
                            W_date = rdr["w_date"] == DBNull.Value ? null : (DateTime?)rdr["w_date"],
                            Gate_source = rdr["gate_source"].ToString(),
                            Gate_dest = rdr["gate_dest"].ToString(),
                            W_count = rdr["w_count"] == DBNull.Value ? null : (long?)rdr["w_count"]
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
