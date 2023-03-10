using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Controllers;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Erp;
using GoWMS.Server.Models.Api;
using GoWMS.Server.Models.Inb;
using Serilog;
using System.Text;

namespace GoWMS.Server.Data
{
    public class ErpDAL
    {
        readonly private string connectionString = ConnGlobals.GetConnErpDB();

        public IEnumerable<V_Receiving_OrdersInfo> GetAllErpReceivingOrders()
        {
            List<V_Receiving_OrdersInfo> lstobj = new List<V_Receiving_OrdersInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Package_ID,Roll_ID " +
                    ",Material_Code,Material_Description" +
                    ",Receiving_Date,GR_Quantity" +
                    ",Unit,GR_Quantity_KG" +
                    ",WH_Code,Warehouse" +
                    ",Location,Document_Number" +
                    ",Job,Job_Code " +
                    "FROM dbo.V_Receiving_Orders", con)

                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        V_Receiving_OrdersInfo objrd = new V_Receiving_OrdersInfo
                        {
                            Package_ID = rdr["Package_ID"].ToString(),
                            Roll_ID = rdr["Roll_ID"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Receiving_Date = rdr["Receiving_Date"] == DBNull.Value ? null : (DateTime?)rdr["Receiving_Date"],
                            GR_Quantity = rdr["GR_Quantity"] == DBNull.Value ? null : (decimal?)rdr["GR_Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            GR_Quantity_KG = rdr["GR_Quantity_KG"] == DBNull.Value ? null : (decimal?)rdr["GR_Quantity_KG"],
                            WH_Code = rdr["WH_Code"].ToString(),
                            Warehouse = rdr["Warehouse"].ToString(),
                            Location = rdr["Location"].ToString(),
                            Document_Number = rdr["Document_Number"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString()
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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

        public IEnumerable<Api_Receivingorders_Go> GetErpReceivingOrdersByTag(string Tag)
        {
            List<Api_Receivingorders_Go> lstobj = new List<Api_Receivingorders_Go>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Package_ID,Roll_ID " +
                                       ",Material_Code,Material_Description" +
                                       ",Receiving_Date,GR_Quantity" +
                                       ",Unit,GR_Quantity_KG" +
                                       ",WH_Code,Warehouse" +
                                       ",Location,Document_Number" +
                                       ",Job,Job_Code " +
                                       "FROM dbo.V_Receiving_Orders " +
                                        "WHERE Package_ID = @Tag", con)

                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    cmd.Parameters.AddWithValue("@Tag", Tag);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Api_Receivingorders_Go objrd = new Api_Receivingorders_Go
                        {
                            Package_Id = rdr["Package_ID"].ToString(),
                            Roll_Id = rdr["Roll_ID"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Receiving_Date = rdr["Receiving_Date"] == DBNull.Value ? null : (DateTime?)rdr["Receiving_Date"],
                            Gr_Quantity = rdr["GR_Quantity"] == DBNull.Value ? null : (decimal?)rdr["GR_Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            Gr_Quantity_Kg = rdr["GR_Quantity_KG"] == DBNull.Value ? null : (decimal?)rdr["GR_Quantity_KG"],
                            Wh_Code = rdr["WH_Code"].ToString(),
                            Warehouse = rdr["Warehouse"].ToString(),
                            Locationno = rdr["Location"].ToString(),
                            Document_Number = rdr["Document_Number"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            Matcategory = rdr["Material_Code"].ToString().Substring(0, 2)
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<V_CylinderInfo> GetAllErpCylinders()
        {
            List<V_CylinderInfo> lstobj = new List<V_CylinderInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Material,Material_Description " +
                    ",Customer_Code,Customer_Description,Customer_Reference, PREVIOUS_REF" +
                    ",Color_1,Cylinder_1, BARCODE_1" +
                    ",Color_2,Cylinder_2, BARCODE_2" +
                    ",Color_3,Cylinder_3, BARCODE_3" +
                    ",Color_4,Cylinder_4, BARCODE_4" +
                    ",Color_5,Cylinder_5, BARCODE_5" +
                    ",Color_6,Cylinder_6, BARCODE_6" +
                    ",Color_7,Cylinder_7, BARCODE_7" +
                    ",Color_8,Cylinder_8, BARCODE_8" +
                    ",Color_9,Cylinder_9, BARCODE_9" +
                    ",Color_10,Cylinder_10, BARCODE_10 " +
                    "FROM dbo.Cylinder", con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        V_CylinderInfo objrd = new V_CylinderInfo
                        {
                            Material = rdr["Material"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Customer_Code = rdr["Customer_Code"].ToString(),
                            Customer_Description = rdr["Customer_Description"].ToString(),
                            Customer_Reference = rdr["Customer_Reference"].ToString(),
                            Color_1 = rdr["Color_1"].ToString(),
                            Cylinder1 = rdr["Cylinder_1"].ToString(),
                            Barcode_1 = rdr["BARCODE_1"].ToString(),
                            Color_2 = rdr["Color_2"].ToString(),
                            Cylinder2 = rdr["Cylinder_2"].ToString(),
                            Barcode_2 = rdr["BARCODE_2"].ToString(),
                            Color_3 = rdr["Color_3"].ToString(),
                            Cylinder3 = rdr["Cylinder_3"].ToString(),
                            Barcode_3 = rdr["BARCODE_3"].ToString(),
                            Color_4 = rdr["Color_4"].ToString(),
                            Cylinder4 = rdr["Cylinder_4"].ToString(),
                            Barcode_4 = rdr["BARCODE_4"].ToString(),
                            Color_5 = rdr["Color_5"].ToString(),
                            Cylinder5 = rdr["Cylinder_5"].ToString(),
                            Barcode_5 = rdr["BARCODE_5"].ToString(),
                            Color_6 = rdr["Color_6"].ToString(),
                            Cylinder6 = rdr["Cylinder_6"].ToString(),
                            Barcode_6 = rdr["BARCODE_6"].ToString(),
                            Color_7 = rdr["Color_7"].ToString(),
                            Cylinder7 = rdr["Cylinder_7"].ToString(),
                            Barcode_7 = rdr["BARCODE_7"].ToString(),
                            Color_8 = rdr["Color_8"].ToString(),
                            Cylinder8 = rdr["Cylinder_8"].ToString(),
                            Barcode_8 = rdr["BARCODE_8"].ToString(),
                            Color_9 = rdr["Color_9"].ToString(),
                            Cylinder9 = rdr["Cylinder_9"].ToString(),
                            Barcode_9 = rdr["BARCODE_9"].ToString(),
                            Color_10 = rdr["Color_10"].ToString(),
                            Cylinder10 = rdr["Cylinder_10"].ToString(),
                            Barcode_10 = rdr["BARCODE_10"].ToString(),
                            PREVIOUS_REF = rdr["PREVIOUS_REF"].ToString()
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public async Task<IEnumerable<V_List_OF_Materials_NeedsInfo>> GetAllErpListofNeeds()
        {
            List<V_List_OF_Materials_NeedsInfo> lstobj = new List<V_List_OF_Materials_NeedsInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Customer,Customer_Name" +
                   ",Finished_Product,Finished_Product_Description" +
                   ",Material_Code,Description" +
                   ",Element,cast(Quantity AS DECIMAL(18,4)) AS Quantity" +
                   ",Unit,Job" +
                   ",Job_Code,MO_Barcode " +
                   "FROM dbo.V_List_OF_Materials_Need", con)
                    {
                        CommandType = CommandType.Text
                    };

                    await con.OpenAsync();

                    var rdr = await cmd.ExecuteReaderAsync();

                    //SqlDataReader rdr = cmd.ExecuteReader();
                    //Task ReadSQL = await rdr.ReadAsync();

                    while (await rdr.ReadAsync())
                    {
                        V_List_OF_Materials_NeedsInfo objrd = new V_List_OF_Materials_NeedsInfo
                        {
                            Customer = rdr["Customer"].ToString(),
                            Customer_Name = rdr["Customer_Name"].ToString(),
                            Finished_Product = rdr["Finished_Product"].ToString(),
                            Finished_Product_Description = rdr["Finished_Product_Description"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Description = rdr["Description"].ToString(),
                            Element = rdr["Element"].ToString(),
                            Quantity = rdr["Quantity"] == DBNull.Value ? null : (decimal?)rdr["Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            MO_Barcode = rdr["MO_Barcode"].ToString()
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
                {
                    Log.Error(ex.ToString());
                }
                finally
                {
                    await con.CloseAsync();
                }
            }
            return lstobj;
        }

        public IEnumerable<Api_Deliveryorder_Go> GetAllErpListofNeedsByMo(string mocode)
        {
            List<Api_Deliveryorder_Go> lstobj = new List<Api_Deliveryorder_Go>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Customer,Customer_Name" +
                    ",Finished_Product,Finished_Product_Description" +
                    ",Material_Code,Description" +
                    ",Element,cast(Quantity AS DECIMAL(18,4)) AS Quantity" +
                    ",Unit,Job" +
                    ",Job_Code,MO_Barcode " +
                    "FROM dbo.V_List_OF_Materials_Need " +
                    "WHERE MO_Barcode = '" + mocode + "'", con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                        {
                            Package_Id = "",
                            Roll_Id = "",
                            Customer_Code = rdr["Customer"].ToString(),
                            Customer_Description = rdr["Customer_Name"].ToString(),
                            Finished_Product = rdr["Finished_Product"].ToString(),
                            Finished_Product_Description = rdr["Finished_Product_Description"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Material_Description = rdr["Description"].ToString(),
                            Matelement = rdr["Element"].ToString(),
                            Wh_Code = "",
                            Warehouse = "",
                            Locationno = "",
                            Quantity = rdr["Quantity"] == DBNull.Value ? null : (decimal?)rdr["Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            Mo_Barcode = rdr["MO_Barcode"].ToString(),
                            Picked = 0,
                            Dotype = "LON"

                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<V_Reserved_MaterialsInfo> GetAllErpReservedMaterials()
        {
            List<V_Reserved_MaterialsInfo> lstobj = new List<V_Reserved_MaterialsInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Package_ID,Roll_ID " +
                    ",Material_Code,Description" +
                    ",WH_Code,Warehouse" +
                    ",Location,cast(Quantity as float) AS Quantity" +
                    ",Unit,Job" +
                    ",Job_Code,MO_Barcode " +
                    "FROM dbo.V_Reserved_Materials", con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        V_Reserved_MaterialsInfo objrd = new V_Reserved_MaterialsInfo
                        {
                            Package_ID = rdr["Package_ID"].ToString(),
                            Roll_ID = rdr["Roll_ID"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Description = rdr["Description"].ToString(),
                            WH_Code = rdr["WH_Code"].ToString(),
                            Warehouse = rdr["Warehouse"].ToString(),
                            Location = rdr["Location"].ToString(),
                            Quantity = rdr["Quantity"] == DBNull.Value ? null : (double?)rdr["Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            MO_Barcode = rdr["MO_Barcode"].ToString()

                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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

        public IEnumerable<Api_Deliveryorder_Go> GetAllErpReservedMaterialsbyMo(string mocode)
        {
            List<Api_Deliveryorder_Go> lstobj = new List<Api_Deliveryorder_Go>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Package_ID,Roll_ID " +
                    ",Material_Code,Description" +
                    ",WH_Code,Warehouse" +
                    ",Location,cast(Quantity AS DECIMAL(18,4)) as Quantity" +
                    ",Unit,Job" +
                    ",Job_Code,MO_Barcode " +
                    "FROM dbo.V_Reserved_Materials " +
                    "WHERE MO_Barcode = '" + mocode + "'", con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                        {
                            Package_Id = rdr["Package_ID"].ToString(),
                            Roll_Id = rdr["Roll_ID"].ToString(),
                            Material_Code = rdr["Material_Code"].ToString(),
                            Material_Description = rdr["Description"].ToString(),
                            Matelement = rdr["Description"].ToString(),
                            Wh_Code = rdr["WH_Code"].ToString(),
                            Warehouse = rdr["Warehouse"].ToString(),
                            Locationno = rdr["Location"].ToString(),
                            Quantity = rdr["Quantity"] == DBNull.Value ? null : (decimal?)rdr["Quantity"],
                            Unit = rdr["Unit"].ToString(),
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            Mo_Barcode = rdr["MO_Barcode"].ToString(),
                            Picked = 0,
                            Dotype = "RES",
                            Customer_Code = "",
                            Customer_Description = "",
                            Finished_Product = "",
                            Finished_Product_Description = ""
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<Api_Deliveryorder_Go> GetAllErpReservedMaterialsCylinderbyMo(string mocode)
        {
            List<Api_Deliveryorder_Go> lstobj = new List<Api_Deliveryorder_Go>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {

                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT Finished_Product, Finished_Product_Description, Job, Job_Code, MO_Barcode, PREVIOUS_REF");
                    Sql.AppendLine(",Color_1,Cylinder_1, BARCODE_1");
                    Sql.AppendLine(",Color_2,Cylinder_2, BARCODE_2");
                    Sql.AppendLine(",Color_3,Cylinder_3, BARCODE_3");
                    Sql.AppendLine(",Color_4,Cylinder_4, BARCODE_4");
                    Sql.AppendLine(",Color_5,Cylinder_5, BARCODE_5");
                    Sql.AppendLine(",Color_6,Cylinder_6, BARCODE_6");
                    Sql.AppendLine(",Color_7,Cylinder_7, BARCODE_7");
                    Sql.AppendLine(",Color_8,Cylinder_8, BARCODE_8");
                    Sql.AppendLine(",Color_9,Cylinder_9, BARCODE_9");
                    Sql.AppendLine(",Color_10,Cylinder_10, BARCODE_10");
                    Sql.AppendLine("FROM dbo.V_List_OF_Materials_Need_Cylinder");
                    Sql.AppendLine("WHERE MO_Barcode=@MO_Barcode");

                    SqlCommand cmd = new SqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    cmd.Parameters.AddWithValue("@MO_Barcode", mocode);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        

                            if (rdr["Cylinder_1"].ToString() != null && rdr["Cylinder_1"].ToString().Length > 0)
                            {
                                Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                                {
                                    Package_Id = rdr["Cylinder_1"].ToString(),
                                    Roll_Id = rdr["BARCODE_1"].ToString(),
                                    Material_Code = rdr["Finished_Product"].ToString(),
                                    Material_Description = rdr["Finished_Product_Description"].ToString(),
                                    Matelement = "1",
                                    Wh_Code = null,
                                    Warehouse = null,
                                    Locationno = null,
                                    Quantity = 1,
                                    Unit = null,
                                    Job = rdr["Job"].ToString(),
                                    Job_Code = rdr["Job_Code"].ToString(),
                                    Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                    Picked = 0,
                                    Dotype = "REC",
                                    Customer_Code = "",
                                    Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                    Finished_Product = rdr["BARCODE_1"].ToString(),
                                    Finished_Product_Description = rdr["Color_1"].ToString()
                                };
                                lstobj.Add(objrd);
                           
                            }
                        if (rdr["Cylinder_2"].ToString() != null && rdr["Cylinder_2"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_2"].ToString(),
                                Roll_Id = rdr["BARCODE_2"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "2",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_2"].ToString(),
                                Finished_Product_Description = rdr["Color_2"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_3"].ToString() != null && rdr["Cylinder_3"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_3"].ToString(),
                                Roll_Id = rdr["BARCODE_3"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "3",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_3"].ToString(),
                                Finished_Product_Description = rdr["Color_3"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_4"].ToString() != null && rdr["Cylinder_4"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_4"].ToString(),
                                Roll_Id = rdr["BARCODE_4"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement ="4",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_4"].ToString(),
                                Finished_Product_Description = rdr["Color_4"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_5"].ToString() != null && rdr["Cylinder_5"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_5"].ToString(),
                                Roll_Id = rdr["BARCODE_5"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "5",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_5"].ToString(),
                                Finished_Product_Description = rdr["Color_5"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_6"].ToString() != null && rdr["Cylinder_6"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_6"].ToString(),
                                Roll_Id = rdr["BARCODE_6"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "6",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_6"].ToString(),
                                Finished_Product_Description = rdr["Color_6"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_7"].ToString() != null && rdr["Cylinder_7"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_7"].ToString(),
                                Roll_Id = rdr["BARCODE_7"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "7",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_7"].ToString(),
                                Finished_Product_Description = rdr["Color_7"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_8"].ToString() != null && rdr["Cylinder_8"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_8"].ToString(),
                                Roll_Id = rdr["BARCODE_8"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "8",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_8"].ToString(),
                                Finished_Product_Description = rdr["Color_8"].ToString()
                            };
                            lstobj.Add(objrd);

                        }


                        if (rdr["Cylinder_9"].ToString() != null && rdr["Cylinder_9"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_9"].ToString(),
                                Roll_Id = rdr["BARCODE_9"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "9",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_9"].ToString(),
                                Finished_Product_Description = rdr["Color_9"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_10"].ToString() != null && rdr["Cylinder_10"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_10"].ToString(),
                                Roll_Id = rdr["BARCODE_10"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "10",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_10"].ToString(),
                                Finished_Product_Description = rdr["Color_10"].ToString()
                            };
                            lstobj.Add(objrd);

                        }





                    }
                }
                catch (SqlException ex)
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

        public IEnumerable<Api_Deliveryorder_Go> GetAllErpReservedMaterialsCylinderbyMat(string Matcode)
        {
            List<Api_Deliveryorder_Go> lstobj = new List<Api_Deliveryorder_Go>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {

                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT TOP 1 Finished_Product, Finished_Product_Description, Job, Job_Code, MO_Barcode, PREVIOUS_REF");
                    Sql.AppendLine(",Color_1,Cylinder_1, BARCODE_1");
                    Sql.AppendLine(",Color_2,Cylinder_2, BARCODE_2");
                    Sql.AppendLine(",Color_3,Cylinder_3, BARCODE_3");
                    Sql.AppendLine(",Color_4,Cylinder_4, BARCODE_4");
                    Sql.AppendLine(",Color_5,Cylinder_5, BARCODE_5");
                    Sql.AppendLine(",Color_6,Cylinder_6, BARCODE_6");
                    Sql.AppendLine(",Color_7,Cylinder_7, BARCODE_7");
                    Sql.AppendLine(",Color_8,Cylinder_8, BARCODE_8");
                    Sql.AppendLine(",Color_9,Cylinder_9, BARCODE_9");
                    Sql.AppendLine(",Color_10,Cylinder_10, BARCODE_10");
                    Sql.AppendLine("FROM dbo.V_List_OF_Materials_Need_Cylinder");
                    Sql.AppendLine("WHERE Finished_Product=@Finished_Product");

                    SqlCommand cmd = new SqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    cmd.Parameters.AddWithValue("@Finished_Product", Matcode);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {


                        if (rdr["Cylinder_1"].ToString() != null && rdr["Cylinder_1"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_1"].ToString(),
                                Roll_Id = rdr["BARCODE_1"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "1",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_1"].ToString(),
                                Finished_Product_Description = rdr["Color_1"].ToString()
                            };
                            lstobj.Add(objrd);

                        }
                        if (rdr["Cylinder_2"].ToString() != null && rdr["Cylinder_2"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_2"].ToString(),
                                Roll_Id = rdr["BARCODE_2"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "2",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_2"].ToString(),
                                Finished_Product_Description = rdr["Color_2"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_3"].ToString() != null && rdr["Cylinder_3"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_3"].ToString(),
                                Roll_Id = rdr["BARCODE_3"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "3",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_3"].ToString(),
                                Finished_Product_Description = rdr["Color_3"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_4"].ToString() != null && rdr["Cylinder_4"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_4"].ToString(),
                                Roll_Id = rdr["BARCODE_4"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "4",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_4"].ToString(),
                                Finished_Product_Description = rdr["Color_4"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_5"].ToString() != null && rdr["Cylinder_5"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_5"].ToString(),
                                Roll_Id = rdr["BARCODE_5"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "5",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_5"].ToString(),
                                Finished_Product_Description = rdr["Color_5"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_6"].ToString() != null && rdr["Cylinder_6"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_6"].ToString(),
                                Roll_Id = rdr["BARCODE_6"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "6",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_6"].ToString(),
                                Finished_Product_Description = rdr["Color_6"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_7"].ToString() != null && rdr["Cylinder_7"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_7"].ToString(),
                                Roll_Id = rdr["BARCODE_7"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "7",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_7"].ToString(),
                                Finished_Product_Description = rdr["Color_7"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_8"].ToString() != null && rdr["Cylinder_8"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_8"].ToString(),
                                Roll_Id = rdr["BARCODE_8"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "8",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_8"].ToString(),
                                Finished_Product_Description = rdr["Color_8"].ToString()
                            };
                            lstobj.Add(objrd);

                        }


                        if (rdr["Cylinder_9"].ToString() != null && rdr["Cylinder_9"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_9"].ToString(),
                                Roll_Id = rdr["BARCODE_9"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "9",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_9"].ToString(),
                                Finished_Product_Description = rdr["Color_9"].ToString()
                            };
                            lstobj.Add(objrd);

                        }

                        if (rdr["Cylinder_10"].ToString() != null && rdr["Cylinder_10"].ToString().Length > 0)
                        {
                            Api_Deliveryorder_Go objrd = new Api_Deliveryorder_Go
                            {
                                Package_Id = rdr["Cylinder_10"].ToString(),
                                Roll_Id = rdr["BARCODE_10"].ToString(),
                                Material_Code = rdr["Finished_Product"].ToString(),
                                Material_Description = rdr["Finished_Product_Description"].ToString(),
                                Matelement = "10",
                                Wh_Code = null,
                                Warehouse = null,
                                Locationno = null,
                                Quantity = 1,
                                Unit = null,
                                Job = rdr["Job"].ToString(),
                                Job_Code = rdr["Job_Code"].ToString(),
                                Mo_Barcode = rdr["MO_Barcode"].ToString(),
                                Picked = 0,
                                Dotype = "REC",
                                Customer_Code = "",
                                Customer_Description = rdr["PREVIOUS_REF"].ToString(),
                                Finished_Product = rdr["BARCODE_10"].ToString(),
                                Finished_Product_Description = rdr["Color_10"].ToString()
                            };
                            lstobj.Add(objrd);

                        }





                    }
                }
                catch (SqlException ex)
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

        public IEnumerable<MaterialInfo> GetAllErpMatReceivingOrders()
        {
            List<MaterialInfo> lstobj = new List<MaterialInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT " +
                    "Material_Code,Material_Description" +
                    ",Unit " +
                    "FROM dbo.V_Receiving_Orders " +
                    "GROUP BY Material_Code, Material_Description, Unit"
                    , con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MaterialInfo objrd = new MaterialInfo
                        {
                            Material_Code = rdr["Material_Code"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Unit = rdr["Unit"].ToString(),
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<Cylinder> GetAllErpCylindersbytag(string Tag)
        {
            List<Cylinder> lstobj = new List<Cylinder>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();

                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_1] as Colorcode,[Cylinder_1] as Cylindercode,[BARCODE_1] as Cylinderbar");
                    Sql.AppendLine(",1 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_1] is not null and [BARCODE_1] is not null)");
                    Sql.AppendLine("AND [BARCODE_1] = @BARCODE_1");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_2] as Colorcode,[Cylinder_2] as Cylindercode,[BARCODE_2] as Cylinderbar");
                    Sql.AppendLine(",3 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_2] is not null and [BARCODE_2] is not null)");
                    Sql.AppendLine("AND [BARCODE_2] = @BARCODE_2");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_3] as Colorcode,[Cylinder_3] as Cylindercode,[BARCODE_3] as Cylinderbar");
                    Sql.AppendLine(",3 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_3] is not null and [BARCODE_3] is not null)");
                    Sql.AppendLine("AND [BARCODE_3] = @BARCODE_3");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_4] as Colorcode,[Cylinder_4] as Cylindercode,[BARCODE_4] as Cylinderbar");
                    Sql.AppendLine(",4 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_4] is not null and [BARCODE_4] is not null)");
                    Sql.AppendLine("AND [BARCODE_4] = @BARCODE_4");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_5] as Colorcode,[Cylinder_5] as Cylindercode,[BARCODE_5] as Cylinderbar");
                    Sql.AppendLine(",5 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_5] is not null and [BARCODE_5] is not null)");
                    Sql.AppendLine("AND [BARCODE_5] = @BARCODE_5");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_6] as Colorcode,[Cylinder_6] as Cylindercode,[BARCODE_6] as Cylinderbar");
                    Sql.AppendLine(",6 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_6] is not null and [BARCODE_6] is not null)");
                    Sql.AppendLine("AND [BARCODE_6] = @BARCODE_6");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_7] as Colorcode,[Cylinder_7] as Cylindercode,[BARCODE_7] as Cylinderbar");
                    Sql.AppendLine(",7 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_7] is not null and [BARCODE_7] is not null)");
                    Sql.AppendLine("AND [BARCODE_7] = @BARCODE_7");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_8] as Colorcode,[Cylinder_8] as Cylindercode,[BARCODE_8] as Cylinderbar");
                    Sql.AppendLine(",8 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_8] is not null and [BARCODE_8] is not null)");
                    Sql.AppendLine("AND [BARCODE_8] = @BARCODE_8");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_9] as Colorcode,[Cylinder_9] as Cylindercode,[BARCODE_9] as Cylinderbar");
                    Sql.AppendLine(",9 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_9] is not null and [BARCODE_9] is not null)");
                    Sql.AppendLine("AND [BARCODE_9] = @BARCODE_9");
                    Sql.AppendLine("UNION ALL");
                    Sql.AppendLine("SELECT [Material],[Material_Description],[Customer_Code] ,[Customer_Description]");
                    Sql.AppendLine(",[Customer_Reference],[PREVIOUS_REF],[Color_10] as Colorcode,[Cylinder_10] as Cylindercode,[BARCODE_10] as Cylinderbar");
                    Sql.AppendLine(",10 as Cylindeno, [IState]");
                    Sql.AppendLine("FROM [dbo].[Cylinder]");
                    Sql.AppendLine("WHERE ([Cylinder_10] is not null and [BARCODE_10] is not null)");
                    Sql.AppendLine("AND [BARCODE_10] = @BARCODE_10");

                    SqlCommand cmd = new SqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();

                    cmd.Parameters.AddWithValue("@BARCODE_1", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_2", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_3", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_4", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_5", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_6", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_7", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_8", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_9", Tag);
                    cmd.Parameters.AddWithValue("@BARCODE_10", Tag);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Cylinder objrd = new Cylinder
                        {
                            Material = rdr["Material"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Customer_Code = rdr["Customer_Code"].ToString(),
                            Customer_Description = rdr["Customer_Description"].ToString(),
                            Customer_Reference = rdr["Customer_Reference"].ToString(),
                            Previos_Ref = rdr["PREVIOUS_REF"].ToString(),
                            Colorcode = rdr["Colorcode"].ToString(),
                            Cylindercode = rdr["Cylindercode"].ToString(),
                            Cylinderbar = rdr["Cylinderbar"].ToString(),
                            Cylindeno = rdr["Cylindeno"] == DBNull.Value ? 0 : (int?)rdr["Cylindeno"],
                            IState = rdr["IState"] == DBNull.Value ? 0 : (int?)rdr["Cylindeno"],
                        };

                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<V_CylinderInfo> GetAllErpCylindersbyMat(string matcode)
        {
            List<V_CylinderInfo> lstobj = new List<V_CylinderInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT Material,Material_Description");
                    Sql.AppendLine(",Customer_Code,Customer_Description,Customer_Reference, PREVIOUS_REF");
                    Sql.AppendLine(",Color_1,Cylinder_1, BARCODE_1");
                    Sql.AppendLine(",Color_2,Cylinder_2, BARCODE_2");
                    Sql.AppendLine(",Color_3,Cylinder_3, BARCODE_3");
                    Sql.AppendLine(",Color_4,Cylinder_4, BARCODE_4");
                    Sql.AppendLine(",Color_5,Cylinder_5, BARCODE_5");
                    Sql.AppendLine(",Color_6,Cylinder_6, BARCODE_6");
                    Sql.AppendLine(",Color_7,Cylinder_7, BARCODE_7");
                    Sql.AppendLine(",Color_8,Cylinder_8, BARCODE_8");
                    Sql.AppendLine(",Color_9,Cylinder_9, BARCODE_9");
                    Sql.AppendLine(",Color_10,Cylinder_10, BARCODE_10");
                    Sql.AppendLine("FROM dbo.Cylinder");
                    Sql.AppendLine("WHERE Material=@Material");


                    SqlCommand cmd = new SqlCommand(Sql.ToString(), con)
                    {
                        CommandType = CommandType.Text
                    };


                    con.Open();
                    cmd.Parameters.AddWithValue("@Material", matcode);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        V_CylinderInfo objrd = new V_CylinderInfo
                        {
                            Material = rdr["Material"].ToString(),
                            Material_Description = rdr["Material_Description"].ToString(),
                            Customer_Code = rdr["Customer_Code"].ToString(),
                            Customer_Description = rdr["Customer_Description"].ToString(),
                            Customer_Reference = rdr["Customer_Reference"].ToString(),
                            Color_1 = rdr["Color_1"].ToString(),
                            Cylinder1 = rdr["Cylinder_1"].ToString(),
                            Barcode_1 = rdr["BARCODE_1"].ToString(),
                            Color_2 = rdr["Color_2"].ToString(),
                            Cylinder2 = rdr["Cylinder_2"].ToString(),
                            Barcode_2 = rdr["BARCODE_2"].ToString(),
                            Color_3 = rdr["Color_3"].ToString(),
                            Cylinder3 = rdr["Cylinder_3"].ToString(),
                            Barcode_3 = rdr["BARCODE_3"].ToString(),
                            Color_4 = rdr["Color_4"].ToString(),
                            Cylinder4 = rdr["Cylinder_4"].ToString(),
                            Barcode_4 = rdr["BARCODE_4"].ToString(),
                            Color_5 = rdr["Color_5"].ToString(),
                            Cylinder5 = rdr["Cylinder_5"].ToString(),
                            Barcode_5 = rdr["BARCODE_5"].ToString(),
                            Color_6 = rdr["Color_6"].ToString(),
                            Cylinder6 = rdr["Cylinder_6"].ToString(),
                            Barcode_6 = rdr["BARCODE_6"].ToString(),
                            Color_7 = rdr["Color_7"].ToString(),
                            Cylinder7 = rdr["Cylinder_7"].ToString(),
                            Barcode_7 = rdr["BARCODE_7"].ToString(),
                            Color_8 = rdr["Color_8"].ToString(),
                            Cylinder8 = rdr["Cylinder_8"].ToString(),
                            Barcode_8 = rdr["BARCODE_8"].ToString(),
                            Color_9 = rdr["Color_9"].ToString(),
                            Cylinder9 = rdr["Cylinder_9"].ToString(),
                            Barcode_9 = rdr["BARCODE_9"].ToString(),
                            Color_10 = rdr["Color_10"].ToString(),
                            Cylinder10 = rdr["Cylinder_10"].ToString(),
                            Barcode_10 = rdr["BARCODE_10"].ToString(),
                            PREVIOUS_REF = rdr["PREVIOUS_REF"].ToString()
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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


        public IEnumerable<Cutime> GetErpCuringtimebyID(string SqlFilter)
        {
            List<Cutime> lstobj = new List<Cutime>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {

                    StringBuilder sqlQurey = new StringBuilder();


                    sqlQurey.AppendLine("SELECT COALESCE([Job], '') AS [Job] ");
                    sqlQurey.AppendLine(", COALESCE([Job_Code], '') AS [Job_Code] ");
                    sqlQurey.AppendLine(", COALESCE([Item_Code], '') AS [Item_Code] ");
                    sqlQurey.AppendLine(", COALESCE([Adhesive1_STD], '') AS [Adhesive1_STD] ");
                    sqlQurey.AppendLine(", COALESCE([Adhesive2_STD], '') AS [Adhesive2_STD] ");
                    sqlQurey.AppendLine(", COALESCE([Adhesive3_STD], '') AS [Adhesive3_STD] ") ;
                    sqlQurey.AppendLine(", COALESCE([Adhesive4_STD], '') AS [Adhesive4_STD] ");
                    sqlQurey.AppendLine(", COALESCE([Type], '') AS [Type] ");
                    sqlQurey.AppendLine(", COALESCE([Film1], '') AS [Film1] ");
                    sqlQurey.AppendLine(", COALESCE([Film2], '') AS [Film2] ");
                    sqlQurey.AppendLine(", COALESCE([Film3], '') AS [Film3] ");
                    sqlQurey.AppendLine(", COALESCE([Film4], '') AS [Film4] ");
                    sqlQurey.AppendLine(", COALESCE([Film5], '') AS [Film5] ");
                    sqlQurey.AppendLine(", COALESCE([Adhesive], '') AS [Adhesive] ");
                    sqlQurey.AppendLine(", COALESCE([Hardener], '') AS [Hardener] ");
                    sqlQurey.AppendLine(", COALESCE([Layers], 0) AS [Layers] ");
                    sqlQurey.AppendLine(", COALESCE([Temp (C)], 0) AS [TempC] ");
                    sqlQurey.AppendLine(", COALESCE([Time (Hr.)], 0) AS [TimeH] ");
                    sqlQurey.AppendLine(", COALESCE([ID], '') AS [ID] ");
                    sqlQurey.AppendLine("FROM Staging.dbo.CuringTime");
                    sqlQurey.AppendLine("WHERE [ID] in (" + SqlFilter.ToString() + ")");
                    sqlQurey.AppendLine(";");

                    SqlCommand cmd = new SqlCommand(sqlQurey.ToString(), con)

                    {
                        CommandType = CommandType.Text
                    };

                    con.Open();
                    
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cutime objrd = new Cutime
                        {
                            Job = rdr["Job"].ToString(),
                            Job_Code = rdr["Job_Code"].ToString(),
                            Item_Code = rdr["Item_Code"].ToString(),
                            Adhesive1_STD = rdr["Adhesive1_STD"].ToString(),
                            Adhesive2_STD = rdr["Adhesive2_STD"].ToString(),
                            Adhesive3_STD = rdr["Adhesive3_STD"].ToString(),
                            Adhesive4_STD = rdr["Adhesive4_STD"].ToString(),
                            Type = rdr["Type"].ToString(),
                            Film1 = rdr["Film1"].ToString(),
                            Film2= rdr["Film2"].ToString(),
                            Film3= rdr["Film3"].ToString(),
                            Film4= rdr["Film4"].ToString(),
                            Film5= rdr["Film5"].ToString(),
                            Adhesive = rdr["Adhesive"].ToString(),
                            Hardener = rdr["Hardener"].ToString(),
                            Layers = rdr["Layers"] == DBNull.Value ? null : (Int32?)rdr["Layers"],
                            TempC = rdr["TempC"] == DBNull.Value ? null : (Int32?)rdr["TempC"],
                            TimeH = rdr["TimeH"] == DBNull.Value ? null : (Int32?)rdr["TimeH"],
                            ID = rdr["ID"].ToString()
                        };
                        lstobj.Add(objrd);
                    }
                }
                catch (SqlException ex)
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
