using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public interface ITruckManager
    {
        /// <summary>
        /// Attempts to use a truck for pallet movement.
        /// </summary>
        /// <returns>True if a truck is available and used; otherwise, false.</returns>
        bool UseTruck();

        /// <summary>
        /// Returns a truck after pallet movement is completed.
        /// </summary>
        void ReturnTruck();

        /// <summary>
        /// Gets the number of available trucks.
        /// </summary>
        int AvailableTrucks { get; }

        /// <summary>
        /// Schedules a truck for a specific time.
        /// </summary>
        /// <param name="scheduleTime">The time to schedule the truck.</param>
        /// <returns>True if the truck is successfully scheduled; otherwise, false.</returns>
        bool ScheduleTruck(DateTime scheduleTime);

        /// <summary>
        /// Checks for scheduled trucks and returns them if their scheduled time has passed.
        /// </summary>
        /// <param name="currentTime">The current time to check against scheduled trucks.</param>
        void CheckScheduledTrucksAndReturn(DateTime currentTime);
    }
}
