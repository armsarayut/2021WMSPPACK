using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Public
{
    public class Rpt_Audittrial
    {
		public Int64? Idx { get; set; }
        public DateTime? Created { get; set; }
        public Int32? Entity_lock { get; set; }
        public int? Entity_Lock { get; internal set; }
        public DateTime? Modified { get; set; }
        public Int64? Client_id { get; set; }
        public string Menu_name { get; set; }
        public string Action_desc { get; set; }
        public string Usid { get; set; }
        public string Client_ip { get; set; }
        public string Client_Ip { get; internal set; }
    }
}
