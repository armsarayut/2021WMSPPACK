﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Erp
{
    public class Cutime
    {
        public string Job { get; set; }
        public string Job_Code { get; set; }
        public string Item_Code { get; set; }
        public string Adhesive1_STD { get; set; }
        public string Adhesive2_STD { get; set; }

        public string Adhesive3_STD { get; set; }
        public string Adhesive4_STD { get; set; }
        public string Type { get; set; }
        public string Film1 { get; set; }
        public string Film2 { get; set; }
        public string Film3 { get; set; }
        public string Film4 { get; set; }
        public string Film5 { get; set; }
        public string Adhesive { get; set; }
        public string Hardener { get; set; }
        public Int32? Layers { get; set; }
        public Int32? TempC { get; set; }
        public Int32? TimeH { get; set; }
        public string ID { get; set; }

    }
}
