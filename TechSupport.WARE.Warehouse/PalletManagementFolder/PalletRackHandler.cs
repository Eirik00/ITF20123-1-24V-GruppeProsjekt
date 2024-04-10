using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public class PalletRackHandler
    {
        public PalletRackHandler(PalletRack palletRack)
        {
            palletRack.RackUpdated += HandleRackUpdated;
        }

        private void HandleRackUpdated(object sender, PalletRackEventArgs e)
        {
            Console.WriteLine($"Rack update: Rack Number = {e.RackNumber}, Floor = {e.Floor}, Action = {e.Action}, Pallet ID = {e.PalletId}");
        }
    }
}
