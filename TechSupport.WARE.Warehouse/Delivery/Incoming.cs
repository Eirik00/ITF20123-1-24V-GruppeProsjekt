﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    public class Incoming : IIncoming
    {
        private List<Package> incomingPackagesList;
        //Events
        internal event EventHandler<IncomingPackageEventArgs>? IncomingPackageEvent;
        internal event EventHandler<IncomingPackageEventArgs>? IncomingDailyPackageEvent;
        internal event EventHandler<IncomingPackageEventArgs>? IncomingWeeklyPackageEvent;
        internal virtual void OnIncomingPackage(IncomingPackageEventArgs e)
        {
            IncomingPackageEvent?.Invoke(this, e);
        }
        internal virtual void OnIncomingDailyPackage(IncomingPackageEventArgs e)
        {
            IncomingDailyPackageEvent?.Invoke(this, e);
        }
        internal virtual void OnIncomingWeeklyPackage(IncomingPackageEventArgs e)
        {
            IncomingWeeklyPackageEvent?.Invoke(this, e);
        }

        public Incoming()
        {
            incomingPackagesList = new List<Package>();
        }

        /// <summary>
        /// Receives packages with the specified delivery time, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryHourAndMinute">The time of delivery in double eks: 16.33</param>
        /// <param name="packages">The list of packages to be received.The instances of the package class that you have created.</param>
        /// <param name="sender">The contact information of the sender. The object of the Contact or Company classes.</param>
        /// <param name="receiver">The contact information of the receiver.The object of the Contact or Company classes</param>
        public void IncomingPackage(double deliveryHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(deliveryHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery Time for packages must be in hour and minutes and must be a valid double with a .(period).");

            if (deliveryHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Delivery hour and minute cannot exceed 4 digits.");

            if (deliveryHourAndMinute < 0 || deliveryHourAndMinute >= 24)
                throw new ArgumentOutOfRangeException(nameof(deliveryHourAndMinute), "Delivery hour and minute must be between 0 and 23.59.");

            if (string.IsNullOrEmpty(sender.Address) || string.IsNullOrEmpty(sender.Email) ||
                string.IsNullOrEmpty(receiver.Address) || string.IsNullOrEmpty(receiver.Email))
                throw new ArgumentException("Sender and receiver must have valid address and email.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender or receiver information is incomplete or invalid.");

            if (packages == null || packages.Count == 0)
                throw new InvalidPackageListException("Invalid or empty package list.");

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHourAndMinute);
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            //Console.WriteLine($"Vare Mottak registerert for Kl {deliveryHourAndMinute} av sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}");
            OnIncomingPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }
        // Overload for receiving of 1 single package
        public void IncomingPackage(double deliveryHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [];
            singlePackageList.Add(package);
            IncomingPackage(deliveryHourAndMinute, singlePackageList, sender, receiver);
            OnIncomingPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, package));
        }

        // Overload to be able to use Company object as a sender or receiver
        public void IncomingPackage(double deliveryHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;
            IncomingPackage(deliveryHourAndMinute, packages, sender, receiver);
            OnIncomingPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }

        /// <summary>
        /// Receives packages with daily recurrence at the specified delivery hour, sender, and receiver information.
        /// </summary>
        /// <param name="deliveryHourAndMinute">The hour of delivery in double eks: 13.45</param>
        /// <param name="packages">The list of packages to receive. The instance of the package class</param>
        /// <param name="sender">The contact information of the sender.The object of the Contact or Company classes</param>
        /// <param name="receiver">The contact information of the receiver.The object of the Contact or Company classes</param>
        public void IncomingDailyPackage(double deliveryHourAndMinute, PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(deliveryHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery Time for packages must be in hour and minutes and must be a valid double with a .(period).");

            if (deliveryHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Delivery hour and minute cannot exceed 4 digits.");

            if (deliveryHourAndMinute < 0 || deliveryHourAndMinute >= 24)
                throw new ArgumentOutOfRangeException(nameof(deliveryHourAndMinute), "Delivery hour and minute must be between 0 and 23.59.");

            if (string.IsNullOrEmpty(sender.Address) || string.IsNullOrEmpty(sender.Email) ||
                string.IsNullOrEmpty(receiver.Address) || string.IsNullOrEmpty(receiver.Email))
                throw new ArgumentException("Sender and receiver must have valid address and email.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender or receiver information is incomplete or invalid.");

            if (packages == null || packages.Count == 0)
                throw new InvalidPackageListException("Invalid or empty package list.");

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                DateTime deliveryTime = DateTime.Today.AddHours(deliveryHourAndMinute);
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            OnIncomingDailyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }
        // Overload for receiving of 1 single package
        public void IncomingDailyPackage(double deliveryHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            IncomingDailyPackage(deliveryHourAndMinute, singlePackageList, sender, receiver);
            OnIncomingDailyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, package));
        }

        // Overload to be able to use Company object as a sender or receiver
        public void IncomingDailyPackage(double deliveryHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;
            IncomingDailyPackage(deliveryHourAndMinute, packages, sender, receiver);
            OnIncomingDailyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }

        /// <summary>
        /// Receives packages with weekly recurrence on the specified delivery day and hour, with sender and receiver information.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for delivery </param>
        /// <param name="deliveryHourAndMinute">The hour of delivery. For eks: 14.33</param>
        /// <param name="packages">The list of packages to receive. The instance of the package class</param>
        /// <param name="sender">The contact information of the sender. The object of the Contact or Company classes</param>
        /// <param name="receiver">The contact information of the receiver. The object of the Contact or Company classes</param>
        public void IncomingWeeklyPackage(DayOfWeek deliveryDay, double deliveryHourAndMinute,PackageList packages, Contact sender, Contact receiver)
        {
            if (!double.TryParse(deliveryHourAndMinute.ToString(), out double _))
                throw new ArgumentException("Delivery Time for packages must be in hour and minutes and must be a valid double with a .(period).");

            if (deliveryHourAndMinute.ToString().Length > 4)
                throw new ArgumentException("Delivery hour and minute cannot exceed 4 digits.");

            if (deliveryHourAndMinute < 0 || deliveryHourAndMinute >= 24)
                throw new ArgumentOutOfRangeException(nameof(deliveryHourAndMinute), "Delivery hour and minute must be between 0 and 23.59.");

            if (string.IsNullOrEmpty(sender.Address) || string.IsNullOrEmpty(sender.Email) ||
                string.IsNullOrEmpty(receiver.Address) || string.IsNullOrEmpty(receiver.Email))
                throw new ArgumentException("Sender and receiver must have valid address and email.");

            if (sender == null || receiver == null || string.IsNullOrEmpty(sender.FirstName) || string.IsNullOrEmpty(sender.Surname) ||
                string.IsNullOrEmpty(receiver.FirstName) || string.IsNullOrEmpty(receiver.Surname))
                throw new InvalidContactCompanyInfoException("Sender or receiver information is incomplete or invalid.");

            if (packages == null || packages.Count == 0)
                throw new InvalidPackageListException("Invalid or empty package list.");

            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHourAndMinute);

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Ordered);
                IncomingPackagesList.Add(package);
            }
            OnIncomingWeeklyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }

        // Overload for receiving of 1 single package
        public void IncomingWeeklyPackage(DayOfWeek deliveryDay, double deliveryHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            IncomingWeeklyPackage(deliveryDay, deliveryHourAndMinute, singlePackageList, sender, receiver);
            OnIncomingWeeklyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, package));
        }

        // Overload to be able to use Company object as a sender or receiver
        public void IncomingWeeklyPackage(DayOfWeek deliveryDay, double deliveryHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;
            IncomingWeeklyPackage(deliveryDay, deliveryHourAndMinute, packages, sender, receiver);
            OnIncomingWeeklyPackage(new IncomingPackageEventArgs(this, sender, receiver, deliveryHourAndMinute, packages));
        }

        //------------Pallets Start-------------------


        //------NoYetImplemented--------

        //------------Pallets end-------------------

        //Denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres mottal til neste forekomst av den dagen i neste uke.
        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {

            if (day < DayOfWeek.Sunday || day > DayOfWeek.Saturday)
                throw new ArgumentException("Invalid day of the week.", nameof(day));

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