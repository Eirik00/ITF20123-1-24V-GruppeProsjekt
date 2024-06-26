﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    public class Outgoing : IOutgoing
    {
        private List<PackageList> outgoingPackagesList;
        private List<Pallet> readyForShipmentPallets = new List<Pallet>();
        private TruckManager truckManager;
        private int nextPalletId = 1;
        private Pallet currentPallet;

        //Events
        internal event EventHandler<OutgoingPackageEventArgs> OutgoingPackageEvent;
        internal event EventHandler<OutgoingPackageEventArgs> OutgoingDailyPackageEvent;
        internal event EventHandler<OutgoingPackageEventArgs> OutgoingWeeklyPackageEvent;

        internal event EventHandler<PalletEventArgs> PalletPreparedForShipment;
        internal event EventHandler<PalletEventArgs> PalletShipped;
        internal virtual void OnOutgoingPackage(OutgoingPackageEventArgs e)
        {
            OutgoingPackageEvent?.Invoke(this, e);
        }
        internal virtual void OnOutgoingDailyPackage(OutgoingPackageEventArgs e)
        {
            OutgoingDailyPackageEvent?.Invoke(this, e);
        }
        internal virtual void OnOutgoingWeeklyPackage(OutgoingPackageEventArgs e)
        {
            OutgoingWeeklyPackageEvent?.Invoke(this, e);
        }

        public Outgoing(TruckManager truckManager)
        {
            outgoingPackagesList = [];
            readyForShipmentPallets = new List<Pallet>();
            this.truckManager = truckManager;
            this.nextPalletId = 1;
            this.currentPallet = new Pallet(GetNextPalletId());

        }

        /// <summary>
        /// Outgoing packages with specified delivery information.
        /// </summary>
        /// <param name="sendingHourAndMinute">The time of the day the delivery is estimated to arrive to receiver in double hours&Minutes Eks: 14.33</param>
        /// <param name="packages">packages to send. The instance of the package class</param>
        /// <param name="sender">The sender's contact information. The object of the Contact or Company classes</param>
        /// <param name="receiver">The receiver's contact information. The object of the Contact or Company classes</param>
        public void OutgoingPackage(double sendingHourAndMinute, PackageList packages, Contact sender, Contact receiver, Employee worker)
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
                if(package.PackageAisle != null)
                {
                    package.PackageAisle.RemovePackage(package, worker);
                }
            }
            //This should delete the sent packet from the original list it was in, plain and simple
            //packages.Packages.Clear();
            //Console.WriteLine($"Vare Levering registrert for Kl. {sendingHourAndMinute} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
            OnOutgoingPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
        }
        // Overload for sending of 1 single package
        public void OutgoingPackage(double sendingHourAndMinute, Package package, Contact sender, Contact receiver, Employee worker)
        {
            PackageList singlePackageList = [];
            singlePackageList.Add(package);
            OutgoingPackage(sendingHourAndMinute, singlePackageList, sender, receiver, worker);
            OnOutgoingPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, package));
        }

        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingPackage(double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany, Employee worker)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;

            OutgoingPackage(sendingHourAndMinute, packages, sender, receiver, worker);
            OnOutgoingPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
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
            //This should delete the sent packet from the original list it was in, plain and simple
            packages.Packages.Clear();
            OnOutgoingDailyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
        }
        // Overload for sending of 1 single package
        public void OutgoingDailyPackage(double sendingHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            OutgoingDailyPackage(sendingHourAndMinute, singlePackageList, sender, receiver);
            OnOutgoingDailyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, package));
        }
        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingDailyPackage(double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;
            OutgoingDailyPackage(sendingHourAndMinute, packages, sender, receiver);
            OnOutgoingDailyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
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
            OnOutgoingWeeklyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
        }
        // Overload for sending of 1 single package
        public void OutgoingWeeklyPackage(DayOfWeek deliveryDay,double sendingHourAndMinute, Package package, Contact sender, Contact receiver)
        {
            PackageList singlePackageList = [package];
            singlePackageList.Add(package);
            OutgoingWeeklyPackage(deliveryDay,sendingHourAndMinute, singlePackageList, sender, receiver);
            OnOutgoingWeeklyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, package));
        }

        // Overload to be able to use Company object as a sender or receiver
        public void OutgoingWeeklyPackage(DayOfWeek deliveryDay, double sendingHourAndMinute, PackageList packages, Company senderCompany, Company receiverCompany)
        {
            Contact sender = senderCompany.ContactPerson;
            Contact receiver = receiverCompany.ContactPerson;
            OutgoingWeeklyPackage(deliveryDay, sendingHourAndMinute, packages, sender, receiver);
            OnOutgoingWeeklyPackage(new OutgoingPackageEventArgs(this, sender, receiver, sendingHourAndMinute, packages));
        }

        //---------------Pallets----------------

        /// <summary>
        /// Prepares pallets for shipment with the provided packages, sender, and receiver information.
        /// </summary>
        /// <param name="packages">The list of packages to prepare for shipment.</param>
        /// <param name="sender">The sender's contact information.</param>
        /// <param name="receiver">The receiver's contact information.</param>
        public void PreparePalletsForShipment(PackageList packages, Contact sender, Contact receiver)
        {
            if (packages == null || packages.Packages.Count == 0)
                throw new ArgumentNullException("packages", "Package list cannot be null or empty.");


            foreach (var package in packages.Packages)
            {
                if (currentPallet == null || currentPallet.PackageCount >= 30)
                {
                    if (currentPallet != null && currentPallet.PackageCount > 0)
                    {
                        readyForShipmentPallets.Add(currentPallet);
                    }
                    currentPallet = new Pallet(GetNextPalletId());
                }
            }
            if (currentPallet != null && currentPallet.PackageCount > 0)
            {
                readyForShipmentPallets.Add(currentPallet);
            }
            PalletPreparedForShipment?.Invoke(this, new PalletEventArgs { Message = "Pallets prepared for shipment." });
            currentPallet = null;
        }

        /// <summary>
        /// Tries to add a package to the current pallet.
        /// </summary>
        /// <param name="package">The package to add to the pallet.</param>
        /// <returns>True if the package was successfully added; otherwise, false.</returns>
        private bool TryAddPackageToPallet(Package package)
        {
            if (currentPallet == null)
                throw new InvalidOperationException("Current pallet is not initialized.");

            return currentPallet.AddPackage(package);
        }

        private void CreateNewPallet()
        {
            currentPallet = new Pallet(GetNextPalletId());
        }

        /// <summary>
        /// Finalizes the current pallet by setting sender and receiver information.
        /// </summary>
        /// <param name="sender">The sender's contact information.</param>
        /// <param name="receiver">The receiver's contact information.</param>
        private void FinalizeCurrentPallet(Contact sender, Contact receiver)
        {
            if (currentPallet == null)
                throw new InvalidOperationException("Current pallet is not initialized.");

            foreach (var package in currentPallet.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
            }

            readyForShipmentPallets.Add(currentPallet);
        }

        /// <summary>
        /// Ships out the prepared pallets from the sender to the receiver.
        /// </summary>
        /// <param name="sender">The sender's contact information.</param>
        /// <param name="receiver">The receiver's contact information.</param>
        public void ShipOutPallets(Contact sender, Contact receiver)
        {
            if (readyForShipmentPallets.Count == 0)
                throw new InvalidOperationException("No pallets are prepared for shipment.");

            foreach (var pallet in readyForShipmentPallets.ToList())
            {
                if (!truckManager.UseTruck()) break;

                Console.WriteLine($"Shipping out a pallet ID: {pallet.PalletId}, from {sender.FirstName} {sender.Surname} to {receiver.FirstName} {receiver.Surname}.");

                PalletShipped?.Invoke(this, new PalletEventArgs { Message = $"Pallet ID: {pallet.PalletId} shipped." });

                readyForShipmentPallets.Remove(pallet);
                truckManager.ReturnTruck();
            }
        }
        private int GetNextPalletId()
        {
            return nextPalletId++;
        }

        public int ReadyForShipmentPalletsCount => readyForShipmentPallets.Count;


        //---------------Pallets----------------


        //GetNextWeekDay metoden brukes i ukentlig sending,denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres sending den dagen til neste uke.
        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            if (day < DayOfWeek.Sunday || day > DayOfWeek.Saturday)
                throw new ArgumentException("Invalid day of the week.", nameof(day));

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
        public List<Pallet> ReadyForShipmentPallets => readyForShipmentPallets;
    }
}