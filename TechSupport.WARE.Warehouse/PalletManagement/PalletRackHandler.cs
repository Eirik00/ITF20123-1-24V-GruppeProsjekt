using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Handles events related to pallet rack updates.
    /// </summary>
    public class PalletRackHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PalletRackHandler"/> class.
        /// </summary>
        /// <param name="palletRack">The pallet rack to handle events for.</param>
        public PalletRackHandler(PalletRack palletRack)
        {
            palletRack.RackUpdated += HandleRackUpdated;
        }

        /// <summary>
        /// Handles the event when a pallet rack is updated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the update.</param>
        private void HandleRackUpdated(object sender, PalletRackEventArgs e)
        {
            Console.WriteLine($"Rack update: Rack Number = {e.RackNumber}, Floor = {e.Floor}, Action = {e.Action}, Pallet ID = {e.PalletId}");
        }
    }
}
