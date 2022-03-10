using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace GoWMS.Server.Controllers
{
    public class ErpApiService
    {
        readonly ApiDAL objDAL = new ApiDAL();

        public List<Api_Itemmaster_Go> GetAllApiItemmasterGos()
        {
            List<Api_Itemmaster_Go> retlist = objDAL.GetAllApiItemmasterGo().ToList();
            return retlist;
        }

        public List<Api_Cylinder_Go> GetAllApiCylinderGos()
        {
            List<Api_Cylinder_Go> retlist = objDAL.GetAllApiCylinderGo().ToList();
            return retlist;
        }

        public List<Api_Listofmaterialsneeds_Go> GetAllApiListofNeedGos()
        {
            List<Api_Listofmaterialsneeds_Go> retlist = objDAL.GetAllApiListofNeedGo().ToList();
            return retlist;
        }

        public List<Api_Receivingorders_Go> GetAllApiRecevingorderGos()
        {
            List<Api_Receivingorders_Go> retlist = objDAL.GetAllApiRecevingorderGo().ToList();
            return retlist;
        }
        public List<Api_Reservedmaterials_Go> GetAllApiReservedmaterialGos()
        {
            List<Api_Reservedmaterials_Go> retlist = objDAL.GetAllApiReservedmaterialGo().ToList();
            return retlist;
        }

        public List<Api_Receivingorders_Go> GetAllApiRecevingorderGosypallet(string pallet)
        {
            List<Api_Receivingorders_Go> retlist = objDAL.GetApiRecevingorderGoBypallet(pallet).ToList();
            return retlist;
        }
        public string CancelReceivingOrderbypack(string pallet, string pack)
        {
            objDAL.CancelReceivingOrdersBypack(pallet, pack);
            return "Cancel Successfully";
        }

        public string ClaerReceivingOrderbypack(string pack)
        {
            objDAL.ClaerReceivingOrdersBypack( pack);
            return "Cancel Successfully";
        }

        public string ClaerReceivingOrdersBypallet(string pallet)
        {
            objDAL.ClaerReceivingOrdersBypallet(pallet);
            return "Cancel Successfully";
        }

        

        public string UpdateReceivingOrderbypallet(string pallet)
        {
            objDAL.UpdateReceivingOrdersBypallet(pallet);
            return "Update Successfully";
        }

        public string UpdateReceivingOrderbypack(string pallet, string pack)
        {
            objDAL.UpdateReceivingOrdersBypack(pallet, pack);
            return "Update Successfully";
        }

        public string InsertReceivingOrderbypackAsync(List<Api_Receivingorders_Go> listOrder, string pallet)
        {
            objDAL.InsertReceivingOrdersBypack(listOrder,pallet);
            return "Update Successfully";
        }
        public bool ClaerDeliveryOrder(string orderno)
        {
            bool bret;

            bret= objDAL.ClaerDeliveryOrder(orderno);

            return bret;
        }

        public bool ClaerDeliveryOrderKey(string Matcode)
        {
            bool bret;

            bret = objDAL.ClaerDeliveryOrderKey(Matcode);

            return bret;
        }

      

        public async Task<string> InsertDeliveryOrderAsync(List<Api_Deliveryorder_Go> listOrder)
        {
            await objDAL.InsertDeliveryOrder(listOrder);
            return "Update Successfully";
        }

        public string SetMappedPallet(string pallet)
        {
            objDAL.SetMappPallet(pallet);
            return "Map Successfully";
        }

     

        public string SetMappedPalletCylinder(string pallet)
        {
            objDAL.SetMappPalletCylinder(pallet);
            return "Map Successfully";
        }

        public string SetMappedPalletAgv(string pallet, string source, string destination)
        {
            objDAL.SetMappPalletAgv(pallet, source, destination);
            return "Map Successfully";
        }

        public List<Api_Deliveryorder_Go> GetAllApiDeliveryorder()
        {
            List<Api_Deliveryorder_Go> retlist = objDAL.GetAllDeliveryorderGo().ToList();
            return retlist;
        }

        public List<Api_Deliveryorder_Go> GetAllApiDeliveryorderByMo(string mocode)
        {
            List<Api_Deliveryorder_Go> retlist = objDAL.GetAllDeliveryorderGoByMo(mocode).ToList();
            return retlist;
        }

        public List<Api_Deliveryorder_Go> GetAllDeliveryorderGoByMat(string mocode)
        {
            List<Api_Deliveryorder_Go> retlist = objDAL.GetAllDeliveryorderGoByMat(mocode).ToList();
            return retlist;
        }
        




        public string SetPick(string jsonLon, string jsonRes , DateTime DeliverDate, Int64 idistination, ref  Int32 iret , ref string sret )
        {
            objDAL.SetPicking(jsonLon, jsonRes, DeliverDate , idistination, ref iret , ref sret);
            return sret;
        }

        public string SetPickStation(string jsonLon, string jsonRes, DateTime DeliverDate, string sdestination, ref Int32 iret, ref string sret)
        {
            objDAL.SetPickingByStation(jsonLon, jsonRes, DeliverDate, sdestination, ref iret, ref sret);
            return sret;
        }

        public string SetPickingByStationCylinder(string jsonLon, string jsonRes, DateTime DeliverDate, string sdestination, ref Int32 iret, ref string sret)
        {
            objDAL.SetPickingByStationCylinder(jsonLon, jsonRes, DeliverDate, sdestination, ref iret, ref sret);
            return sret;
        }

        


        public string SetPickUnplaned(string jsonRes, ref Int32 iret, ref string sret)
        {
            objDAL.SetPickingUnplaned(jsonRes, ref iret, ref sret);
            return sret;
        }

        public string SetAuduitASrsUnplaned(string jsonRes, ref Int32 iret, ref string sret)
        {
            objDAL.SetAuduitASrsUnplaned(jsonRes, ref iret, ref sret);
            return sret;
        }

      



    }
}
