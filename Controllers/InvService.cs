using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using GoWMS.Server.Models;
using GoWMS.Server.Models.Inv;


namespace GoWMS.Server.Controllers
{
    public class InvService
    {
        readonly InvDAL objDAL = new InvDAL();

        public List<InvStockList> GetStckList()
        {
            List<InvStockList> ListRet = objDAL.GetStockList().ToList();
            return ListRet;
        }

        public List<InvStockList> GetStockListCuRoom()
        {
            List<InvStockList> ListRet = objDAL.GetStockListCuRoom().ToList();
            return ListRet;
        }

        
        public List<InvStockSum> GetStockSum()
        {
            List<InvStockSum> ListRet = objDAL.GetStockSum().ToList();
            return ListRet;
        }

        public List<InvStockSum> GetStockSumCuRoom()
        {
            List<InvStockSum> ListRet = objDAL.GetStockSumCuRoom().ToList();
            return ListRet;
        }

        public List<Vrpt_shelf_listInfo> GetShelfList()
        {
            List<Vrpt_shelf_listInfo> ListRet = objDAL.GetShelfLocation().ToList();
            return ListRet;
        }

        public List<Vrpt_shelf_listInfo> GetShelfLocationCuRoom()
        {
            List<Vrpt_shelf_listInfo> ListRet = objDAL.GetShelfLocationCuRoom().ToList();
            return ListRet;
        }

        public Task<IEnumerable<Inv_Stock_GoInfo>> GetStockListInfo()
        {
            return objDAL.GetStockListInfo();
        }

        public async Task UpdateHoldStock(List<InvStockList> liststock)
        {
            await objDAL.UpdateHoldStock(liststock);
        }

        public async Task UpdateReleaseStock(List<InvStockList> liststock)
        {
            await objDAL.UpdateReleaseStock(liststock);
        }

        public async Task ClearRack(Int32 iStatus, string sPallet, string sBin)
        {
            await objDAL.ClearRack(iStatus, sPallet, sBin);
        }

        public async Task ClearStockCuroom(Int32 iStatus, string sPallet, string sBin)
        {
            await objDAL.ClearStockCuroom(iStatus, sPallet, sBin);
        }

        

        public async Task PickingRack(string sPallet, string sBin)
        {

        }

        public async Task BookkingRack(string sPallet, string sBin)
        {
            await objDAL.BookkingRack(sPallet, sBin);
        }


    }
}
