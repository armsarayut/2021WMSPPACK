﻿using System;
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






    }
}