﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    // Styrer tilgjengeligheten og planleggingen av trucker for flytting av paller.
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
        public TruckManager()
        {
            availableTrucks = totalTrucks;
        }
        
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

        public void ReturnTruck()
        {
            availableTrucks = Math.Min(totalTrucks, availableTrucks + 1);
            OnTruckUpdated(new TruckEventArgs { Action = "Returned" });
        }
        public int AvailableTrucks => availableTrucks;

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

        public void CheckScheduledTrucksAndReturn(DateTime currentTime)
        {
            if (!scheduledTrucks.ContainsKey(currentTime.Date)) return;
            int trucksToReturn = scheduledTrucks[currentTime.Date].Count;
            availableTrucks += trucksToReturn;
            scheduledTrucks.Remove(currentTime.Date);
        }
    }
}

// Define TruckEventArgs to pass event data
public class TruckEventArgs : EventArgs
{
    public string Action { get; set; }
}
