using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Erp
{
    public class Cylinder
    {
        public string Material { get; set; }
        public string Material_Description { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public string Customer_Reference { get; set; }
        public string Previos_Ref { get; set; }
        public string Colorcode { get; set; }
        public string Cylindercode { get; set; }
        public string Cylinderbar { get; set; }
        public int? Cylindeno { get; set; }
        public int? IState { get; set; }
        public string Palletcode { get; set; }
    }
}
