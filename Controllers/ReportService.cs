using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;


namespace GoWMS.Server.Controllers
{
    public class ReportService
    {
        readonly ReportDAL objDAL = new ReportDAL();

        public Boolean InsertAudittrial(String actdesc, String munname)
        {
            bool bRet = objDAL.InsertAudittrial(actdesc, munname,0);

            return bRet;
        }
        public Boolean InsertAudittrial(String actdesc, String munname, long user)
        {
            bool bRet = objDAL.InsertAudittrial(actdesc, munname, user);

            return bRet;
        }

        public List<Rpt_Tracking_Go> GetTracking_GosByPack(string spallet)
        {
            List<Rpt_Tracking_Go> retlist = objDAL.GetTracking_GosByPack(spallet).ToList();
            return retlist;
        }

        public List<Rpt_Tracking_Go> GetTracking_GosAll()
        {
            List<Rpt_Tracking_Go> retlist = objDAL.GetTracking_GosAll().ToList();
            return retlist;
        }


        public List<Rpt_Tracking_Go> GetTracking_GosByDate(DateTime dtStart, DateTime dtStop)
        {
            List<Rpt_Tracking_Go> retlist = objDAL.GetTracking_GosByDate(dtStart, dtStop).ToList();
            return retlist;
        }

    }
}
