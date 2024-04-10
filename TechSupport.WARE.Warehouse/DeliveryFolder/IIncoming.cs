using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IIncoming
    {
        List<Package> IncomingPackagesList { get; }

        /// <summary>
        /// Adds packages for a single(None daily/weekly recurring) import.It sets the time for a delivery and It also sets the Contact sender and Contact reciever for the delivery.
        /// </summary>
        /// <param name="deliveryTime">The time the delivery is to arrive</param>
        /// <param name="packages">The list of packages to be imported.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void IncomingPackage(double deliveryTime, PackageList packages, Contact sender, Contact receiver);

        /// <summary>
        /// Sets up daily recurring import for a list of packages.It takes a delivery hour, sender, and receiver, and schedules deliveries for each day based on the current date.
        /// </summary>
        /// <param name="deliveryHour">The time the import item to arrive is to be made everyday</param>
        /// <param name="packages">The list packages to be imported.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void IncomingDailyPackage(double deliveryHour, PackageList packages, Contact sender , Contact receiver);

        /// <summary>
        /// Sets up weekly recurring imports for a list of packages.
        /// Functions same as RecurringDailyImports,but on a weekly basis,It allows users to specify which day of the week deliveries are to occur.
        /// </summary>
        /// <param name="deliveryDay">The days of the week for imports.</param>
        /// <param name="deliveryHour">The time of the day for import to arrive.</param>
        /// <param name="packages">The list of packages to arrive.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void IncomingWeeklyPackage(DayOfWeek deliveryDay, double deliveryHour, PackageList packages, Contact sender, Contact receiver);

        /// <summary>
        /// 
        /// </summary>
        String ToString();
    }
}

