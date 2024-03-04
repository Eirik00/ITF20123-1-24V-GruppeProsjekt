using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.Exceptions;

namespace TechSupport.WARE.Warehouse
{
    public class Outgoing : IOutgoing
    {
        private List<PackageList> outgoingPackagesList;
        //Event
        public delegate void PackageHandler(object sender, EventArgs e);
        public event EventHandler<PackageList> NewPackageSent;
        public virtual void OnPackageSent(PackageList e)
        {
            NewPackageSent?.Invoke(this, e);
        }

        public Outgoing()
        {
            outgoingPackagesList = [];
        }

        /// <summary>
        /// Outgoing packages with specified delivery information.
        /// </summary>
        /// <param name="sendingHourAndMinute">The time of the day the delivery is estimated to arrive to receiver in double hours&Minutes Eks: 14.33</param>
        /// <param name="packages">packages to send. The instance of the package class</param>
        /// <param name="sender">The sender's contact information. The object of the Contact or Company classes</param>
        /// <param name="receiver">The receiver's contact information. The object of the Contact or Company classes</param>
        public void OutgoingPackage(double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(sendingHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery hour and minute must be a valid double.");

            if (sendingHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Sending hour and minute cannot exceed 4 digits.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender's or receiver's information is incomplete or invalid.");

            if (packages == null || !packages.Packages.Any())
                throw new InvalidPackageListException("Invalid or empty package list.");

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
            //Console.WriteLine($"Vare Levering registrert for Kl. {sendingHourAndMinute} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
            OnPackageSent(packages);
        }
        // Overload for sending of 1 single package
        public void OutgoingPackage(double sendingHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            OutgoingPackage(sendingHourAndMinute, singlePackageList, sender, receiver);
        }

        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingPackage(double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;

            OutgoingPackage(sendingHourAndMinute, packages, sender, receiver);
        }

        /// <summary>
        /// Registers a recurring daily sending of packages.
        /// </summary>
        /// <param name="sendingHourAndMinute">The time of the day the delivery is meant to arrive in hours&minute eks 13.44</param>
        /// <param name="packages">The packages to send. The Instance of the package class</param>
        /// <param name="sender">The contact information of the sender. The object of the Contact or Company classes</param>
        /// <param name="receiver">The contact information of the receiver. The object of the Contact or Company classes</param>
        public void OutgoingDailyPackage(double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(sendingHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery hour and minute must be a valid double.");

            if (sendingHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Sending hour and minute cannot exceed 4 digits.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender's or receiver's information is incomplete or invalid.");

            if (packages == null || !packages.Packages.Any())
                throw new InvalidPackageListException("Invalid or empty package list.");

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
        // Overload for sending of 1 single package
        public void OutgoingDailyPackage(double sendingHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            OutgoingDailyPackage(sendingHourAndMinute, singlePackageList, sender, receiver);
        }
        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingDailyPackage(double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;

            OutgoingDailyPackage(sendingHourAndMinute, packages, sender, receiver);
        }

        /// <summary>
        /// Registers a recurring weekly sending of packages.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for the delivery Eks: DayOfWeek.Wednesday </param>
        /// <param name="sendingHourAndMinute">The hour of delivery in double eks 14.35</param>
        /// <param name="packages">The packages to send. The Instance of the package class</param>
        /// <param name="sender">The contact information of the sender. The object of the Contact or Company classes</param>
        /// <param name="receiver">The contact information of the receiver. The object of the Contact or Company classes</param>
        public void OutgoingWeeklyPackage(DayOfWeek deliveryDay, double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(sendingHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery hour and minute must be a valid double.");

            if (sendingHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Sending hour and minute cannot exceed 4 digits.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender's or receiver's information is incomplete or invalid.");

            if (packages == null || !packages.Packages.Any())
                throw new InvalidPackageListException("Invalid or empty package list.");

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
        // Overload for sending of 1 single package
        public void OutgoingWeeklyPackage(DayOfWeek deliveryDay,double sendingHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            OutgoingWeeklyPackage(deliveryDay,sendingHourAndMinute, singlePackageList, sender, receiver);
        }

        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingWeeklyPackage(DayOfWeek deliveryDay, double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;

            OutgoingWeeklyPackage(deliveryDay, sendingHourAndMinute, packages, sender, receiver);
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