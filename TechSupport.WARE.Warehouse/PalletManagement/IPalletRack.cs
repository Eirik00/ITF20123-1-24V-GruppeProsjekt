using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public interface IPalletRack
    {
        /// <summary>
        /// Adds a pallet to a specified rack and floor.
        /// </summary>
        /// <param name="rackNumber">The number of the rack.</param>
        /// <param name="floor">The floor number.</param>
        /// <param name="pallet">The pallet to add.</param>
        /// <returns>True if the pallet was successfully added; otherwise, false.</returns>
        bool AddPalletToRack(int rackNumber, int floor, IPallet pallet);

        /// <summary>
        /// Finds a pallet by its ID.
        /// </summary>
        /// <param name="id">The ID of the pallet to find.</param>
        /// <returns>The pallet if found; otherwise, null.</returns>
        IPallet FindPallet(int id);
    }
}
