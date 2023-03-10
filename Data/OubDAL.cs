using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models.Oub;
using GoWMS.Server.Models.Public;
using NpgsqlTypes;
using System.Text;
using Serilog;

namespace GoWMS.Server.Data
{
    public class OubDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnLocalDBPG();

        public IEnumerable<Sap_Storeout> GetAllSapStoreout()
        {
            List<Sap_Storeout> lstobj = new List<Sap_Storeout>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM public.sap_storeout");
                    sql.AppendLine("WHERE status < @status");
                    sql.AppendLine("ORDER BY idx");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@status", 3);

                    con.Open();

                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Sap_Storeout objrd = new Sap_Storeout
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (Int32?)rdr["entity_lock"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_Id"] == DBNull.Value ? null : (Int64?)rdr["client_Id"],
                            Client_Ip = rdr["client_Ip"].ToString(),
                            Order_No = rdr["order_no"].ToString(),
                            Ship_To_Code = rdr["ship_to_code"].ToString(),
                            Ship_Name = rdr["ship_name"].ToString(),
                            Delivery_Priority = rdr["delivery_priority"] == DBNull.Value ? null : (Int32?)rdr["delivery_priority"],
                            Delivery_Date = rdr["delivery_date"] == DBNull.Value ? null : (DateTime?)rdr["delivery_date"],
                            Item_Code = rdr["item_code"].ToString(),
                            Batch_Number = rdr["batch_number"].ToString(),
                            Request_Qty = rdr["request_qty"] == DBNull.Value ? null : (decimal?)rdr["request_qty"],
                            Status = rdr["status"] == DBNull.Value ? null : (Int32?)rdr["status"],
                            Error_Code = rdr["error_code"].ToString(),
                            Movement_Type = rdr["movement_type"].ToString(),
                            Movement_Reason = rdr["movement_reason"].ToString(),
                            To_No = rdr["to_no"].ToString(),
                            To_Line = rdr["to_line"].ToString(),
                            Po_Header_Txt = rdr["po_header_txt"].ToString(),
                            Requisitioner = rdr["requisitioner"].ToString(),
                            Po_No = rdr["po_no"].ToString(),
                            Remark = rdr["remark"].ToString(),
                            Doc_Ref = rdr["doc_ref"].ToString(),
                            Created_By = rdr["created_by"].ToString(),
                            Created_Date = rdr["created_date"] == DBNull.Value ? null : (DateTime?)rdr["created_date"],
                            Update_By = rdr["update_by"].ToString(),
                            Update_Date = rdr["update_date"] == DBNull.Value ? null : (DateTime?)rdr["update_date"],
                            Order_Line = rdr["order_line"].ToString(),
                            Stock_Consign = rdr["stock_consign"].ToString(),
                            Site = rdr["site"].ToString(),
                            Storage_Location = rdr["storage_location"].ToString(),
                            Warehouse = rdr["warehouse"].ToString(),
                            Item_Name = rdr["item_Name"].ToString(),
                            Tracking_Number = rdr["tracking_number"].ToString(),
                            Su_No = rdr["su_no"].ToString(),
                            Pallet_No = rdr["pallet_no"].ToString(),
                            Stock_Qty = rdr["stock_qty"] == DBNull.Value ? null : (decimal?)rdr["stock_qty"],
                            Transfer_Qty = rdr["transfer_qty"] == DBNull.Value ? null : (decimal?)rdr["transfer_qty"],
                            Ref_No = rdr["ref_no"].ToString(),
                            Ref_Line = rdr["ref_line"].ToString(),
                            Unit = rdr["unit"].ToString(),
                            Vendor_Code = rdr["vendor_code"].ToString(),
                            Batch_No = rdr["batch_no"].ToString()
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

        public IEnumerable<Sap_Storeout> GetSapStoreoutSetBatch()
        {
            List<Sap_Storeout> lstobj = new List<Sap_Storeout>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM public.sap_storeout");
                    sql.AppendLine("WHERE status = @status");
                    sql.AppendLine("ORDER BY idx");

                    NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@status", 0);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Sap_Storeout objrd = new Sap_Storeout
                        {
                            Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                            Entity_Lock = rdr["entity_lock"] == DBNull.Value ? null : (Int32?)rdr["entity_lock"],
                            Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                            Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                            Client_Id = rdr["client_id"] == DBNull.Value ? null : (Int64?)rdr["client_Id"],
                            Client_Ip = rdr["client_ip"].ToString(),
                            Order_No = rdr["order_no"].ToString(),
                            Ship_To_Code = rdr["ship_to_code"].ToString(),
                            Ship_Name = rdr["ship_name"].ToString(),
                            Delivery_Priority = rdr["delivery_priority"] == DBNull.Value ? null : (Int32?)rdr["delivery_priority"],
                            Delivery_Date = rdr["delivery_date"] == DBNull.Value ? null : (DateTime?)rdr["delivery_date"],
                            Item_Code = rdr["item_code"].ToString(),
                            Batch_Number = rdr["batch_number"].ToString(),
                            Request_Qty = rdr["request_qty"] == DBNull.Value ? null : (decimal?)rdr["request_qty"],
                            Status = rdr["status"] == DBNull.Value ? null : (Int32?)rdr["status"],
                            Error_Code = rdr["error_code"].ToString(),
                            Movement_Type = rdr["movement_type"].ToString(),
                            Movement_Reason = rdr["movement_reason"].ToString(),
                            To_No = rdr["to_no"].ToString(),
                            To_Line = rdr["to_line"].ToString(),
                            Po_Header_Txt = rdr["po_header_txt"].ToString(),
                            Requisitioner = rdr["requisitioner"].ToString(),
                            Po_No = rdr["po_no"].ToString(),
                            Remark = rdr["remark"].ToString(),
                            Doc_Ref = rdr["doc_ref"].ToString(),
                            Created_By = rdr["created_by"].ToString(),
                            Created_Date = rdr["created_date"] == DBNull.Value ? null : (DateTime?)rdr["created_date"],
                            Update_By = rdr["update_by"].ToString(),
                            Update_Date = rdr["update_date"] == DBNull.Value ? null : (DateTime?)rdr["update_date"],
                            Order_Line = rdr["order_line"].ToString(),
                            Stock_Consign = rdr["stock_consign"].ToString(),
                            Site = rdr["site"].ToString(),
                            Storage_Location = rdr["storage_location"].ToString(),
                            Warehouse = rdr["warehouse"].ToString(),
                            Item_Name = rdr["item_Name"].ToString(),
                            Tracking_Number = rdr["tracking_number"].ToString(),
                            Su_No = rdr["su_no"].ToString(),
                            Pallet_No = rdr["pallet_no"].ToString(),
                            Stock_Qty = rdr["stock_qty"] == DBNull.Value ? null : (decimal?)rdr["stock_qty"],
                            Transfer_Qty = rdr["transfer_qty"] == DBNull.Value ? null : (decimal?)rdr["transfer_qty"],
                            Ref_No = rdr["ref_no"].ToString(),
                            Ref_Line = rdr["ref_line"].ToString(),
                            Unit = rdr["unit"].ToString(),
                            Vendor_Code = rdr["vendor_code"].ToString(),
                            Batch_No = rdr["batch_no"].ToString()
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

        public string GetRunnning(string sSeq, Int32 iPad)
        {
            string sRunning = "";
            Int32? iRet =0 ;
            
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retrunning from public.fuc_create_running(:sSeq , :iPad);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":sSeq", sSeq);
                    cmd.Parameters.AddWithValue(":iPad", iPad);

                    con.Open();
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        iRet = rdr["_retchk"] == DBNull.Value ? null : (Int32?)rdr["_retchk"];
                        sRunning = rdr["_retrunning"].ToString();
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
            return sRunning;
        }

        public Boolean CreateBatchOrder(DateTime deliverydate,Int32 deliveryprio,string orderno, string shiptocode, string sSeq)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from public.fuc_create_batchorder(:deliverydate , :deliveryprio, :orderno, :shiptocode, :sSeq);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":deliverydate", NpgsqlDbType.Timestamp, deliverydate);
                    cmd.Parameters.AddWithValue(":deliveryprio", NpgsqlDbType.Integer, deliveryprio);
                    cmd.Parameters.AddWithValue(":orderno", NpgsqlDbType.Varchar, orderno);
                    cmd.Parameters.AddWithValue(":shiptocode", NpgsqlDbType.Varchar, shiptocode);
                    cmd.Parameters.AddWithValue(":sSeq", NpgsqlDbType.Varchar, sSeq);

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

                if (iRet==1)
                {
                    bRet = true;
                }
            }
            return bRet;
        }

