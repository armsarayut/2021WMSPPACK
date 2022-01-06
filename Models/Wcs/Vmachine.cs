using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Wcs
{
    public class Vmachine
    {
        public string Mccode { get; set; }
        public Int32? St_no { get; set; }
        public string Desc_th { get; set; }
        public bool? Is_alert { get; set; }
    }
}
