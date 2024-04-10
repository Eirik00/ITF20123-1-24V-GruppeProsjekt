using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public class TruckHandler
    {
        public TruckHandler(TruckManager truckManager)
        {
            truckManager.TruckUpdated += HandleTruckUpdated;
        }

        private void HandleTruckUpdated(object sender, TruckEventArgs e)
        {
            Console.WriteLine($"Truck update: Action = {e.Action}");
        }
    }
}
