using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models.Oub;
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
                    sqlQurey.AppendLine("where batch_no = :batch_no ;");

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



    }
}
