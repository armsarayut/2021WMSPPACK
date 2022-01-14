using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Wcs;
using GoWMS.Server.Models.Hagv;


namespace GoWMS.Server.Controllers
{
    public class AgvService
    {
        readonly AgvDAL objDAL = new AgvDAL();

        public List<Tas_Agvworks> GetAllTaskAgv()
        {
            List<Tas_Agvworks> retlist = objDAL.GetAllTaskAgv().ToList();
            return retlist;
        }

        public List<Set_Agv_Gate> GetAllGateAgv()
        {
            List<Set_Agv_Gate> retlist = objDAL.GetAllGateAgv().ToList();
            return retlist;
        }

        public List<Functionreturn> CreateAvgworks(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority)

        {
            List<Functionreturn> retlist = objDAL.CreateAvgworks(pallet, worktype, gatesource, gatedest, workpriority).ToList();
            return retlist;
        }

        public List<Vrptqueueloadtimeagv> GetAllReportTaskAgv()
        {
            List<Vrptqueueloadtimeagv> retlist = objDAL.GetAllReportTaskAgv().ToList();
            return retlist;
        }

        public List<Functionreturn> CancleTaskAGV(string pallet)
        {
            List<Functionreturn> retlist = objDAL.CancleTaskAGV(pallet).ToList();
            return retlist;
        }

        public List<RptTaskHourCount> GetAllReportTaskPerHourAgv()
        {
            List<RptTaskHourCount> retlist = objDAL.GetAllReportTaskPerHourAgv().ToList();
            return retlist;
        }

        public List<DashAgvTime> GetAllDashboardComplete()
        {
            List<DashAgvTime> retlist = objDAL.GetAllDashboardComplete().ToList();
            return retlist;
        }

        public System.Data.DataTable GetQueryAgvStatusApiName()
        {
            return objDAL.GetQueryAgvStatusApiName();
        }
        public List<Agv_Status> GetAllAgvStatusDesc()
        {
            return objDAL.GetAllAgvStatusDesc();
        }

        public List<AgvRptEODStation> GetEndofDayStation()
        {
            List<AgvRptEODStation> retlist = objDAL.GetEndofDayStation().ToList();
            return retlist;
        }
    }
}
