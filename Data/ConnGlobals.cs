﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Data
{
    public static class ConnGlobals
    {
        public static string GetConnLocalDB()
        {
            return "server=DESKTOP-NQ62BHU\\MSSQL; database=GoSQL;Trusted_Connection=True;";
        }

        #region Load Database

        //private static readonly string NpgServer = "localhost"; // Develop
        //private static readonly string NpgServer = "192.168.200.141"; // Production
        private static readonly string NpgServer = "10.50.68.11"; // Production Public
        //private static readonly string NpgServer = "192.168.1.14"; // Production Public

        //private static readonly string NpgServer = "203.159.93.86"; // Host Test

        //private static readonly string NpgDB = "PPACK"; // Develop
        private static readonly string NpgDB = "Gowes"; // Production

        private static readonly string NpgUser = "postgres";
        private static readonly string NpgPass = "@ei0u2020";
        private static readonly string NpgPort = "5432";
        private static readonly string NpgContime = "60";
        private static readonly string NpgComtime = "0"; 

        /// <summary>
        /// GetConnLocalDBPG
        /// </summary>
        /// <remarks>Get Database Connection string for GoWMS</remarks>
        /// <returns></returns>
        public static string GetConnLocalDBPG()
        {
            return "Server=" + NpgServer + " ; Port=" + NpgPort + "; Database=" + NpgDB + "; User Id=" + NpgUser + "; Password=" + NpgPass + "; CommandTimeout=" + NpgComtime + "; Timeout=" + NpgContime + ";";
        }
        #endregion

        /// <summary>
        /// GetConnApiDB
        /// </summary>
        /// <remarks>Get Database Connection string for API GoWMS</remarks>
        /// <returns></returns>
        public static string GetConnApiDB()
        {
            return "Server=" + NpgServer + " ;Port=" + NpgPort + ";Database=" + NpgDB + ";User Id=" + NpgUser + ";Password=" + NpgPass + ";Timeout=" + NpgContime + ";";
        }

        #region ERP Database
        //private static readonly string ErpServer = "203.150.202.44";

        private static readonly string ErpServer = "10.28.148.87"; // New Server 2023/12/11
        private static readonly string ErpDB = "Staging";
        //private static readonly string ErpUser = "ASRS";
        //private static readonly string ErpPass = "ASRS";
        private static readonly string ErpUser = "ASRS_WMS";
        private static readonly string ErpPass = "ASRS_WMS";

        private static readonly string ErpContime = "120";

        /// <summary>
        /// GetConnErpDB
        /// </summary>
        /// <remarks>Get Database Connection String For SQL Server. This project is Prepack</remarks>
        /// <returns></returns>
        public static string GetConnErpDB()
        {
            return "Data Source=" + ErpServer + ";Initial Catalog=" + ErpDB + ";Persist Security Info=True;User ID=" + ErpUser + ";Password=" + ErpPass + ";Connect Timeout=" + ErpContime + ";";
            //return "server=DESKTOP-NQ62BHU\\MSSQL; database=GoSQL;Trusted_Connection=True;";
            //return "Data Source=" + ErpServer + ";Initial Catalog=" + ErpDB + ";Persist Security Info=True;User ID=" + ErpUser + ";Password=" + ErpPass + ";";
        }
        #endregion

        #region ERP WGC Site [Oracle 11.2G]
        private static readonly string oraServer = "192.168.1.143";
        private static readonly string oraPort = "1521";
        //private static readonly string oraSource = "ASRS_WMS";
        private static readonly string oraUser = "WGRB";
        private static readonly string oraPass = "WGRB2021LINK";
        private static readonly string oraDB = "orcl";

        /// <summary>
        /// GetConnErpDBWCG
        /// </summary>
        /// <remarks>Get Database Connection String For Oracle. This project is WGC</remarks>
        /// <returns></returns>
        public static string GetConnErpDBWCG()
        {
            //Using Oracle.ManagedDataAccess.Core without tnsnames.ora
            //return "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + oraServer + ")(PORT=" + oraPort + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SID = " + oraDB + ")));User Id=" + oraUser + ";Password=" + oraPass + ";";
            return "Provider = OraOLEDB.Oracle; Data Source = WGRB; User Id = WGRB; Password = WGRB2021LINK; OLEDB.NET = True;";
        }
        #endregion

    }
}
