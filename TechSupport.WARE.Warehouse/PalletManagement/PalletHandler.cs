using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Handles events related to pallet updates.
    /// </summary>
    public class PalletHandler
    {
        /// <summary>
        /// Initializes a new instance of the PalletHandler class.
        /// </summary>
        /// <param name="pallet">The pallet to handle events for.</param>
        public PalletHandler(Pallet pallet)
        {
            pallet.PalletUpdated += HandlePalletUpdated;
        }

        private void HandlePalletUpdated(object sender, PalletsEventArgs e)
        {
            Console.WriteLine($"Pallet update: Action = {e.PalletAction}, Package ID = {e.PackageId}");
        }
    }
}
