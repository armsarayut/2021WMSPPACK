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

        public List<Tas_Agvworks> GetAllTaskAgvCuRoom()
        {
            List<Tas_Agvworks> retlist = objDAL.GetAllTaskAgvCuRoom().ToList();
            return retlist;
        }

        public List<Set_Agv_Gate> GetAllGateAgv()
        {
            List<Set_Agv_Gate> retlist = objDAL.GetAllGateAgv().ToList();
            return retlist;
        }

        public List<Set_Agv_Gate> GetAllGateAgvCuRoom()
        {
            List<Set_Agv_Gate> retlist = objDAL.GetAllGateAgvCuRoom().ToList();
            return retlist;
        }

        

        public List<Functionreturn> CreateAvgworks(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority)

        {
            List<Functionreturn> retlist = objDAL.CreateAvgworks(pallet, worktype, gatesource, gatedest, workpriority).ToList();
            return retlist;
        }

        public List<Functionreturn> CreateAvgworksCuRoom(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority)

        {
            List<Functionreturn> retlist = objDAL.CreateAvgworksCuRoom(pallet, worktype, gatesource, gatedest, workpriority).ToList();
            return retlist;
        }

        public List<Functionreturn> CreateAvgworksCuRoomBLL(string pallet, string worktype, string gatesource, string gatedest, Int32 workpriority)

        {
            List<Functionreturn> retlist = objDAL.CreateAvgworksCuRoomBLL(pallet, worktype, gatesource, gatedest, workpriority).ToList();
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

        public List<Functionreturn> CancleTaskAGVCuRoom(string pallet)
        {
            List<Functionreturn> retlist = objDAL.CancleTaskAGVCuRoom(pallet).ToList();
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

        public System.Data.DataTable GetQueryAgvCuStatusApiName()
        {
            return objDAL.GetQueryAgvCuStatusApiName();
        }

        
        public List<Agv_Status> GetAllAgvStatusDesc()
        {
            return objDAL.GetAllAgvStatusDesc();
        }

        public List<Agv_Status> GetAllAgvCuRoomStatusDesc()
        {
            return objDAL.GetAllAgvCuRoomStatusDesc();
        }

        public List<AgvRptEODStation> GetEndofDayStation()
        {
            List<AgvRptEODStation> retlist = objDAL.GetEndofDayStation().ToList();
            return retlist;
        }

        public System.Data.DataTable ApiAgvTask(DateTime pFromDate, TimeSpan pFromTime, DateTime pToDate, TimeSpan pToTime)
        {
            string vFromDate = pFromDate.ToString("yyyy-MM-dd") + " " + pFromTime.ToString(@"hh\:mm\:ss");
            string vToDate = pToDate.ToString("yyyy-MM-dd") + " " + pToTime.ToString(@"hh\:mm\:ss");
            return objDAL.ApiAgvTask(vFromDate, vToDate);
        }



         public List<Functionreturn> UpdateStationmodeCuroom(string stationname, bool mode)
        {
            List<Functionreturn> retlist = objDAL.UpdateStationmodeCuroom(stationname, mode).ToList();
            return retlist;
        }
    }
}
