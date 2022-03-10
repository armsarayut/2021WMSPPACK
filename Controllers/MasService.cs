using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Mas;
using GoWMS.Server.Models.Erp;

namespace GoWMS.Server.Controllers
{
    public class MasService
    {
        readonly MasDAL objDAL = new MasDAL();

        public List<Mas_Pallet_Go> GetAllMasterPallets()
        {
            List<Mas_Pallet_Go> retlist = objDAL.GetAllMasterpalletGo().ToList();
            return retlist;
        }

        public List<Mas_Storagebin_Go> GetAllMasterStorageBins()
        {
            List<Mas_Storagebin_Go> retlist = objDAL.GetAllStorageBinGo().ToList();
            return retlist;
        }

        public List<Mas_Item_Go> GetAllMasterItems()
        {
            List<Mas_Item_Go> retlist = objDAL.GetAllMasteritemGo().ToList();
            return retlist;
        }

        public List<Mas_Storage_Go> GetAllMasterStorages()
        {
            List<Mas_Storage_Go> retlist = objDAL.GetAllMasterstorageGo().ToList();
            return retlist;
        }

        public List<Mas_Worktype_Go> GetAllMasterWorktypes()
        {
            List<Mas_Worktype_Go> retlist = objDAL.GetAllMasterworktypeGo().ToList();
            return retlist;
        }

        public List<Mas_Status_Go> GetAllMasterStatus()
        {
            List<Mas_Status_Go> retlist = objDAL.GetAllMasterstatusGo().ToList();
            return retlist;
        }

        public List<Mas_Reason_Go> GetAllMasterReasons()
        {
            List<Mas_Reason_Go> retlist = objDAL.GetAllMasterreasonGo().ToList();
            return retlist;
        }

        public Boolean ValidateMasterpallet(string spallet)
        {
            Boolean bret = false;
            bret = objDAL.ValidateMasterpallet(spallet);
            return bret;
        }


        public List<Mas_Cylinder_Go> GetAllCylinderGo()
        {
            List<Mas_Cylinder_Go> retlist = objDAL.GetAllCylinderGo().ToList();
            return retlist;
        }

        public List<Mas_Cylinder_Go> GetAllCylinderGobypallet(string pallet)
        {
            List<Mas_Cylinder_Go> retlist = objDAL.GetAllCylinderGobypallet(pallet).ToList();
            return retlist;
        }

        public List<Mas_Cylinder_Go> GetAllCylinderGobytag(string itemtag)
        {
            List<Mas_Cylinder_Go> retlist = objDAL.GetAllCylinderGobytag(itemtag).ToList();
            return retlist;
        }

        public List<Mas_Cylinderlist_Go> GetCylinderListGoByPallet(string pallet)
        {
            List<Mas_Cylinderlist_Go> retlist = objDAL.GetAllCylinderList().ToList();
            return retlist;
        }

        public List<Mas_Cylinderlist_Go> GetAllCylinderListGoByPallet(string pallet)
        {
            List<Mas_Cylinderlist_Go> retlist = objDAL.GetAllCylinderListByPallet(pallet).ToList();
            return retlist;
        }

        public async Task<string> InsertCylinderList(List<Mas_Cylinderlist_Go> listOrder, string mpallet)
        {
            bool bret;

            bret = await objDAL.InsertCylinderList(listOrder, mpallet);

            if (bret)
            {
                return "Insert Successfully";
            }
            else
            {
                return "Insert Fail";
            }
            
        }

        public async Task<string> InsertCylinderListMaster(List<Cylinder> listOrder, string mpallet)
        {
            bool bret;

            bret = await objDAL.InsertCylinderListMaster(listOrder, mpallet);

            if (bret)
            {
                return "Insert Successfully";
            }
            else
            {
                return "Insert Fail";
            }

        }




    }
}
