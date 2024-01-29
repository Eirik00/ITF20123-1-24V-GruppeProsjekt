using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public interface IDelivery
    {
        List<Package> DeliveryPackageList { get; }
        void PackageDelivery(DateTime deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        void RecurringDailyPackageDelivery(TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact receiver);

        void RecurringWeeklyPackageDelivery(DayOfWeek[] deliveryDays, TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact Receiver);
    }
}