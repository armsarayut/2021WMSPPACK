using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Mas
{
    public class Mas_Cylinderlist_Go
    {
		public Int64? Efidx { get; set; }
		public Int32? Efstatus { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }
		public Int64? Innovator { get; set; }
		public string Device { get; set; }
		public string Material_Code { get; set; }
		public string Material_Description { get; set; }
		public string Customer_Code { get; set; }
		public string Customer_Description { get; set; }
		public string Customer_Reference { get; set; }
		public string Previos_ref { get; set; }
		
		public Int32? Cylinderno { get; set; }
		public string Colorcode { get; set; }
		public string Cylindercode { get; set; }
		public string Cylinderbar { get; set; }
		public string Palletno { get; set; }
		public string Itemtag { get; set; }
		
	}
}
