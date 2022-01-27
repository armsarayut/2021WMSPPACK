using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Models.Wcs;
using Npgsql;
using NpgsqlTypes;
using System.Text;
using System.Data;
using Serilog;


namespace GoWMS.Server.Data
{
    public class WcsDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<Vmachine> GetAllMachine()
        {
            List<Vmachine> lstobj = new List<Vmachine>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select mccode, information, st_no, desc_th, is_alert, backcolor, focecolor, is_cmd");
                    sql.AppendLine("from wcs.vmachine_status");
                    sql.AppendLine("order by mccode");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Vmachine objrd = new Vmachine
                        {
                            Mccode = rdr["mccode"].ToString(),
                            Information = rdr["information"].ToString(),
                            St_no = rdr["st_no"] == DBNull.Value ? null : (Int32?)rdr["st_no"],
                            Desc_th = rdr["desc_th"].ToString(),
                            Is_alert = rdr["is_alert"] == DBNull.Value ? true : (bool?)rdr["is_alert"],
                            Backcolor = rdr["backcolor"].ToString(),
                            Focecolor = rdr["focecolor"].ToString(),
                            Is_cmd = rdr["is_cmd"] == DBNull.Value ? false : (bool?)rdr["is_cmd"]
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

        public IEnumerable<Vmachine_command> GetCommandMachine(string mccode)
        {
            List<Vmachine_command> lstobj = new List<Vmachine_command>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select mccode, st_no, st_desc_th, sg_no, sg_desc_th");
                    sql.AppendLine("from wcs.vmachine_command");
                    sql.AppendLine("where  mccode=@mccode");
                    sql.AppendLine("order by st_no");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();

                    cmd.Parameters.AddWithValue("@mccode", mccode);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Vmachine_command objrd = new Vmachine_command
                        {
                            Mccode = rdr["mccode"].ToString(),
                            St_no = rdr["st_no"] == DBNull.Value ? null : (Int32?)rdr["st_no"],
                            St_desc_th = rdr["st_desc_th"].ToString(),
                            Sg_no = rdr["sg_no"].ToString(),
                            Sg_desc_th = rdr["sg_desc_th"].ToString()
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

        public Boolean CreateCommandMC(string mccode, Int32 command )
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            string cmdcode = "CMD08";
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from wcs.fuc_create_mccommand(:mccode , :cmdcode, :command);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":mccode", NpgsqlDbType.Varchar, mccode);
                    cmd.Parameters.AddWithValue(":cmdcode", NpgsqlDbType.Varchar, cmdcode);
                    cmd.Parameters.AddWithValue(":command", NpgsqlDbType.Integer, command);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        iRet = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"];
                        sRet = rdr["_retmsg"].ToString();
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

                if (iRet == 1)
                {
                    bRet = true;
                }
            }
            return bRet;
        }


        public Boolean CreatePotocalMC(string mccode, Int32 startpos, Int32 stoppos, Int32 unittyp, string palletid, Int32 weight, Int32 command)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            string cmdcode = "CMD08";
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from wcs.fuc_create_mcprotocol(:mccode , :startpos, :stoppos, :unittype, :palletid , :weight, :command);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":mccode", NpgsqlDbType.Varchar, mccode);
                    cmd.Parameters.AddWithValue(":startpos", NpgsqlDbType.Integer, startpos);
                    cmd.Parameters.AddWithValue(":stoppos", NpgsqlDbType.Integer, stoppos);
                    cmd.Parameters.AddWithValue(":unittype", NpgsqlDbType.Integer, unittyp);
                    cmd.Parameters.AddWithValue(":palletid", NpgsqlDbType.Varchar, palletid);
                    cmd.Parameters.AddWithValue(":weight", NpgsqlDbType.Integer, weight);
                    cmd.Parameters.AddWithValue(":command", NpgsqlDbType.Integer, command);
    

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        iRet = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"];
                        sRet = rdr["_retmsg"].ToString();
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

