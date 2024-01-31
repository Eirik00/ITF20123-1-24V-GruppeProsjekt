using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IDelivery
    {
        List<Package> DeliveryPackageList { get; }

        /// <summary>
        /// Updates
        /// </summary>
        void PackageDelivery(DateTime deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        void RecurringDailyPackageDelivery(TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        void RecurringWeeklyPackageDelivery(DayOfWeek[] deliveryDays, TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact Receiver);

        String ToString();
    }
}

