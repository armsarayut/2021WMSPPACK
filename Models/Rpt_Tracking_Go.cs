using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models
{
    public class Rpt_Tracking_Go
    {
        public long? Efidx { get; set; }
        public Int32? Efstatus { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public long? Innovator { get; set; }
        public string Device { get; set; }
        public string Package_id { get; set; }
        public string Pallet_no { get; set; }
        public Int32? Trackstate { get; set; }
        public string Tracklocation { get; set; }

    }
}
