using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Import : IImport
    {
        private List<Package> importPackagesList;

        public Import()
        {
            importPackagesList = [];
        }

        /// <summary>
        /// Imports packages with the specified delivery time, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryTime">The time of delivery.</param>
        /// <param name="packages">The list of packages to import.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void PackageImport(double deliveryTime, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryTime);
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Vare Mottak registerert for Kl {deliveryTime} av sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
        }

        /// <summary>
        /// Imports packages with daily recurrence at the specified delivery hour, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryHour">The hour of delivery.</param>
        /// <param name="packages">The list of packages to import.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void DailyPackageImport(double deliveryHour, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                DateTime deliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Daglig Vare Mottak Registrert for Kl {deliveryHour} fra sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
        }

        /// <summary>
        /// Imports packages with weekly recurrence on the specified delivery day and hour, with sender and receiver information.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for delivery.</param>
        /// <param name="deliveryHour">The hour of delivery.</param>
        /// <param name="packages">The list of packages to import.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void WeeklyPackageImport(DayOfWeek deliveryDay, double deliveryHour,PackageList packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Ukentlig Vare Mottak Registrert for {deliveryDay} Kl. {deliveryHour} av sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        //Denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres mottal til neste forekomst av den dagen i neste uke.
        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override string ToString()
        {
            StringBuilder importDetails = new();
            importDetails.AppendLine("Motatte Varer:");

            foreach (var package in ImportPackagesList)
            {
                importDetails.AppendLine(package.ToString());
                importDetails.AppendLine("Status Change Log:");
                foreach (var statusChange in package.GetPackageLog().GetEntries())
                {
                    importDetails.AppendLine($"  - Previous Status: {statusChange.GetPreviousStatus()}, New Status: {statusChange.GetNewStatus()}, Time: {statusChange.GetDateTime()}");
                }
                importDetails.AppendLine();
            }

            return importDetails.ToString();
        }


        public List<Package> ImportPackagesList => importPackagesList;
    }
}