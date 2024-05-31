using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Manages the availability and scheduling of trucks for pallet movement.
    /// </summary>
    public class TruckManager : ITruckManager
    {
        private int totalTrucks = 2;
        private int availableTrucks;
        private Dictionary<DateTime, List<int>> scheduledTrucks = new Dictionary<DateTime, List<int>>();

        //events
        public event EventHandler<TruckEventArgs> TruckUpdated;

        protected virtual void OnTruckUpdated(TruckEventArgs e)
        {
            TruckUpdated?.Invoke(this, e);
        }
        //events end

        /// <summary>
        /// Initializes a new instance of the <see cref="TruckManager"/> class.
        /// </summary>
        public TruckManager()
        {
            availableTrucks = totalTrucks;
        }

        /// <summary>
        /// Attempts to use a truck for pallet movement.
        /// </summary>
        /// <returns>True if a truck is available and used; otherwise, false.</returns>
        public bool UseTruck()
        {
            if (availableTrucks > 0)
            {
                availableTrucks--;
                OnTruckUpdated(new TruckEventArgs { Action = "Used" });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a truck after pallet movement is completed.
        /// </summary>
        public void ReturnTruck()
        {
            availableTrucks = Math.Min(totalTrucks, availableTrucks + 1);
            OnTruckUpdated(new TruckEventArgs { Action = "Returned" });
        }
        public int AvailableTrucks => availableTrucks;

        /// <summary>
        /// Schedules a truck for a specific time.
        /// </summary>
        /// <param name="scheduleTime">The time to schedule the truck.</param>
        /// <returns>True if the truck is successfully scheduled; otherwise, false.</returns>
        public bool ScheduleTruck(DateTime scheduleTime)
        {
            if (availableTrucks <= 0) return false;
            availableTrucks--;
            if (!scheduledTrucks.ContainsKey(scheduleTime.Date))
                scheduledTrucks[scheduleTime.Date] = new List<int> { 1 };
            else
                scheduledTrucks[scheduleTime.Date].Add(1);
            return true;
        }

        /// <summary>
        /// Checks for scheduled trucks and returns them if their scheduled time has passed.
        /// </summary>
        /// <param name="currentTime">The current time to check against scheduled trucks.</param>
        public void CheckScheduledTrucksAndReturn(DateTime currentTime)
        {
            if (!scheduledTrucks.ContainsKey(currentTime.Date)) return;
            int trucksToReturn = scheduledTrucks[currentTime.Date].Count;
            availableTrucks += trucksToReturn;
            scheduledTrucks.Remove(currentTime.Date);
        }
    }
}

//TruckEventArgs to pass event data
public class TruckEventArgs : EventArgs
{
    public string Action { get; set; }
}
