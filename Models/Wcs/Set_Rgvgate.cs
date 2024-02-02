using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWMS.Server.Models.Wcs
{
    public class Set_Rgvgate
    {
        public Int64? Idx { get; set; }
        public DateTime? Created { get; set; }
        public Int32? Entity_Lock { get; set; }
        public DateTime? Modified { get; set; }
        public Int64? Client_Id { get; set; }
        public string Client_Ip { get; set; }
        public String Gate_Id { get; set; }
        public Int32? Gate_No { get; set; }
        public String Gate_Desc {get; set;}


    }
}
