using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public class PalletHandler
    {
        public PalletHandler(Pallet pallet)
        {
            pallet.PalletUpdated += HandlePalletUpdated;
        }

        private void HandlePalletUpdated(object sender, PalletEventArgs e)
        {
            Console.WriteLine($"Pallet update: Action = {e.Action}, Package ID = {e.PackageId}");
        }
    }
}
