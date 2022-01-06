using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Data
{
    public class VarGlobals
    {
        public static string MainMenu { get; set; }
        public static string CurrentMunu { get; set; }
        public static string Imagelogoreport()
        {
            return $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\Logocus.jpg"}";
        }

        public static string Fontreport()
        {
            return $"{Directory.GetCurrentDirectory()}{@"\wwwroot\fonts\ARIALUNI.TTF"}";
        }

        public static string CurrentUserName { get; set; }
        public static class User
        {
            public static long UserID { get; set; }
            public static string UserCode { get; set; }
            public static string UserName { get; set; }
            public static long DeepID { get; set; }
            public static long GroupID { get; set; }
        }


    }
}