                if (iRet == 1)
                {
                    bRet = true;
                }
            }
            return bRet;
        }


        public IEnumerable<Vset_gate_rgv> GetGateRgv()
        {
            List<Vset_gate_rgv> lstobj = new List<Vset_gate_rgv>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select mccode, st_no, type");
                    sql.AppendLine("from wcs.vset_gate_rgv");
                    sql.AppendLine("order by st_no");
                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();


                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Vset_gate_rgv objrd = new Vset_gate_rgv
                        {
                            Mccode = rdr["mccode"].ToString(),
                            St_no = rdr["st_no"] == DBNull.Value ? null : (Int32?)rdr["st_no"],
                            Type = rdr["type"].ToString()
                          
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


        public IEnumerable<Tas_WorksInfo> GetASRSWork()
        {
            List<Tas_WorksInfo> lstobj = new List<Tas_WorksInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT idx, created, entity_lock, modified, client_id, client_ip");
                    sql.AppendLine(", su_no, lpncode, work_code, work_status, work_srm, work_location");
                    sql.AppendLine(", work_weight, actual_weight, work_size, actual_size, work_gate, work_ref");
                    sql.AppendLine(", ctime, stime, etime, work_priority");
                    sql.AppendLine("from wcs.tas_works");
                    sql.AppendLine("order by created");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();


                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Tas_WorksInfo objrd = new Tas_WorksInfo
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (Int32?)rdr["entity_lock"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (Int64?)rdr["client_id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Su_No = rdr["su_no"].ToString(),
                            Lpncode = rdr["lpncode"].ToString(),
                            Work_Code = rdr["work_code"].ToString(),

                            Work_Status = rdr["work_status"].ToString(),
                            Work_Srm = rdr["work_srm"].ToString(),
                            Work_Location = rdr["work_location"].ToString(),
                            Work_Weight = rdr["work_weight"] == DBNull.Value ? null : (decimal?)rdr["work_weight"],
                            Actual_Weight = rdr["actual_weight"] == DBNull.Value ? null : (decimal?)rdr["actual_weight"],
                            Work_Size = rdr["work_size"] == DBNull.Value ? null : (Int32?)rdr["work_size"],
                            Actual_Size = rdr["actual_size"] == DBNull.Value ? null : (Int32?)rdr["actual_size"],
                            Work_Gate = rdr["work_gate"].ToString(),
                            Work_Ref = rdr["work_ref"].ToString(),

                            Ctime = rdr["ctime"] == DBNull.Value ? null : (DateTime?)rdr["ctime"],
                            Stime = rdr["stime"] == DBNull.Value ? null : (DateTime?)rdr["stime"],
                            Etime = rdr["etime"] == DBNull.Value ? null : (DateTime?)rdr["etime"],
                            Work_Priority = rdr["work_priority"] == DBNull.Value ? null : (Int32?)rdr["work_priority"],


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

        public IEnumerable<Tas_Mcworks> GetASRSWorks()
        {
            List<Tas_Mcworks> lstobj = new List<Tas_Mcworks>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT idx, created, entity_lock, modified, client_id, client_ip");
                    sql.AppendLine(", lpncode, work_code, work_status, work_id");
                    sql.AppendLine(", srm_no, srm_from, srm_to, srm_status");
                    sql.AppendLine(", rgv_no, rgv_from, rgv_to, rgv_status");
                    sql.AppendLine(", cvy_no, cvy_from, cvy_to, cvy_status");
                    sql.AppendLine(", pallet_no, pallet_hight, pallet_width, pallet_size");
                    sql.AppendLine(", ctime, stime, etime, gate_out, work_priority");
                    sql.AppendLine("FROM wcs.tas_mcworks");
                    sql.AppendLine("order by ctime");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();


                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Tas_Mcworks objrd = new Tas_Mcworks
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (Int32?)rdr["entity_lock"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (Int64?)rdr["client_id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Lpncode = rdr["lpncode"].ToString(),
                            Work_Code = rdr["work_code"].ToString(),
                            Work_Status = rdr["work_status"] == DBNull.Value ? null : (Int32?)rdr["work_status"],
                            Work_Id = rdr["work_id"] == DBNull.Value ? null : (Int64?)rdr["work_id"],
                            Srm_No = rdr["srm_no"] == DBNull.Value ? null : (Int32?)rdr["srm_no"],
                            Srm_From = rdr["srm_from"] == DBNull.Value ? null : (Int32?)rdr["srm_from"],
                            Srm_To = rdr["srm_to"] == DBNull.Value ? null : (Int32?)rdr["srm_to"],
                            Srm_Status = rdr["srm_status"] == DBNull.Value ? null : (Int32?)rdr["srm_status"],

                            Rgv_No = rdr["rgv_no"] == DBNull.Value ? null : (Int32?)rdr["rgv_no"],
                            Rgv_From = rdr["rgv_from"] == DBNull.Value ? null : (Int32?)rdr["rgv_from"],
                            Rgv_To = rdr["rgv_to"] == DBNull.Value ? null : (Int32?)rdr["rgv_to"],
                            Rgv_Status = rdr["rgv_status"] == DBNull.Value ? null : (Int32?)rdr["rgv_status"],

                            Cvy_No = rdr["cvy_no"] == DBNull.Value ? null : (Int32?)rdr["cvy_no"],
                            Cvy_From = rdr["cvy_from"] == DBNull.Value ? null : (Int32?)rdr["cvy_from"],
                            Cvy_To = rdr["cvy_to"] == DBNull.Value ? null : (Int32?)rdr["cvy_to"],
                            Cvy_Status = rdr["cvy_status"] == DBNull.Value ? null : (Int32?)rdr["cvy_status"],

                            Pallet_No = rdr["pallet_no"] == DBNull.Value ? null : (Int32?)rdr["pallet_no"],
                            Pallet_Hight = rdr["pallet_hight"] == DBNull.Value ? null : (Int32?)rdr["pallet_hight"],
                            Pallet_Width = rdr["pallet_width"] == DBNull.Value ? null : (Int32?)rdr["pallet_width"],
                            Pallet_Size = rdr["pallet_size"] == DBNull.Value ? null : (Int32?)rdr["pallet_size"],



                            Ctime = rdr["ctime"] == DBNull.Value ? null : (DateTime?)rdr["ctime"],
                            Stime = rdr["stime"] == DBNull.Value ? null : (DateTime?)rdr["stime"],
                            Etime = rdr["etime"] == DBNull.Value ? null : (DateTime?)rdr["etime"],
                            Gate_Out = rdr["gate_out"] == DBNull.Value ? null : (Int32?)rdr["gate_out"],
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

        public IEnumerable<Tas_Rgvworks> GetRGVWorks()
        {
            List<Tas_Rgvworks> lstobj = new List<Tas_Rgvworks>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT idx, created, entity_lock, modified, client_id, client_ip");
                    sql.AppendLine(", lpncode, work_code, work_status, work_id");
                    sql.AppendLine(", rgv_no, rgv_from, rgv_to, rgv_status");
                    sql.AppendLine(", cvy_no, cvy_from, cvy_to, cvy_status");
                    sql.AppendLine(", pallet_no, pallet_hight, pallet_width, pallet_size");
                    sql.AppendLine(", ctime, stime, etime, gate_out, work_priority");
                    sql.AppendLine("FROM wcs.tas_rgvworks");
                    sql.AppendLine("order by ctime");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();


                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Tas_Rgvworks objrd = new Tas_Rgvworks
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (Int32?)rdr["entity_lock"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (Int64?)rdr["client_id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Lpncode = rdr["lpncode"].ToString(),
                            Work_Code = rdr["work_code"].ToString(),
                            Work_Status = rdr["work_status"] == DBNull.Value ? null : (Int32?)rdr["work_status"],
                            Work_Id = rdr["work_id"] == DBNull.Value ? null : (Int64?)rdr["work_id"],
                          
                            Rgv_No = rdr["rgv_no"] == DBNull.Value ? null : (Int32?)rdr["rgv_no"],
                            Rgv_From = rdr["rgv_from"] == DBNull.Value ? null : (Int32?)rdr["rgv_from"],
                            Rgv_To = rdr["rgv_to"] == DBNull.Value ? null : (Int32?)rdr["rgv_to"],
                            Rgv_Status = rdr["rgv_status"] == DBNull.Value ? null : (Int32?)rdr["rgv_status"],

                            Cvy_No = rdr["cvy_no"] == DBNull.Value ? null : (Int32?)rdr["cvy_no"],
                            Cvy_From = rdr["cvy_from"] == DBNull.Value ? null : (Int32?)rdr["cvy_from"],
                            Cvy_To = rdr["cvy_to"] == DBNull.Value ? null : (Int32?)rdr["cvy_to"],
                            Cvy_Status = rdr["cvy_status"] == DBNull.Value ? null : (Int32?)rdr["cvy_status"],

                            Pallet_No = rdr["pallet_no"] == DBNull.Value ? null : (Int32?)rdr["pallet_no"],
                            Pallet_Hight = rdr["pallet_hight"] == DBNull.Value ? null : (Int32?)rdr["pallet_hight"],
                            Pallet_Width = rdr["pallet_width"] == DBNull.Value ? null : (Int32?)rdr["pallet_width"],
                            Pallet_Size = rdr["pallet_size"] == DBNull.Value ? null : (Int32?)rdr["pallet_size"],

                            Ctime = rdr["ctime"] == DBNull.Value ? null : (DateTime?)rdr["ctime"],
                            Stime = rdr["stime"] == DBNull.Value ? null : (DateTime?)rdr["stime"],
                            Etime = rdr["etime"] == DBNull.Value ? null : (DateTime?)rdr["etime"],
                            Gate_Out = rdr["gate_out"] == DBNull.Value ? null : (Int32?)rdr["gate_out"],
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

    }
}
