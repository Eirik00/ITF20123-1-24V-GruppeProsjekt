﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    /// <summary>
    /// Defines event arguments for truck-related events.
    /// </summary>
    public class TruckHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TruckHandler"/> class.
        /// </summary>
        /// <param name="truckManager">The truck manager to handle events for.</param>
        public TruckHandler(TruckManager truckManager)
        {
            truckManager.TruckUpdated += HandleTruckUpdated;
        }

        /// <summary>
        /// Handles the event when a truck is updated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the update.</param>
        private void HandleTruckUpdated(object sender, TruckEventArgs e)
        {
            Console.WriteLine($"Truck update: Action = {e.Action}");
        }
    }
}
