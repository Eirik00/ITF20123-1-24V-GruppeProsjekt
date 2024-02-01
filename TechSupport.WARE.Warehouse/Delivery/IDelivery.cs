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
        /// <c>PackageDelivery</c> sets the time for a delivery with a List of packages. It also sets the Contact sender and Contact reciever for the delivery.
        /// </summary>
        void PackageDelivery(DateTime deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        /// <summary>
        /// 
        /// </summary>
        void RecurringDailyPackageDelivery(TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        /// <summary>
        /// 
        /// </summary>
        void RecurringWeeklyPackageDelivery(DayOfWeek[] deliveryDays, TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact Receiver);

        /// <summary>
        /// 
        /// </summary>
        String ToString();
    }
}

