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

        public Boolean CreatePotocalMC(string mccode, Int32 startpos, Int32 stoppos, Int32 unittyp, string palletid, Int32 weight, Int32 command)
        {
            Boolean bRet = false;
            bRet = objDAL.CreatePotocalMC( mccode,  startpos,  stoppos,  unittyp,  palletid,  weight,  command);
            return bRet;
        }


        public List<Vset_gate_rgv> GetGateRgv()
        {
            List<Vset_gate_rgv> retlist = objDAL.GetGateRgv().ToList();
            return retlist;
        }

    }
}