        public Boolean CreateBatchOrder_curoom(DateTime deliverydate, Int32 deliveryprio, string orderno, string shiptocode, string sSeq)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from public.fuc_create_batchorder_curoom(:deliverydate , :deliveryprio, :orderno, :shiptocode, :sSeq);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":deliverydate", NpgsqlDbType.Timestamp, deliverydate);
                    cmd.Parameters.AddWithValue(":deliveryprio", NpgsqlDbType.Integer, deliveryprio);
                    cmd.Parameters.AddWithValue(":orderno", NpgsqlDbType.Varchar, orderno);
                    cmd.Parameters.AddWithValue(":shiptocode", NpgsqlDbType.Varchar, shiptocode);
                    cmd.Parameters.AddWithValue(":sSeq", NpgsqlDbType.Varchar, sSeq);

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

        public Boolean CreateBatchSetting (string sSeq, Int32 istation)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from public.fuc_create_batchsetting(:sSeq , :istation);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue(":sSeq", NpgsqlDbType.Varchar, sSeq);
                    cmd.Parameters.AddWithValue(":istation", NpgsqlDbType.Integer, istation);

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

        public Boolean StartBatchsetting(string sSeq, Int32 istation)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from public.fuc_start_batchsetting(:sSeq , :istation);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":sSeq", NpgsqlDbType.Varchar, sSeq);
                    cmd.Parameters.AddWithValue(":istation", NpgsqlDbType.Integer, istation);

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


        public Boolean StartBatchsetting_curoom(string sSeq, Int32 istation)
        {
            Boolean bRet = false;
            string sRet = "";
            Int32? iRet = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("select _retchk, _retmsg from public.fuc_start_batchsetting_curoom(:sSeq , :istation);");
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":sSeq", NpgsqlDbType.Varchar, sSeq);
                    cmd.Parameters.AddWithValue(":istation", NpgsqlDbType.Integer, istation);

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

        public Boolean RollbackBatch(string sSeq)
        {
            Boolean bRet = false;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("delete from public.sap_batchsetting where batch_no = :batch_no1 ;");
                    sqlQurey.AppendLine("delete from public.sap_batchorder where batch_no = :batch_no2 ;");

                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":batch_no1", NpgsqlDbType.Varchar, sSeq);
                    cmd.Parameters.AddWithValue(":batch_no2", NpgsqlDbType.Varchar, sSeq);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    bRet = true;
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
            return bRet;
        }

        public Boolean SetDestinationAGV(string batch_no, string station)
        {
            Boolean bRet = false;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder sqlQurey = new StringBuilder();
                    sqlQurey.AppendLine("update public.sap_storeout");
                    sqlQurey.AppendLine("set  requisitioner = :requisitioner");
                    sqlQurey.AppendLine("where order_no = :batch_no ;");

                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.AddWithValue(":requisitioner", NpgsqlDbType.Varchar, station);
                    cmd.Parameters.AddWithValue(":batch_no", NpgsqlDbType.Varchar, batch_no);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    bRet = true;
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
            return bRet;
        }

        public IEnumerable<Sap_StoreoutInfo> GetPicking(string sPallet)
        {
            List<Sap_StoreoutInfo> lstModels = new List<Sap_StoreoutInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                StringBuilder sqlQurey = new StringBuilder();

                sqlQurey.AppendLine("SELECT row_number() over(order by t1.item_code asc) AS rn, t1.idx, ");
                sqlQurey.AppendLine("t1.item_code, t1.item_name, t1.request_qty, t1.unit, t1.su_no, ");
                sqlQurey.AppendLine("t1.pallet_no, t1.stock_qty, t1.transfer_qty, t1.movement_type ");
                sqlQurey.AppendLine("from public.sap_storeout t1");
                sqlQurey.AppendLine("WHERE (status = @status)");
                sqlQurey.AppendLine("AND t1.pallet_no = @pallet_no");
                sqlQurey.AppendLine("ORDER BY t1.item_code asc ");
                sqlQurey.AppendLine(";");

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                cmd.Parameters.AddWithValue("@status", NpgsqlDbType.Integer, 3);
                cmd.Parameters.AddWithValue("@pallet_no", NpgsqlDbType.Varchar, sPallet);

                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Sap_StoreoutInfo listRead = new Sap_StoreoutInfo
                    {
                        Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                        Bcount = false,
                        Item_Code = rdr["item_code"].ToString(),
                        Item_Name = rdr["item_name"].ToString(),
                        Request_Qty = rdr["request_qty"] == DBNull.Value ? null : (decimal?)rdr["request_qty"],
                        Unit = rdr["unit"].ToString(),
                        Su_No = rdr["su_no"].ToString(),
                        Pallet_No = rdr["pallet_no"].ToString(),
                        Stock_Qty = rdr["stock_qty"] == DBNull.Value ? null : (decimal?)rdr["stock_qty"],
                        Transfer_Qty = rdr["transfer_qty"] == DBNull.Value ? null : (decimal?)rdr["transfer_qty"],
                        Movement_Type = rdr["movement_type"].ToString()
                    };
                    lstModels.Add(listRead);
                }
                con.Close();
            }
            return lstModels;
        }

        public IEnumerable<Sap_StoreoutInfo> GetPickingID(string SPackID)
        {
            List<Sap_StoreoutInfo> lstModels = new List<Sap_StoreoutInfo>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                StringBuilder sqlQurey = new StringBuilder();

                sqlQurey.AppendLine("SELECT row_number() over(order by t1.item_code asc) AS rn, t1.idx, ");
                sqlQurey.AppendLine("t1.item_code, t1.item_name, t1.request_qty, t1.unit, t1.su_no, ");
                sqlQurey.AppendLine("t1.pallet_no, t1.stock_qty, t1.transfer_qty, t1.movement_type ");
                sqlQurey.AppendLine("from public.sap_storeout t1");
                sqlQurey.AppendLine("WHERE (status = @status)");
                sqlQurey.AppendLine("AND t1.su_no = @su_no");
                sqlQurey.AppendLine("ORDER BY t1.item_code asc ");
                sqlQurey.AppendLine(";");

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                cmd.Parameters.AddWithValue("@status", NpgsqlDbType.Integer, 3);
                cmd.Parameters.AddWithValue("@su_no", NpgsqlDbType.Varchar, SPackID);

                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Sap_StoreoutInfo listRead = new Sap_StoreoutInfo
                    {
                        Idx = rdr["idx"] == DBNull.Value ? null : (Int64?)rdr["idx"],
                        Bcount = false,
                        Item_Code = rdr["item_code"].ToString(),
                        Item_Name = rdr["item_name"].ToString(),
                        Request_Qty = rdr["request_qty"] == DBNull.Value ? null : (decimal?)rdr["request_qty"],
                        Unit = rdr["unit"].ToString(),
                        Su_No = rdr["su_no"].ToString(),
                        Pallet_No = rdr["pallet_no"].ToString(),
                        Stock_Qty = rdr["stock_qty"] == DBNull.Value ? null : (decimal?)rdr["stock_qty"],
                        Transfer_Qty = rdr["transfer_qty"] == DBNull.Value ? null : (decimal?)rdr["transfer_qty"],
                        Movement_Type = rdr["movement_type"].ToString()
                    };
                    lstModels.Add(listRead);
                }
                con.Close();
            }
            return lstModels;
        }

        public Boolean ValidateDeliveryList(string spack)
        {
            Boolean bret = false;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("select *");
                    Sql.AppendLine("from wms.oub_deliveryorder_go");
                    Sql.AppendLine("where package_id = @package_id");

                    NpgsqlCommand cmd = new NpgsqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();

                    cmd.Parameters.AddWithValue("@package_id", NpgsqlDbType.Varchar, spack);

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

        public IEnumerable<Oub_Delivery_Go> GetDeliveryAll()
        {
            List<Oub_Delivery_Go> lstModels = new List<Oub_Delivery_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                StringBuilder sqlQurey = new StringBuilder();

                sqlQurey.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                sqlQurey.AppendLine(", package_id, roll_id, material_code, material_description ");
                sqlQurey.AppendLine(", matelement, quantity, picked, unit, wh_code, warehouse");
                sqlQurey.AppendLine(", locationno, job, job_code, customer_code, customer_description");
                sqlQurey.AppendLine(", finished_product, finished_product_description");
                sqlQurey.AppendLine(", mo_barcode, dotype, apiid, toagvstation, refuuid");
                sqlQurey.AppendLine(", pallet_no, stockqty, deliveryto");
                sqlQurey.AppendLine("FROM wms.oub_delivery_go");
                //sqlQurey.AppendLine("WHERE pallet_no = @pallet_no");
                sqlQurey.AppendLine("ORDER BY package_id ASC");
                sqlQurey.AppendLine(";");

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                //cmd.Parameters.AddWithValue("@pallet_no", NpgsqlDbType.Varchar, sPallet);

                con.Open();


                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Oub_Delivery_Go listRead = new Oub_Delivery_Go
                    {

                        Efidx = rdr["efidx"] == DBNull.Value ? null : (long?)rdr["efidx"],
                        Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                        Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                        Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                        Innovator = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                        Device = rdr["device"].ToString(),
                        Package_Id = rdr["package_id"].ToString(),
                        Roll_Id = rdr["roll_id"].ToString(),
                        Material_Code = rdr["material_code"].ToString(),
                        Material_Description = rdr["material_description"].ToString(),
                        Matelement = rdr["matelement"].ToString(),
                        Quantity = rdr["quantity"] == DBNull.Value ? null : (Decimal?)rdr["quantity"],
                        Picked = rdr["picked"] == DBNull.Value ? null : (Decimal?)rdr["picked"],
                        Unit = rdr["unit"].ToString(),
                        Wh_Code = rdr["wh_code"].ToString(),
                        Warehouse = rdr["warehouse"].ToString(),
                        Locationno = rdr["locationno"].ToString(),
                        Job = rdr["job"].ToString(),
                        Job_Code = rdr["job_code"].ToString(),
                        Customer_Code = rdr["customer_code"].ToString(),
                        Customer_Description = rdr["customer_description"].ToString(),
                        Finished_Product = rdr["finished_product"].ToString(),
                        Finished_Product_Description = rdr["finished_product_description"].ToString(),
                        Mo_Barcode = rdr["mo_barcode"].ToString(),
                        Dotype = rdr["dotype"].ToString(),
                        Apiid = rdr["apiid"] == DBNull.Value ? null : (long?)rdr["apiid"],
                        Toagvstation = rdr["toagvstation"] == DBNull.Value ? null : (long?)rdr["toagvstation"],
                        Refuuid = rdr["refuuid"].ToString(),
                        Pallet_No = rdr["pallet_no"].ToString(),
                        Stockqty = rdr["stockqty"] == DBNull.Value ? null : (Decimal?)rdr["stockqty"],
                        Deliveryto = rdr["deliveryto"].ToString()
                    };
                    lstModels.Add(listRead);
                }
                con.Close();
            }
            return lstModels;
        }


        public IEnumerable<Oub_Delivery_Go> GetDeliveryList(string sPallet)
        {
            List<Oub_Delivery_Go> lstModels = new List<Oub_Delivery_Go>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                StringBuilder sqlQurey = new StringBuilder();

                sqlQurey.AppendLine("SELECT efidx, efstatus, created, modified, innovator, device");
                sqlQurey.AppendLine(", package_id, roll_id, material_code, material_description ");
                sqlQurey.AppendLine(", matelement, quantity, picked, unit, wh_code, warehouse");
                sqlQurey.AppendLine(", locationno, job, job_code, customer_code, customer_description");
                sqlQurey.AppendLine(", finished_product, finished_product_description");
                sqlQurey.AppendLine(", mo_barcode, dotype, apiid, toagvstation, refuuid");
                sqlQurey.AppendLine(", pallet_no, stockqty, deliveryto");
                sqlQurey.AppendLine("FROM wms.oub_delivery_go");
                sqlQurey.AppendLine("WHERE pallet_no = @pallet_no");
                sqlQurey.AppendLine("ORDER BY package_id ASC");
                sqlQurey.AppendLine(";");

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQurey.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@pallet_no", NpgsqlDbType.Varchar, sPallet);

                con.Open();
              

                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Oub_Delivery_Go listRead = new Oub_Delivery_Go
                    {
            
                        Efidx = rdr["efidx"] == DBNull.Value ? null : (long?)rdr["efidx"],
                        Efstatus = rdr["efstatus"] == DBNull.Value ? null : (Int32?)rdr["efstatus"],
                        Created = rdr["created"] == DBNull.Value ? null : (DateTime?)rdr["created"],
                        Modified = rdr["modified"] == DBNull.Value ? null : (DateTime?)rdr["modified"],
                        Innovator = rdr["innovator"] == DBNull.Value ? null : (long?)rdr["innovator"],
                        Device = rdr["device"].ToString(),
                        Package_Id = rdr["package_id"].ToString(),
                        Roll_Id = rdr["roll_id"].ToString(),
                        Material_Code = rdr["material_code"].ToString(),
                        Material_Description = rdr["material_description"].ToString(),
                        Matelement = rdr["matelement"].ToString(),
                        Quantity  = rdr["quantity"] == DBNull.Value ? null : (Decimal?)rdr["quantity"],
                        Picked = rdr["picked"] == DBNull.Value ? null : (Decimal?)rdr["picked"],
                        Unit = rdr["unit"].ToString(),
                        Wh_Code = rdr["wh_code"].ToString(),
                        Warehouse = rdr["warehouse"].ToString(),
                        Locationno = rdr["locationno"].ToString(),
                        Job = rdr["job"].ToString(),
                        Job_Code =  rdr["job_code"].ToString(),
                        Customer_Code = rdr["customer_code"].ToString(),
                        Customer_Description = rdr["customer_description"].ToString(),
                        Finished_Product = rdr["finished_product"].ToString(),
                        Finished_Product_Description = rdr["finished_product_description"].ToString(),
                        Mo_Barcode = rdr["mo_barcode"].ToString(),
                        Dotype = rdr["dotype"].ToString(),
                        Apiid = rdr["apiid"] == DBNull.Value ? null : (long?)rdr["apiid"],
                        Toagvstation = rdr["toagvstation"] == DBNull.Value ? null : (long?)rdr["toagvstation"],
                        Refuuid = rdr["refuuid"].ToString(),
                        Pallet_No = rdr["pallet_no"].ToString(),
                        Stockqty = rdr["stockqty"] == DBNull.Value ? null : (Decimal?)rdr["stockqty"],
                        Deliveryto = rdr["deliveryto"].ToString()
                    };
                    lstModels.Add(listRead);
                }
                con.Close();
            }
            return lstModels;
        }

        public void SetConsolMappallet(string pallet, string packid)
        {
            Int32? iRet = 0;
            string sRet = "Calling";
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("Call wms.poc_oub_mappalletconsol(");
                sql.AppendLine("@spalletno, @spackid, @retchk, @retmsg)");
                NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@spalletno", pallet);
                cmd.Parameters.AddWithValue("@spackid", packid);
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

        public void CancelConsolBypack(string pack)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete from wms.oub_delivery_go");
                sql.AppendLine("Where package_id = @Pack ");

                NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

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

        public void CancelConsolByID(Int64 pack)
        {
            using NpgsqlConnection con = new NpgsqlConnection(connectionString);
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete from wms.oub_delivery_go");
                sql.AppendLine("Where efidx = @efidx ");

                NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@efidx", pack);
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


    }
}
