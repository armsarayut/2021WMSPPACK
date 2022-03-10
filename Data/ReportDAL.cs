using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Rpt;
using NpgsqlTypes;
using System.Text;
using GoWMS.Server.DataAccess;
using Serilog;


namespace GoWMS.Server.Data
{
    public class ReportDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<RptAudittrial> GetAllAudittrial()
        {
            List<RptAudittrial> lstobj = new List<RptAudittrial>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select * ");
                    sql.AppendLine("from public.api_cylinder_go");
                    sql.AppendLine("order by efidx");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        RptAudittrial objrd = new RptAudittrial
                        {
                            /*
                            Idx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Entity_Lock = rdr["efstatus"] == DBNull.Value ? null : (int?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                            Client_Ip = rdr["device"].ToString(),
                            */
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
        public Boolean InsertAudittrial(String actdesc, String munname, long user)
        {
            long iUser = user;
            long iClient = 0;
            string sClient = "127.0.0.1";
            bool bRet = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("insert into public.rpt_audittrial(");
            sql.AppendLine("client_id, client_ip, id_stuser, menu_name, action_desc");
            sql.AppendLine(")");
            sql.AppendLine("Values(");
            sql.AppendLine("@client_id, @client_ip, @id_stuser, @menu_name, @action_desc");
            sql.AppendLine(")");
            sql.AppendLine(";");


            NpgsqlCommand NpgCmd = new NpgsqlCommand(sql.ToString())
            {
                CommandType = CommandType.Text
            };
            NpgCmd.Parameters.AddWithValue("@client_id", NpgsqlDbType.Bigint, iClient);
            NpgCmd.Parameters.AddWithValue("@client_ip", NpgsqlDbType.Varchar, sClient);
            NpgCmd.Parameters.AddWithValue("@id_stuser", NpgsqlDbType.Bigint, iUser);
            NpgCmd.Parameters.AddWithValue("@menu_name", NpgsqlDbType.Varchar, munname);
            NpgCmd.Parameters.AddWithValue("@action_desc", NpgsqlDbType.Varchar, actdesc);



            NpgDAL npgDAL = new NpgDAL();
            bRet = npgDAL.SyncInsertsqlData(NpgCmd);

            return bRet;
        }


        public IEnumerable<Rpt_Tracking_Go> GetTracking_GosByPack(string spallet)
        {
            List<Rpt_Tracking_Go> lstobj = new List<Rpt_Tracking_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select efidx, efstatus, created, modified, innovator, device, package_id, pallet_no, trackstate, tracklocation ");
                    sql.AppendLine("from wms.rpt_tracking_go");
                    sql.AppendLine("where package_id=@package_id");
                    sql.AppendLine("order by efidx");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    cmd.Parameters.AddWithValue("@package_id", NpgsqlDbType.Varchar, spallet);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Rpt_Tracking_Go objrd = new Rpt_Tracking_Go
                        {

                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (int?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Package_id = rdr["package_id"].ToString(),
                            Pallet_no = rdr["pallet_no"].ToString(),
                            Trackstate = rdr["trackstate"] == DBNull.Value ? null : (Int32?)rdr["trackstate"],
                            Tracklocation = rdr["tracklocation"].ToString()

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


        public IEnumerable<Rpt_Tracking_Go> GetTracking_GosAll()
        {
            List<Rpt_Tracking_Go> lstobj = new List<Rpt_Tracking_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select efidx, efstatus, created, modified, innovator, device, package_id, pallet_no, trackstate, tracklocation ");
                    sql.AppendLine("from wms.rpt_tracking_go");
                   // sql.AppendLine("where package_id=@package_id");
                    sql.AppendLine("order by efidx");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                   // cmd.Parameters.AddWithValue("@package_id", NpgsqlDbType.Varchar, spallet);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Rpt_Tracking_Go objrd = new Rpt_Tracking_Go
                        {

                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (int?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Package_id = rdr["package_id"].ToString(),
                            Pallet_no = rdr["pallet_no"].ToString(),
                            Trackstate = rdr["trackstate"] == DBNull.Value ? null : (Int32?)rdr["trackstate"],
                            Tracklocation = rdr["tracklocation"].ToString()

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


        public IEnumerable<Rpt_Tracking_Go> GetTracking_GosByDate(DateTime dtStart, DateTime dtStop)
        {
            List<Rpt_Tracking_Go> lstobj = new List<Rpt_Tracking_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select efidx, efstatus, created, modified, innovator, device, package_id, pallet_no, trackstate, tracklocation ");
                    sql.AppendLine("from wms.rpt_tracking_go");
                    sql.AppendLine("where created>=@startdate and created <= @stopdate");
                    sql.AppendLine("order by efidx");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    cmd.Parameters.AddWithValue("@startdate", NpgsqlDbType.Timestamp, dtStart);
                    cmd.Parameters.AddWithValue("@stopdate", NpgsqlDbType.Timestamp, dtStop);


                    //cmd.Parameters.AddWithValue("@screated", NpgsqlDbType.t, spallet);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Rpt_Tracking_Go objrd = new Rpt_Tracking_Go
                        {

                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (int?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Package_id = rdr["package_id"].ToString(),
                            Pallet_no = rdr["pallet_no"].ToString(),
                            Trackstate = rdr["trackstate"] == DBNull.Value ? null : (Int32?)rdr["trackstate"],
                            Tracklocation = rdr["tracklocation"].ToString()

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
