using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Oub;
using GoWMS.Server.Models.Public;
using Microsoft.AspNetCore.Mvc;


namespace GoWMS.Server.Controllers
{
    public class StoreoutService
    {
        readonly OubDAL objDAL = new OubDAL();


        public List<Sap_Storeout> GetAllSapStoreout()
        {
            List<Sap_Storeout> retlist = objDAL.GetAllSapStoreout().ToList();
            return retlist;
        }

        public List<Sap_Storeout> GetSapStoreoutSetBatch()
        {
            List<Sap_Storeout> retlist = objDAL.GetSapStoreoutSetBatch().ToList();
            return retlist;
        }
        public string GetRunnning(string sSeq, Int32 iPad)
        {
            string sRunning = "";
            sRunning = objDAL.GetRunnning(sSeq, iPad);
            return sRunning;
        }
        public Boolean CreateBatchOrder(DateTime deliverydate, Int32 deliveryprio, string orderno, string shiptocode, string sSeq)
        {
            Boolean bRet = false;
            bRet = objDAL.CreateBatchOrder(deliverydate, deliveryprio, orderno, shiptocode, sSeq);
            return bRet;
        }

        public Boolean CreateBatchSetting(string sSeq, Int32 istation)
        {
            Boolean bRet = false;
            bRet = objDAL.CreateBatchSetting(sSeq, istation);
            return bRet;
        }

        public Boolean RollbackBatch(string sSeq)
        {
            Boolean bRet = false;
            bRet = objDAL.RollbackBatch(sSeq);
            return bRet;
        }

        public Boolean StartBatchsetting(string sSeq, Int32 istation)
        {
            Boolean bRet = false;
            bRet = objDAL.StartBatchsetting(sSeq, istation);
            return bRet;
        }

        public Boolean SetDestinationAGV(string batch_no, string station)
        {
            Boolean bRet = false;
            bRet = objDAL.SetDestinationAGV(batch_no, station);
            return bRet;
        }

        public List<Sap_StoreoutInfo> GetPicking(string sPallet)
        {
            List<Sap_StoreoutInfo> retlist = objDAL.GetPicking(sPallet).ToList();
            return retlist;
        }

        public List<Oub_Delivery_Go> GetDeliveryAll()
        {
            List<Oub_Delivery_Go> retlist = objDAL.GetDeliveryAll().ToList();
            return retlist;
        }

        public List<Oub_Delivery_Go> GetDeliveryList(string sPallet)
        {
            List<Oub_Delivery_Go> retlist = objDAL.GetDeliveryList(sPallet).ToList();
            return retlist;
        }

        public List<Sap_StoreoutInfo> GetPickingID(string SPackID)
        {
            List<Sap_StoreoutInfo> retlist = objDAL.GetPickingID(SPackID).ToList();
            return retlist;
        }
        public void SetConsolMappallet(string pallet, string packid)
        {
            objDAL.SetConsolMappallet(pallet, packid);
        }

        public void CancelConsolBypack(string pack)
        {
            objDAL.CancelConsolBypack(pack);
        }

        public void CancelConsolByID(Int64 pack)
        {
            objDAL.CancelConsolByID(pack);
        }




    }
}
