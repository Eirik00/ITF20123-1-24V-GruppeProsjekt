using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IOutgoing
    {
        /// <summary>
        /// List of packages that are scheduled for delivery.
        /// </summary>
        List<PackageList> OutgoingPackagesList { get; }

        /// <summary>
        /// Adds packages for a single(None daily/weekly recurring) delivery.It sets the time for a delivery and It also sets the Contact sender and Contact reciever for the delivery.
        /// </summary>
        /// <param name="deliveryTime">The time the delivery is made in hours and minutes eks: 14.35</param>
        /// <param name="packages">The list of packages to be delivered.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void OutgoingPackage(double deliveryTime, PackageList packages, Contact sender, Contact receiver);

        /// <summary>
        /// Sets up daily recurring delivery for a list of packages.It takes a delivery hour, sender, and receiver, and schedules deliveries for each day based on the current date.
        /// </summary>
        /// <param name="deliveryHour">The time the delivery is to be made everyday in double eks: 13.44</param>
        /// <param name="packages">The list packages to be delivered.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void RecurringDailyOutgoing(double deliveryHour,PackageList packages, Contact sender, Contact receiver);

        /// <summary>
        /// Sets up weekly recurring delivery for a list of packages.
        /// Functions same as RecurringDailyOutgoing,but on a weekly basis,It allows users to specify which day of the week deliveries are to occur.
        /// </summary>
        /// <param name="deliveryDay">The days of the week for delivery Eks: DayOfWeek.Monday</param>
        /// <param name="deliveryTime">The time of the day for delivery in double Eks: 13.44</param>
        /// <param name="packages">The list of packages to be delivered.</param>
        /// <param name="sender">The sender of the packages.</param>
        /// <param name="receiver">The receiver of the packages.</param>
        void RecurringWeeklyOutgoing(DayOfWeek deliveryDay, double deliveryTime, PackageList packages, Contact sender, Contact receiver);

        /// <summary>
        /// Is ment to convert the delivery information into more easly readlable string format, but might actually do the opposite due to reasons
        /// </summary>
        /// <returns>A string representation of the planned deliveries.</returns>
        //String ToString();
    }
}

