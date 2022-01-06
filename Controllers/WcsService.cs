using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Models.Wcs;
using GoWMS.Server.Data;

namespace GoWMS.Server.Controllers
{
    public class WcsService
    {
        readonly WcsDAL objDAL = new WcsDAL();

        public List<Vmachine> GetAllMachine()
        {
            List<Vmachine> retlist = objDAL.GetAllMachine().ToList();
            return retlist;
        }

        public List<Vmachine_command> GetCommandMachine(string mccode)
        {
            List<Vmachine_command> retlist = objDAL.GetCommandMachine(mccode).ToList();
            return retlist;
        }
        public Boolean CreateCommandMC(string mccode, Int32 command)
        {
            Boolean bRet = false;
            bRet = objDAL.CreateCommandMC(mccode, command);
            return bRet;
        }




    }
}
