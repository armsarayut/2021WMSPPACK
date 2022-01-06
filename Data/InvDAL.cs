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
                    Sql.AppendLine("select row_number() over(order by  itemcode asc) AS rn,");
                    Sql.AppendLine("itemcode, itemname, quantity, pallettag, pallteno, storagearea, storagebin");
                    Sql.AppendLine("from wms.inv_stock_go ");
                    Sql.AppendLine("order by itemcode");

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
                            StorageArae = rdr["storagearea"].ToString()
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

                    Sql.AppendLine("select row_number() over(order by itemcode asc) AS rn,");
                    Sql.AppendLine("itemcode, itemname, sum(quantity) as totalstock, count(pallteno) as countpallet");
                    Sql.AppendLine("from wms.inv_stock_go ");
                    Sql.AppendLine("group by itemcode, itemname");
                    Sql.AppendLine("order by itemcode");

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
                            St_Color = stColor
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
