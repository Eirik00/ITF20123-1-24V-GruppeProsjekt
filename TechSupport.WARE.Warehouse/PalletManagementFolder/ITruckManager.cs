using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public interface ITruckManager
    {
        bool UseTruck();
        void ReturnTruck();
        int AvailableTrucks { get; }
        bool ScheduleTruck(DateTime scheduleTime);
        void CheckScheduledTrucksAndReturn(DateTime currentTime);
    }
}
