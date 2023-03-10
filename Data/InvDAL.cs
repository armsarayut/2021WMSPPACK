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
using GoWMS.Server.Models.Inv;
using Serilog;

namespace GoWMS.Server.Data
{
    public class InvDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<InvStockList> GetStockList()
        {
            List<InvStockList> lstobj = new List<InvStockList>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    
                    Sql.AppendLine("SELECT row_number() over(order by  itemcode asc) AS rn, efstatus, ");
                    Sql.AppendLine("itemcode, itemname, quantity, pallettag, pallteno, storagearea, storagebin, itemtag");
                    Sql.AppendLine("FROM wms.inv_stock_go ");
                    Sql.AppendLine("WHERE allocatequantity < quantity");
                    Sql.AppendLine("ORDER BY itemcode");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        InvStockList objrd = new InvStockList
                        {
                            Rn = rdr["rn"] == DBNull.Value ? null : (Int64?)rdr["rn"],
                            Item_code = rdr["itemcode"].ToString(),
                            Item_name = rdr["itemname"].ToString(),
                            Qty = rdr["quantity"] == DBNull.Value ? null : (decimal?)rdr["quantity"],
                            Su_no = rdr["pallettag"].ToString(),
                            Palletcode = rdr["pallteno"].ToString(),
                            Shelfname = rdr["storagebin"].ToString(),
                            StorageArae = rdr["storagearea"].ToString(),
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Tag_no = rdr["itemtag"].ToString()
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

        public IEnumerable<InvStockList> GetStockListCuRoom()
        {
            List<InvStockList> lstobj = new List<InvStockList>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();

                    //Sql.AppendLine("SELECT row_number() over(order by  itemcode asc) AS rn, efstatus, ");
                    //Sql.AppendLine("itemcode, itemname, quantity, pallettag, pallteno, storagearea, storagebin, itemtag");
                    //Sql.AppendLine(",CAST (AGE(now()::timestamp(0), storagetime::timestamp(0)) as text) as ages");
                    //Sql.AppendLine(",TRUNC(EXTRACT(EPOCH FROM AGE(now()::timestamp(0), storagetime::timestamp(0))/60)) as ageminutes");

                    //Sql.AppendLine("FROM wms.inv_stock_go ");
                    //Sql.AppendLine("WHERE allocatequantity < quantity");
                    //Sql.AppendLine("AND  storagearea in (@storagearea1, @storagearea2)");
                    //Sql.AppendLine("ORDER BY itemcode");

                    Sql.AppendLine("SELECT row_number() over(order by  itemcode asc) AS rn, efstatus, ");
                    Sql.AppendLine("itemcode, itemname, quantity, pallettag, pallteno, storagearea, storagebin, itemtag");
                    Sql.AppendLine(",ages");
                    Sql.AppendLine(",ageminutes");
                    Sql.AppendLine(",time_hr");
                    Sql.AppendLine("FROM wms.vrpt_stock_curoom ");
                    //Sql.AppendLine("WHERE allocatequantity < quantity");
                    Sql.AppendLine("WHERE  storagearea in (@storagearea1, @storagearea2)");
                    Sql.AppendLine("ORDER BY itemcode");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new NpgsqlParameter<string>("@storagearea1", "Curing 40"));
                    cmd.Parameters.Add(new NpgsqlParameter<string>("@storagearea2", "Curing 50"));

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        InvStockList objrd = new InvStockList
                        {
                            Rn = rdr["rn"] == DBNull.Value ? null : (Int64?)rdr["rn"],
                            Item_code = rdr["itemcode"].ToString(),
                            Item_name = rdr["itemname"].ToString(),
                            Qty = rdr["quantity"] == DBNull.Value ? null : (decimal?)rdr["quantity"],
                            Su_no = rdr["pallettag"].ToString(),
                            Palletcode = rdr["pallteno"].ToString(),
                            Shelfname = rdr["storagebin"].ToString(),
                            StorageArae = rdr["storagearea"].ToString(),
                            Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                            Tag_no = rdr["itemtag"].ToString(),
                            AgeTimes = rdr["ages"].ToString(),
                            AgeMinutes = rdr["ageminutes"] == DBNull.Value ? null : (Double?)rdr["ageminutes"],
                            LimitTime = rdr["time_hr"] == DBNull.Value ? 0 : (Int32?)rdr["time_hr"]
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

        public IEnumerable<InvStockSum> GetStockSum()
        {
            List<InvStockSum> lstobj = new List<InvStockSum>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine("SELECT row_number() over(order by itemcode asc) AS rn,");
                    Sql.AppendLine("itemcode, itemname, sum(quantity) as totalstock, count(pallettag) as countpallet");
                    Sql.AppendLine("FROM wms.inv_stock_go ");
                    Sql.AppendLine("WHERE allocatequantity < quantity");
                    Sql.AppendLine("GROUP BY itemcode, itemname");
                    Sql.AppendLine("ORDER BY itemcode");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        InvStockSum objrd = new InvStockSum
                        {
                            Rn = rdr["rn"] == DBNull.Value ? null : (Int64?)rdr["rn"],
                            Item_code = rdr["itemcode"].ToString(),
                            Item_name = rdr["itemname"].ToString(),
                            Totalstock = rdr["totalstock"] == DBNull.Value ? null : (Decimal?)rdr["totalstock"],
                            Countpallet = rdr["countpallet"] == DBNull.Value ? null : (Int64?)rdr["countpallet"]
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

        public IEnumerable<InvStockSum> GetStockSumCuRoom()
        {
            List<InvStockSum> lstobj = new List<InvStockSum>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine("SELECT row_number() over(order by itemcode asc) AS rn,");
                    Sql.AppendLine("itemcode, itemname, sum(quantity) as totalstock, count(pallettag) as countpallet");
                    Sql.AppendLine("FROM wms.inv_stock_go ");
                    Sql.AppendLine("WHERE allocatequantity < quantity");
                    Sql.AppendLine("AND  storagearea in (@storagearea1, @storagearea2)");
                    Sql.AppendLine("GROUP BY itemcode, itemname");
                    Sql.AppendLine("ORDER BY itemcode");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new NpgsqlParameter<string>("@storagearea1", "Curing 40"));
                    cmd.Parameters.Add(new NpgsqlParameter<string>("@storagearea2", "Curing 50"));
                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        InvStockSum objrd = new InvStockSum
                        {
                            Rn = rdr["rn"] == DBNull.Value ? null : (Int64?)rdr["rn"],
                            Item_code = rdr["itemcode"].ToString(),
                            Item_name = rdr["itemname"].ToString(),
                            Totalstock = rdr["totalstock"] == DBNull.Value ? null : (Decimal?)rdr["totalstock"],
                            Countpallet = rdr["countpallet"] == DBNull.Value ? null : (Int64?)rdr["countpallet"]
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
        public IEnumerable<Vrpt_shelf_listInfo> GetShelfLocation()
        {
            List<Vrpt_shelf_listInfo> lstobj = new List<Vrpt_shelf_listInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT modified, srm_no, shelf_no, shelfcode, shelfname");
                    Sql.AppendLine(", shelfbank, shelfframe, shelfbay, shelflevel, shelfstatus");
                    Sql.AppendLine(", lpncode, refercode, actual_weight, actual_size, desc_size, st_desc");
                    Sql.AppendLine("from  wcs.vrpt_shelf_list");
                    Sql.AppendLine("order by shelf_no asc");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
             


                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MudBlazor.Color stColor = new MudBlazor.Color();

                        switch (rdr["st_desc"].ToString())
                        {
                            case "ไม่ทราบสถานะ":
                                stColor = MudBlazor.Color.Dark;
                                break;
                            case "ว่างไม่มีพาเลท":
                                stColor = MudBlazor.Color.Default;
                                break;
                            case "มีพาเลทสินค้า":
                                stColor = MudBlazor.Color.Success;
                                break;
                            case "หยุดการใช้งาน":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "จองรอเก็บสินค้า":
                                stColor = MudBlazor.Color.Primary;
                                break;
                            case "จองรอจ่ายสินค้า":
                                stColor = MudBlazor.Color.Secondary;
                                break;
                            case "มีพาเลทไม่มีสินค้า":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "จองรอย้ายสินค้า":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "ตำแหน่งผิดปกติ":
                                stColor = MudBlazor.Color.Error;
                                break;
                        }


                        Vrpt_shelf_listInfo objrd = new Vrpt_shelf_listInfo
                        {
                            Srm_no = rdr["srm_no"] == DBNull.Value ? null : (Int32?)rdr["srm_no"],
                            Shelf_no = rdr["shelf_no"] == DBNull.Value ? null : (Int32?)rdr["shelf_no"],
                            Shelfcode = rdr["shelfcode"].ToString(),
                            Shelfname = rdr["shelfname"].ToString(),
                            Shelfbank = rdr["shelfbank"] == DBNull.Value ? null : (Int16?)rdr["shelfbank"],
                            Shelfbay = rdr["shelfbay"] == DBNull.Value ? null : (Int32?)rdr["shelfbay"],
                            Shelfframe = rdr["shelfframe"] == DBNull.Value ? null : (Int16?)rdr["shelfframe"],
                            Shelflevel = rdr["shelflevel"] == DBNull.Value ? null : (Int16?)rdr["shelflevel"],
                            Shelfstatus = rdr["shelfstatus"] == DBNull.Value ? null : (Int32?)rdr["shelfstatus"],
                            Lpncode = rdr["lpncode"].ToString(),
                            Refercode = rdr["refercode"].ToString(),
                            Actual_weight = rdr["actual_weight"] == DBNull.Value ? null : (decimal?)rdr["actual_weight"],
                            Actual_size = rdr["actual_size"] == DBNull.Value ? null : (Int32?)rdr["actual_size"],
                            Desc_size = rdr["desc_size"].ToString(),
                            St_desc = rdr["st_desc"].ToString(),
                            St_Color = stColor,
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"]
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


        public IEnumerable<Vrpt_shelf_listInfo> GetShelfLocationCuRoom()
        {
            List<Vrpt_shelf_listInfo> lstobj = new List<Vrpt_shelf_listInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT modified, srm_no, shelf_no, shelfcode, shelfname");
                    Sql.AppendLine(", shelfbank, shelfframe, shelfbay, shelflevel, shelfstatus");
                    Sql.AppendLine(", lpncode, refercode, actual_weight, actual_size, desc_size, st_desc");
                    Sql.AppendLine("from  wms.vrpt_shelf_list");
                    Sql.AppendLine("order by shelf_no asc");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MudBlazor.Color stColor = new MudBlazor.Color();

                        switch (rdr["st_desc"].ToString())
                        {
                            case "ไม่ทราบสถานะ":
                                stColor = MudBlazor.Color.Dark;
                                break;
                            case "ว่างไม่มีพาเลท":
                                stColor = MudBlazor.Color.Default;
                                break;
                            case "มีพาเลทสินค้า":
                                stColor = MudBlazor.Color.Success;
                                break;
                            case "หยุดการใช้งาน":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "จองรอเก็บสินค้า":
                                stColor = MudBlazor.Color.Primary;
                                break;
                            case "จองรอจ่ายสินค้า":
                                stColor = MudBlazor.Color.Secondary;
                                break;
                            case "มีพาเลทไม่มีสินค้า":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "จองรอย้ายสินค้า":
                                stColor = MudBlazor.Color.Warning;
                                break;
                            case "ตำแหน่งผิดปกติ":
                                stColor = MudBlazor.Color.Error;
                                break;
                        }


                        Vrpt_shelf_listInfo objrd = new Vrpt_shelf_listInfo
                        {
                            Srm_no = rdr["srm_no"] == DBNull.Value ? null : (Int32?)rdr["srm_no"],
                            Shelf_no = rdr["shelf_no"] == DBNull.Value ? null : (Int32?)rdr["shelf_no"],
                            Shelfcode = rdr["shelfcode"].ToString(),
                            Shelfname = rdr["shelfname"].ToString(),
                            Shelfbank = rdr["shelfbank"] == DBNull.Value ? null : (Int16?)rdr["shelfbank"],
                            Shelfbay = rdr["shelfbay"] == DBNull.Value ? null : (Int32?)rdr["shelfbay"],
                            Shelfframe = rdr["shelfframe"] == DBNull.Value ? null : (Int16?)rdr["shelfframe"],
                            Shelflevel = rdr["shelflevel"] == DBNull.Value ? null : (Int16?)rdr["shelflevel"],
                            Shelfstatus = rdr["shelfstatus"] == DBNull.Value ? null : (Int32?)rdr["shelfstatus"],
                            Lpncode = rdr["lpncode"].ToString(),
                            Refercode = rdr["refercode"].ToString(),
                            Actual_weight = rdr["actual_weight"] == DBNull.Value ? null : (decimal?)rdr["actual_weight"],
                            Actual_size = rdr["actual_size"] == DBNull.Value ? null : (Int32?)rdr["actual_size"],
                            Desc_size = rdr["desc_size"].ToString(),
                            St_desc = rdr["st_desc"].ToString(),
                            St_Color = stColor,
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"]
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

        public async Task<IEnumerable<Inv_Stock_GoInfo>> GetStockListInfo()
        {

            List<Inv_Stock_GoInfo> lstobj = new List<Inv_Stock_GoInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                StringBuilder Sql = new StringBuilder();

                Sql.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                Sql.AppendLine(", pono, pallettag, itemtag, itemcode, itemname, itembar, unit");
                Sql.AppendLine(", weightunit, quantity, weight, lotno, totalquantity, totalweight");
                Sql.AppendLine(", docno, docby, docdate, docnote, grnrefer, grntime, grtime");
                Sql.AppendLine(", grtype, pallteno, palltmapkey, storagetime, storageno");
                Sql.AppendLine(", storagearea, storagebin, gnrefer, allocatequantity, allocateweight");
                Sql.AppendLine(",CAST (AGE(now()::timestamp(0), storagetime::timestamp(0)) as text) as ages");
                Sql.AppendLine(",TRUNC(EXTRACT(EPOCH FROM AGE(now()::timestamp(0), storagetime::timestamp(0))/60)) as ageminutes");
                Sql.AppendLine("FROM wms.inv_stock_go");
                Sql.AppendLine("WHERE allocatequantity < quantity");
                //Sql.AppendLine("AND  storagearea in (@storagearea1)");
                Sql.AppendLine("order by itemcode ASC, docdate ASC, pallettag ASC");


                NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter<string>("@storagearea1", "ASRS"));

                con.Open();
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (await rdr.ReadAsync())
                {
                    Inv_Stock_GoInfo objrd = new Inv_Stock_GoInfo
                    {
                        Efidx = rdr["efidx"] == DBNull.Value ? null : (Int64?)rdr["efidx"],
                        Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                        Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                        Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                        Innovator = rdr["innovator"] == DBNull.Value ? null : (Int64?)rdr["innovator"],
                        Device = rdr["device"].ToString(),
                        Pono = rdr["pono"].ToString(),
                        Pallettag = rdr["pallettag"].ToString(),
                        Itemtag = rdr["itemtag"].ToString(),
                        Itemcode = rdr["itemcode"].ToString(),
                        Itemname = rdr["itemname"].ToString(),
                        Itembar = rdr["itembar"].ToString(),
                        Unit = rdr["unit"].ToString(),
                        Weightunit = rdr["weightunit"].ToString(),
                        Quantity = rdr["quantity"] == DBNull.Value ? null : (decimal?)rdr["quantity"],
                        Weight = rdr["weight"] == DBNull.Value ? null : (decimal?)rdr["weight"],
                        Lotno = rdr["lotno"].ToString(),
                        Totalquantity = rdr["totalquantity"] == DBNull.Value ? null : (decimal?)rdr["totalquantity"],
                        Totalweight = rdr["totalweight"] == DBNull.Value ? null : (decimal?)rdr["totalweight"],
                        Docno = rdr["docno"].ToString(),
                        Docby = rdr["docby"].ToString(),
                        Docdate = rdr["docdate"] == DBNull.Value ? null : (DateTime?)rdr["docdate"],
                        Docnote = rdr["docnote"].ToString(),
                        Grnrefer = rdr["grnrefer"] == DBNull.Value ? null : (Int64?)rdr["grnrefer"],
                        Grntime = rdr["grntime"] == DBNull.Value ? null : (DateTime?)rdr["grntime"],
                        Grtime = rdr["grtime"] == DBNull.Value ? null : (DateTime?)rdr["grtime"],
                        Grtype = rdr["grtype"].ToString(),
                        Pallteno = rdr["pallteno"].ToString(),
                        Palltmapkey = rdr["palltmapkey"].ToString(),
                        Storagetime = rdr["storagetime"] == DBNull.Value ? null : (DateTime?)rdr["storagetime"],
                        Storageno = rdr["storageno"].ToString(),
                        Storagearea = rdr["storagearea"].ToString(),
                        Storagebin = rdr["storagebin"].ToString(),
                        Gnrefer = rdr["gnrefer"] == DBNull.Value ? null : (Int64?)rdr["gnrefer"],
                        Allocatequantity = rdr["allocatequantity"] == DBNull.Value ? null : (decimal?)rdr["allocatequantity"],
                        Allocateweight = rdr["allocateweight"] == DBNull.Value ? null : (decimal?)rdr["allocateweight"],
                        AgeTimes = rdr["ages"].ToString(),
                        AgeMinutes = rdr["ageminutes"] == DBNull.Value ? null : (double?)rdr["ageminutes"]
                    };
                    lstobj.Add(objrd);
                }
                con.Close();
            }
            return lstobj;
        }

        public async Task UpdateHoldStock(List<InvStockList> liststock)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Update wms.inv_stock_go");
                sql.AppendLine("Set efstatus = @efstatus");
                sql.AppendLine("Where pallettag in (");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                int iState = -1;
                string sParamState = "@efstatus";

                cmd.Parameters.Add(new NpgsqlParameter<int>(sParamState, iState));

                var i = 0;
                foreach (var s in liststock)
                {
                    if (i != 0) sql.Append(",");
                    var pallettag = "pallettag" + i.ToString();


                    sql.Append("@").Append(pallettag);

                    cmd.Parameters.Add(new NpgsqlParameter<string>(pallettag, s.Su_no));


                    i++;
                }
                sql.AppendLine(")");

                con.Open();
                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();

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

        public async Task UpdateReleaseStock(List<InvStockList> liststock)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Update wms.inv_stock_go");
                sql.AppendLine("Set efstatus = @efstatus");
                sql.AppendLine("Where pallettag in (");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                int iState = 0;
                string sParamState = "@efstatus";

                cmd.Parameters.Add(new NpgsqlParameter<int>(sParamState, iState));

                var i = 0;
                foreach (var s in liststock)
                {
                    if (i != 0) sql.Append(",");
                    var pallettag = "pallettag" + i.ToString();


                    sql.Append("@").Append(pallettag);

                    cmd.Parameters.Add(new NpgsqlParameter<string>(pallettag, s.Su_no));


                    i++;
                }
                sql.AppendLine(")");

                con.Open();
                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();

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

        public async Task ClearRack(Int32 iStatus, string sPallet, string sBin)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Update wms.mas_storagebin_go");
                sql.AppendLine("Set efstatus = @efstatus");
                sql.AppendLine(", pallletno = null");
                sql.AppendLine(", modified = now()");
                sql.AppendLine("Where binno = @binno") ;
                sql.AppendLine(";");

                sql.AppendLine("Delete from wms.inv_stock_go");
                sql.AppendLine("Where pallteno = @pallteno");
                sql.AppendLine("and storagebin = @storagebin");
                sql.AppendLine("and storagearea <> @storagearea");
                sql.AppendLine(";");

                sql.AppendLine("Delete from public.sap_stock");
                sql.AppendLine("Where palletcode = @palletcode");
                sql.AppendLine(";");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                int iState = iStatus;
                string sParamState = "@efstatus";
                string sParamBin = "@binno";
                string sPallteno = "@pallteno";
                string sStoragebin = "@storagebin";
                string sStoragearea = "@storagearea";
                string sPalletcode = "@palletcode";

                cmd.Parameters.Add(new NpgsqlParameter<int>(sParamState, iState));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sParamBin, sBin));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sPallteno, sPallet));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sStoragebin, sBin));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sStoragearea, "ASRS"));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sPalletcode, sPallet));

                con.Open();
                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();

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

        public async Task ClearStockCuroom(Int32 iStatus, string sPallet, string sBin)
        {

            Int32? iRet = 0;
            string sRet = "Calling";
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Call wms.poc_inv_claerstockcuroom(");
                sql.AppendLine("@spalletno, @sbinno ,@retchk,@retmsg)");
                NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@spalletno", sPallet);
                cmd.Parameters.AddWithValue("@sbinno", sBin);
                cmd.Parameters.AddWithValue("@retchk", iRet);
                cmd.Parameters.AddWithValue("@retmsg", sRet);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    iRet = rdr["retchk"] == DBNull.Value ? null : (Int32?)rdr["retchk"];
                    sRet = rdr["retmsg"].ToString();
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


        public async Task BookkingRack(string sPallet, string sBin)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Update wms.mas_storagebin_go");
                sql.AppendLine("Set efstatus = @efstatus");
                sql.AppendLine(", pallletno = null");
                sql.AppendLine(", modified = now()");
                sql.AppendLine("Where binno = @binno");

                using var cmd = new NpgsqlCommand(connection: con, cmdText: null);

                int iState = 6;
                string sParamState = "@efstatus";
                string sParamBin = "@binno";

                cmd.Parameters.Add(new NpgsqlParameter<int>(sParamState, iState));
                cmd.Parameters.Add(new NpgsqlParameter<string>(sParamBin, sBin));



                con.Open();
                cmd.CommandText = sql.ToString();
                await cmd.ExecuteNonQueryAsync();

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
}
