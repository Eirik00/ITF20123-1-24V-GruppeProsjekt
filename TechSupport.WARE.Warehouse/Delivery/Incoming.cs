using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Incoming : IIncoming
    {
        private List<Package> incomingPackagesList;

        public Incoming()
        {
            incomingPackagesList = [];
        }

        /// <summary>
        /// Receives packages with the specified delivery time, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryHourAndMinute">The time of delivery in double eks: 16.33</param>
        /// <param name="packages">The list of packages to be received.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void IncomingPackage(double deliveryHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHourAndMinute);
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            Console.WriteLine($"Vare Mottak registerert for Kl {deliveryHourAndMinute} av sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
        }

        /// <summary>
        /// Receives packages with daily recurrence at the specified delivery hour, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryHourAndMinute">The hour of delivery in double eks: 13.45</param>
        /// <param name="packages">The list of packages to receive.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void IncomingDailyPackage(double deliveryHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                DateTime deliveryTime = DateTime.Today.AddHours(deliveryHourAndMinute);
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Daglig Vare Mottak Registrert for Kl {deliveryHourAndMinute} fra sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
        }

        public void DailyPackageImport(double deliveryHour, Package package, Contact sender, Contact receiver)
        {
                package.Sender = sender;
                package.Receiver = receiver;
                DateTime deliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            
            Console.WriteLine($"Gjentagende Daglig Vare Mottak Registrert for Kl {deliveryHour} fra sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
        }

        /// <summary>
        /// Receives packages with weekly recurrence on the specified delivery day and hour, with sender and receiver information.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for delivery </param>
        /// <param name="deliveryHourAndMinute">The hour of delivery.</param>
        /// <param name="packages">The list of packages to import.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void IncomingWeeklyPackage(DayOfWeek deliveryDay, double deliveryHourAndMinute,PackageList packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHourAndMinute);

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Ukentlig Vare Mottak Registrert for {deliveryDay} Kl. {deliveryHourAndMinute} av sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        public void WeeklyPackageImport(DayOfWeek deliveryDay, double deliveryHour, Package package, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            
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
            StringBuilder receivingDetails = new();
            receivingDetails.AppendLine("Motatte Varer:");

            foreach (var package in IncomingPackagesList)
            {
                receivingDetails.AppendLine(package.ToString());
                receivingDetails.AppendLine("Status Change Log:");
                foreach (var statusChange in package.GetPackageLog().GetEntries())
                {
                    receivingDetails.AppendLine($"  - Previous Status: {statusChange.GetPreviousStatus()}, New Status: {statusChange.GetNewStatus()}, Time: {statusChange.GetDateTime()}");
                }
                receivingDetails.AppendLine();
            }

            return receivingDetails.ToString();
        }


        public List<Package> IncomingPackagesList => incomingPackagesList;
    }
}