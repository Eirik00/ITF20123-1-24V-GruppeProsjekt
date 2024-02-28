using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Outgoing : IOutgoing
    {
        //Packages to be sent are added to this list
        private List<PackageList> outgoingPackagesList;

        public Outgoing()
        {
            outgoingPackagesList = [];
        }

        /// <summary>
        /// Outgoing packages with specified delivery information.
        /// </summary>
        /// <param name="sendingHourAndMinute">The time of the day the delivery is estimated to arrive to receiver in double hours&Minutes Eks: 14.33</param>
        /// <param name="packages">packages to send.</param>
        /// <param name="sender">The sender's contact information.</param>
        /// <param name="receiver">The receiver's contact information.</param>
        public void OutgoingPackage(double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(sendingHourAndMinute);
                package.ChangeStatus(StatusList.Delivery);
                OutgoingPackagesList.Add(packages);    
            }
            //This should delete the sent packet from the original list it was in, plain and simple
            //packages.Packages.Clear();
            Console.WriteLine($"Vare Levering registrert for Kl. {sendingHourAndMinute} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        /// <summary>
        /// Registers a recurring daily sending of packages.
        /// </summary>
        /// <param name="sendingHourAndMinute">The time of the day the delivery is meant to arrive in hours&minute eks 13.44</param>
        /// <param name="packages">The packages to send</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void RecurringDailyOutgoing(double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(sendingHourAndMinute);
                package.ChangeStatus(StatusList.Delivery);
                OutgoingPackagesList.Add(packages);

                
            }
            Console.WriteLine($"Gjentagende Daglig Outgoing Registrert for Kl. {sendingHourAndMinute}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
            //This should delete the sent packet from the original list it was in, plain and simple
            packages.Packages.Clear();
        }

        /// <summary>
        /// Registers a recurring weekly sending of packages.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for the delivery Eks: DayOfWeek.Wednesday </param>
        /// <param name="sendingHourAndMinute">The hour of delivery in double eks 14.35</param>
        /// <param name="packages">The packages to send.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void RecurringWeeklyOutgoing(DayOfWeek deliveryDay, double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(sendingHourAndMinute);

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                OutgoingPackagesList.Add(packages);
            }
            //This should delete the sent packet from the original list it was in, plain and simple
            packages.Packages.Clear();
            Console.WriteLine($"Gjentagende Ukentlig Vare Outgoing Registrert for {deliveryDay} Kl {sendingHourAndMinute}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }


        //GetNextWeekDay metoden brukes i ukentlig sending,denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres sending den dagen til neste uke.
        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override string ToString()
        {
            StringBuilder SendingDetails = new();
            SendingDetails.AppendLine("Leverte Varer:");

            foreach (var packageList in OutgoingPackagesList)
            {
                foreach (var package in packageList.Packages)
                {
                    SendingDetails.AppendLine(package.ToString());
                    SendingDetails.AppendLine("Status Change Log:");
                    foreach (var statusChange in package.GetPackageLog().GetEntries())
                    {
                        SendingDetails.AppendLine($"  - Previous Status: {statusChange.GetPreviousStatus()}, New Status: {statusChange.GetNewStatus()}, Time: {statusChange.GetDateTime()}");
                    }
                    SendingDetails.AppendLine();
                }
            }

            return SendingDetails.ToString();
        }


        public List<PackageList> OutgoingPackagesList => outgoingPackagesList;
    }
}