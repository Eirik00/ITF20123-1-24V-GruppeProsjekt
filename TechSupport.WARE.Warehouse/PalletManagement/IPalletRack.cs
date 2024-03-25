using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public interface IPalletRack
    {
        bool AddPalletToRack(int rackNumber, int floor, IPallet pallet);
        IPallet FindPallet(int id);
    }
}
