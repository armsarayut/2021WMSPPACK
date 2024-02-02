using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Mas;
using GoWMS.Server.Models.Erp;
using NpgsqlTypes;
using Serilog;
using GoWMS.Server.Models.Hagv;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GoWMS.Server.Data
{
    public class MasDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<Mas_Pallet_Go> GetAllMasterpalletGo()
        {
            List<Mas_Pallet_Go> lstobj = new List<Mas_Pallet_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select * ");
                    Sql.AppendLine("from wms.mas_pallet_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Pallet_Go objrd = new Mas_Pallet_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Pallettype = rdr["pallettype"] == DBNull.Value ? null : (Int32?)rdr["pallettype"]
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

        public Boolean ValidateMasterpallet(string spallet)
        {
            Boolean bret = false;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_pallet_go");
                    Sql.AppendLine("where palletno = @palletno");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();

                    cmd.Parameters.AddWithValue("@palletno", NpgsqlDbType.Varchar, spallet);

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        bret = true;
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
            return bret;
        }

        public IEnumerable<Mas_Storagebin_Go> GetAllStorageBinGo()
        {
            List<Mas_Storagebin_Go> lstobj = new List<Mas_Storagebin_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_storagebin_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Storagebin_Go objrd = new Mas_Storagebin_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Stocode = rdr["stocode"].ToString(),
                            Binno = rdr["binno"].ToString(),
                            Binname = rdr["binname"].ToString(),
                            Pallletno = rdr["pallletno"].ToString()
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

        public IEnumerable<Mas_Item_Go> GetAllMasteritemGo()
        {
            List<Mas_Item_Go> lstobj = new List<Mas_Item_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_item_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Item_Go objrd = new Mas_Item_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Itemcode = rdr["itemcode"].ToString(),
                            Itemname = rdr["itemname"].ToString(),
                            Itembrand = rdr["itembrand"].ToString(),
                            Weightnet = rdr["weightnet"] == DBNull.Value ? null : (decimal?)rdr["weightnet"],
                            Weightgross = rdr["weightgross"] == DBNull.Value ? null : (decimal?)rdr["weightgross"],
                            Weightuint = rdr["weightuint"].ToString(),
                            Vendor = rdr["vendor"].ToString()
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

        public IEnumerable<Mas_Storage_Go> GetAllMasterstorageGo()
        {
            List<Mas_Storage_Go> lstobj = new List<Mas_Storage_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_storage_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Storage_Go objrd = new Mas_Storage_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Stocode = rdr["stocode"].ToString(),
                            Stoname = rdr["stoname"].ToString(),
                            Stoaddress = rdr["stoaddress"].ToString()
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

        public IEnumerable<Mas_Worktype_Go> GetAllMasterworktypeGo()
        {
            List<Mas_Worktype_Go> lstobj = new List<Mas_Worktype_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_worktype_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Worktype_Go objrd = new Mas_Worktype_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Workcode = rdr["workcode"].ToString(),
                            Description = rdr["description"].ToString()
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

        public IEnumerable<Mas_Status_Go> GetAllMasterstatusGo()
        {
            List<Mas_Status_Go> lstobj = new List<Mas_Status_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_status_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Status_Go objrd = new Mas_Status_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Statcode = rdr["statcode"] == DBNull.Value ? null : (Int32?)rdr["statcode"],
                            Description = rdr["description"].ToString()
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

        public IEnumerable<Mas_Reason_Go> GetAllMasterreasonGo()
        {
            List<Mas_Reason_Go> lstobj = new List<Mas_Reason_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.mas_reason_go");
                    Sql.AppendLine("order by efidx");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Reason_Go objrd = new Mas_Reason_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Rescode = rdr["rescode"].ToString(),
                            Description = rdr["description"].ToString()
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

        public IEnumerable<Mas_Cylinder_Go> GetAllCylinderGo()
        {
            List<Mas_Cylinder_Go> lstobj = new List<Mas_Cylinder_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", material_code, material_description, customer_code, customer_description, customer_reference");
                    Sql.AppendLine(", color1, cylinder1, color2, cylinder2, color3, cylinder3, color4, cylinder4");
                    Sql.AppendLine(", color5, cylinder5, color6, cylinder6, color7, cylinder7, color8, cylinder8");
                    Sql.AppendLine(", color9, cylinder9, color10, cylinder10, palletno, itemtag");
                    Sql.AppendLine("FROM wms.mas_cylinder_go");
                    Sql.AppendLine("ORDER by palletno ASC, itemtag, ASC");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Cylinder_Go objrd = new Mas_Cylinder_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Material_Code = rdr["material_code"].ToString(),
                            Material_Description = rdr["material_description"].ToString(),
                            Customer_Code = rdr["customer_code"].ToString(),
                            Customer_Description = rdr["customer_description"].ToString(),
                            Customer_Reference = rdr["customer_reference"].ToString(),
                            Color1 = rdr["color1"].ToString(),
                            Cylinder1 = rdr["cylinder1"].ToString(),
                            Color2 = rdr["color2"].ToString(),
                            Cylinder2 = rdr["cylinder2"].ToString(),
                            Color3 = rdr["color3"].ToString(),
                            Cylinder3 = rdr["cylinder3"].ToString(),
                            Color4 = rdr["color4"].ToString(),
                            Cylinder4 = rdr["cylinder4"].ToString(),
                            Color5 = rdr["color5"].ToString(),
                            Cylinder5 = rdr["cylinder5"].ToString(),
                            Color6 = rdr["color6"].ToString(),
                            Cylinder6 = rdr["cylinder6"].ToString(),
                            Color7 = rdr["color7"].ToString(),
                            Cylinder7 = rdr["cylinder7"].ToString(),
                            Color8 = rdr["color8"].ToString(),
                            Cylinder8 = rdr["cylinder8"].ToString(),
                            Color9 = rdr["color9"].ToString(),
                            Cylinder9 = rdr["cylinder9"].ToString(),
                            Color10 = rdr["color10"].ToString(),
                            Cylinder10 = rdr["cylinder10"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Itemtag = rdr["itemtag"].ToString()
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



        public IEnumerable<Mas_Cylinder_Go> GetAllCylinderGobypallet(string pallet)
        {
            List<Mas_Cylinder_Go> lstobj = new List<Mas_Cylinder_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", material_code, material_description, customer_code, customer_description, customer_reference");
                    Sql.AppendLine(", color1, cylinder1, color2, cylinder2, color3, cylinder3, color4, cylinder4");
                    Sql.AppendLine(", color5, cylinder5, color6, cylinder6, color7, cylinder7, color8, cylinder8");
                    Sql.AppendLine(", color9, cylinder9, color10, cylinder10, palletno, itemtag");
                    Sql.AppendLine("FROM wms.mas_cylinder_go");
                    Sql.AppendLine("WHERE palletno = @palletno");
                    Sql.AppendLine("ORDER by palletno ASC, itemtag, ASC");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@palletno", NpgsqlDbType.Varchar, pallet);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Cylinder_Go objrd = new Mas_Cylinder_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Material_Code = rdr["material_code"].ToString(),
                            Material_Description = rdr["material_description"].ToString(),
                            Customer_Code = rdr["customer_code"].ToString(),
                            Customer_Description = rdr["customer_description"].ToString(),
                            Customer_Reference = rdr["customer_reference"].ToString(),
                            Color1 = rdr["color1"].ToString(),
                            Cylinder1 = rdr["cylinder1"].ToString(),
                            Color2 = rdr["color2"].ToString(),
                            Cylinder2 = rdr["cylinder2"].ToString(),
                            Color3 = rdr["color3"].ToString(),
                            Cylinder3 = rdr["cylinder3"].ToString(),
                            Color4 = rdr["color4"].ToString(),
                            Cylinder4 = rdr["cylinder4"].ToString(),
                            Color5 = rdr["color5"].ToString(),
                            Cylinder5 = rdr["cylinder5"].ToString(),
                            Color6 = rdr["color6"].ToString(),
                            Cylinder6 = rdr["cylinder6"].ToString(),
                            Color7 = rdr["color7"].ToString(),
                            Cylinder7 = rdr["cylinder7"].ToString(),
                            Color8 = rdr["color8"].ToString(),
                            Cylinder8 = rdr["cylinder8"].ToString(),
                            Color9 = rdr["color9"].ToString(),
                            Cylinder9 = rdr["cylinder9"].ToString(),
                            Color10 = rdr["color10"].ToString(),
                            Cylinder10 = rdr["cylinder10"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Itemtag = rdr["itemtag"].ToString()
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

        public IEnumerable<Mas_Cylinder_Go> GetAllCylinderGobytag(string itemtag)
        {
            List<Mas_Cylinder_Go> lstobj = new List<Mas_Cylinder_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", material_code, material_description, customer_code, customer_description, customer_reference");
                    Sql.AppendLine(", color1, cylinder1, color2, cylinder2, color3, cylinder3, color4, cylinder4");
                    Sql.AppendLine(", color5, cylinder5, color6, cylinder6, color7, cylinder7, color8, cylinder8");
                    Sql.AppendLine(", color9, cylinder9, color10, cylinder10, palletno, itemtag");
                    Sql.AppendLine("FROM wms.mas_cylinder_go");
                    Sql.AppendLine("WHERE itemtag = @itemtag");
                    Sql.AppendLine("ORDER by palletno ASC, itemtag, ASC");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@itemtag", NpgsqlDbType.Varchar, itemtag);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Cylinder_Go objrd = new Mas_Cylinder_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Material_Code = rdr["material_code"].ToString(),
                            Material_Description = rdr["material_description"].ToString(),
                            Customer_Code = rdr["customer_code"].ToString(),
                            Customer_Description = rdr["customer_description"].ToString(),
                            Customer_Reference = rdr["customer_reference"].ToString(),
                            Color1 = rdr["color1"].ToString(),
                            Cylinder1 = rdr["cylinder1"].ToString(),
                            Color2 = rdr["color2"].ToString(),
                            Cylinder2 = rdr["cylinder2"].ToString(),
                            Color3 = rdr["color3"].ToString(),
                            Cylinder3 = rdr["cylinder3"].ToString(),
                            Color4 = rdr["color4"].ToString(),
                            Cylinder4 = rdr["cylinder4"].ToString(),
                            Color5 = rdr["color5"].ToString(),
                            Cylinder5 = rdr["cylinder5"].ToString(),
                            Color6 = rdr["color6"].ToString(),
                            Cylinder6 = rdr["cylinder6"].ToString(),
                            Color7 = rdr["color7"].ToString(),
                            Cylinder7 = rdr["cylinder7"].ToString(),
                            Color8 = rdr["color8"].ToString(),
                            Cylinder8 = rdr["cylinder8"].ToString(),
                            Color9 = rdr["color9"].ToString(),
                            Cylinder9 = rdr["cylinder9"].ToString(),
                            Color10 = rdr["color10"].ToString(),
                            Cylinder10 = rdr["cylinder10"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Itemtag = rdr["itemtag"].ToString()
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

        public IEnumerable<Mas_Cylinderlist_Go> GetAllCylinderList()
        {
            List<Mas_Cylinderlist_Go> lstobj = new List<Mas_Cylinderlist_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();


                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", material_code, material_description");
                    Sql.AppendLine(", customer_code, customer_description, customer_reference, previos_ref");
                    Sql.AppendLine(", cylinderno, colorcode, cylindercode, cylinderbar, palletno, itemtag");
                    Sql.AppendLine("FROM wms.mas_cylinderlist_go ");
                    Sql.AppendLine("ORDER by material_code ASC, cylinderno ASC ");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Cylinderlist_Go objrd = new Mas_Cylinderlist_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Material_Code = rdr["material_code"].ToString(),
                            Material_Description = rdr["material_description"].ToString(),
                            Customer_Code = rdr["customer_code"].ToString(),
                            Customer_Description = rdr["customer_description"].ToString(),
                            Customer_Reference = rdr["customer_reference"].ToString(),
                            Previos_ref = rdr["previos_ref"].ToString(),
                            Cylinderno = rdr["cylinderno"] == DBNull.Value ? null : (Int32?)rdr["cylinderno"],
                            Cylindercode = rdr["cylindercode"].ToString(),
                            Colorcode = rdr["colorcode"].ToString(),
                            Cylinderbar = rdr["cylinderbar"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Itemtag = rdr["itemtag"].ToString()
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

        public IEnumerable<Mas_Cylinderlist_Go> GetAllCylinderListByPallet(string maplet)
        {
            List<Mas_Cylinderlist_Go> lstobj = new List<Mas_Cylinderlist_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", material_code, material_description");
                    Sql.AppendLine(", customer_code, customer_description, customer_reference, previos_ref");
                    Sql.AppendLine(", cylinderno, colorcode, cylindercode, cylinderbar, palletno, itemtag");
                    Sql.AppendLine("FROM wms.mas_cylinderlist_go ");
                    Sql.AppendLine("WHERE palletno = @palletno ");
                    Sql.AppendLine("ORDER by cylinderno ASC ");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    cmd.Parameters.AddWithValue("@palletno", NpgsqlDbType.Varchar, maplet);
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Cylinderlist_Go objrd = new Mas_Cylinderlist_Go
                        {
                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),
                            Material_Code = rdr["material_code"].ToString(),
                            Material_Description = rdr["material_description"].ToString(),
                            Customer_Code = rdr["customer_code"].ToString(),
                            Customer_Description = rdr["customer_description"].ToString(),
                            Customer_Reference = rdr["customer_reference"].ToString(),
                            Previos_ref = rdr["previos_ref"].ToString(),
                            Cylinderno =  rdr["cylinderno"] == DBNull.Value ? null : (Int32?)rdr["cylinderno"],
                            Cylindercode = rdr["cylindercode"].ToString(),
                            Colorcode = rdr["colorcode"].ToString(),
                            Cylinderbar = rdr["cylinderbar"].ToString(),
                            Palletno = rdr["palletno"].ToString(),
                            Itemtag = rdr["itemtag"].ToString()
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


        public async Task<bool> InsertCylinderList(List<Mas_Cylinderlist_Go> listOrder , string mpallet)
        {
            bool bret = false;
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO wms.mas_cylinderlist_go(");
                sql.AppendLine("material_code, material_description");
                sql.AppendLine(", customer_code, customer_description, customer_reference, previos_ref");
                sql.AppendLine(", cylinderno, colorcode, cylindercode, cylinderbar, palletno, itemtag)");
                sql.AppendLine("Values");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                var i = 0;
                foreach (var s in listOrder)
                {
                    if (i != 0) sql.AppendLine(",");
                    var material_code = "material_code" + i.ToString();
                    var material_description = "material_description" + i.ToString();
                    var customer_code = "customer_code" + i.ToString();
                    var customer_description = "customer_description" + i.ToString();
                    var customer_reference = "customer_reference" + i.ToString();
                    var previos_ref = "previos_ref" + i.ToString();
                    var cylinderno = "cylinderno" + i.ToString();
                    var colorcode = "colorcode" + i.ToString();
                    var cylindercode = "cylindercode" + i.ToString();
                    var cylinderbar = "cylinderbar" + i.ToString();
                    var palletno = "palletno" + i.ToString();
                    var itemtag = "itemtag" + i.ToString();
                   

                    sql.Append("(@").Append(material_code)
                        .Append(", @").Append(material_description)
                        .Append(", @").Append(customer_code)
                        .Append(", @").Append(customer_description)
                        .Append(", @").Append(customer_reference)
                         .Append(", @").Append(previos_ref)
                        .Append(", @").Append(cylinderno)
                         .Append(", @").Append(colorcode)
                        .Append(", @").Append(cylindercode)
                        .Append(", @").Append(cylinderbar)
                        .Append(", @").Append(palletno)
                        .Append(", @").Append(itemtag)
                        .Append(')');

                    cmd.Parameters.Add(new NpgsqlParameter<string>(material_code, s.Material_Code));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(material_description, s.Material_Description));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_code, s.Customer_Code));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_description, s.Customer_Description));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_reference, s.Customer_Reference));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(previos_ref, s.Previos_ref));
                    cmd.Parameters.Add(new NpgsqlParameter<int>(cylinderno, (int)s.Cylinderno));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(colorcode, s.Colorcode));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(cylindercode, s.Cylindercode));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(cylinderbar, s.Cylinderbar));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(palletno, mpallet));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(itemtag, s.Itemtag));

                    i++;

                }
                con.Open();

                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();
                bret = true;

            }
            catch (NpgsqlException ex)
            {
                bret = false;
                Log.Error(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            return bret;
        }

        public async Task<bool> InsertCylinderListMaster(List<Cylinder> listOrder, string mpallet)
        {
            bool bret = false;
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE FROM wms.mas_cylinderlist_go");
                sql.AppendLine("WHERE palletno=@palletno");
                sql.AppendLine(";");

                sql.AppendLine("INSERT INTO wms.mas_cylinderlist_go(");
                sql.AppendLine("material_code, material_description");
                sql.AppendLine(", customer_code, customer_description, customer_reference, previos_ref");
                sql.AppendLine(", cylinderno, colorcode, cylindercode, cylinderbar, palletno, itemtag)");
                sql.AppendLine("Values");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                var i = 0;
                foreach (var s in listOrder)
                {
                    if (i != 0) sql.AppendLine(",");
                    var material_code = "material_code" + i.ToString();
                    var material_description = "material_description" + i.ToString();
                    var customer_code = "customer_code" + i.ToString();
                    var customer_description = "customer_description" + i.ToString();
                    var customer_reference = "customer_reference" + i.ToString();
                    var previos_ref = "previos_ref" + i.ToString();
                    var cylinderno = "cylinderno" + i.ToString();
                    var colorcode = "colorcode" + i.ToString();
                    var cylindercode = "cylindercode" + i.ToString();
                    var cylinderbar = "cylinderbar" + i.ToString();
                    var palletno = "palletno" + i.ToString();
                    var itemtag = "itemtag" + i.ToString();


                    sql.Append("(@").Append(material_code)
                        .Append(", @").Append(material_description)
                        .Append(", @").Append(customer_code)
                        .Append(", @").Append(customer_description)
                        .Append(", @").Append(customer_reference)
                         .Append(", @").Append(previos_ref)
                        .Append(", @").Append(cylinderno)
                         .Append(", @").Append(colorcode)
                        .Append(", @").Append(cylindercode)
                        .Append(", @").Append(cylinderbar)
                        .Append(", @").Append(palletno)
                        .Append(", @").Append(itemtag)
                        .Append(')');

                    cmd.Parameters.Add(new NpgsqlParameter<string>(material_code, s.Material));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(material_description, s.Material_Description));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_code, s.Customer_Code));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_description, s.Customer_Description));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(customer_reference, s.Customer_Reference));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(previos_ref, s.Previos_Ref));
                    cmd.Parameters.Add(new NpgsqlParameter<int>(cylinderno, (int)s.Cylindeno));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(colorcode, s.Colorcode));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(cylindercode, s.Cylindercode));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(cylinderbar, s.Cylinderbar));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(palletno, mpallet));
                    cmd.Parameters.Add(new NpgsqlParameter<string>(itemtag, s.Cylinderbar));

                    i++;

                }
                cmd.Parameters.Add(new NpgsqlParameter<string>("@palletno", mpallet));

                con.Open();

                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();
                bret = true;

            }
            catch (NpgsqlException ex)
            {
                bret = false;
                Log.Error(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            return bret;
        }

        public void CancelCylinderListMasterBypack(string pallet, string pack)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Delete from  wms.mas_cylinderlist_go");
                sql.AppendLine("Where itemtag = @Pack ");
                sql.AppendLine("And palletno = @Pallet");
                NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Pallet", pallet);
                cmd.Parameters.AddWithValue("@Pack", pack);
                con.Open();
                cmd.ExecuteNonQuery();
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

        public IEnumerable<Cusetgatemode> GetAllGatecuroomList()
        {
            List<Cusetgatemode> lstobj = new List<Cusetgatemode>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();


                    Sql.AppendLine("select gate_name, ena_inbound, ena_outbound");
                    Sql.AppendLine("from hagv.set_agv_gate");
                    Sql.AppendLine("where area_group = @area_group and entity_lock = @entity_lock");
                    //Sql.AppendLine("where area_group = 3 and entity_lock = 0");
                    Sql.AppendLine("order by gate_name");
                    Sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue("@area_group", 3);
                    cmd.Parameters.AddWithValue("@entity_lock", 0);
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cusetgatemode objrd = new Cusetgatemode
                        {
                            Gate_name = rdr["gate_name"].ToString(),
                              Ena_inbound = rdr["ena_inbound"] == DBNull.Value ? false : (bool?)rdr["ena_inbound"],
                            Ena_outbound = rdr["ena_outbound"] == DBNull.Value ? false : (bool?)rdr["ena_outbound"],
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


        public IEnumerable<Mas_Curingtime> GetAllCuringtimeList()
        {
            List<Mas_Curingtime> lstobj = new List<Mas_Curingtime>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();


                    Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                    Sql.AppendLine(", job, job_code, item_code, adhesive1_std, adhesive2_std, adhesive3_std, adhesive4_std");
                    Sql.AppendLine(", type, film1, film2, film3, film4, film5, adhesive, hardener, layers, temp_c, time_hr, id");
                    Sql.AppendLine("FROM wms.mas_curingtime_go");
                    //Sql.AppendLine("where area_group = 3 and entity_lock = 0");
                    Sql.AppendLine("order by job, job_code, item_code");
                    Sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Mas_Curingtime objrd = new Mas_Curingtime
                        {


                            Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                            Device = rdr["device"].ToString(),

                            Job = rdr["job"].ToString(),
                            Job_Code = rdr["job_code"].ToString(),
                            Item_Code = rdr["item_code"].ToString(),
                            Adhesive1_STD = rdr["adhesive1_std"].ToString(),
                            Adhesive2_STD = rdr["adhesive2_std"].ToString(),
                            Adhesive3_STD = rdr["adhesive3_std"].ToString(),
                            Adhesive4_STD = rdr["adhesive4_std"].ToString(),
                            Type = rdr["type"].ToString(),
                            Film1 = rdr["film1"].ToString(),
                            Film2 = rdr["film2"].ToString(),
                            Film3 = rdr["film3"].ToString(),
                            Film4 = rdr["film4"].ToString(),
                            Film5 = rdr["film5"].ToString(),
                            Adhesive = rdr["adhesive"].ToString(),
                            Hardener = rdr["hardener"].ToString(),
                            Layers = rdr["layers"] == DBNull.Value ? null : (Int32?)rdr["layers"],
                            TempC = rdr["temp_c"] == DBNull.Value ? null : (Int32?)rdr["temp_c"],
                            TimeH = rdr["time_hr"] == DBNull.Value ? null : (Int32?)rdr["time_hr"],
                            ID = rdr["id"].ToString()
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

        public IEnumerable<Set_agv_gate> GetAllAgvgate()
        {
            List<Set_agv_gate> lstobj = new List<Set_agv_gate>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();


                    Sql.AppendLine("SELECT gate_name, client_id as detection_enable");
                    Sql.AppendLine("FROM hagv.set_agv_gate");
                    //Sql.AppendLine("Where entity_lock = @entity_lock ");
                    Sql.AppendLine(";");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    //cmd.Parameters.AddWithValue("@entity_lock", 0);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Set_agv_gate objrd = new Set_agv_gate
                        {

                            Gate_name = rdr["gate_name"].ToString(),

                            Detection_enable = rdr["detection_enable"] == DBNull.Value ? null : (Int64?)rdr["detection_enable"],

                            Enable = rdr["detection_enable"] == DBNull.Value ? true : Convert.ToBoolean(rdr["detection_enable"])
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

        public bool SetGateDetection(string setcode, Int32 setval)
        {
            bool bRet = false;

            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {

                StringBuilder sql = new StringBuilder();

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

   
                sql.AppendLine("UPDATE hagv.set_agv_gate");
                sql.AppendLine("SET client_id = @val_int");
                //sql.AppendLine("WHERE entity_lock = @entity_lock");
                sql.AppendLine("WHERE gate_name = @gate_name");
                sql.AppendLine(";");

                cmd.Parameters.AddWithValue("@val_int", setval);
                cmd.Parameters.AddWithValue("@gate_name", setcode);
                //cmd.Parameters.AddWithValue("@entity_lock", 0);

                con.Open();
                cmd.CommandText = sql.ToString();
                cmd.ExecuteNonQuery();

                bRet = true;

            }
            catch (NpgsqlException ex)
            {
                Log.Error(ex.ToString());
                bRet = false;
            }
            finally
            {
                con.Close();
            }


            return bRet;
        }

    }
}
