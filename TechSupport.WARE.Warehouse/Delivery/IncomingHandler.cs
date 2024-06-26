﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents a handler for incoming packages.
    /// </summary>
    public class IncomingHandler
    {
        public IncomingHandler(Incoming incoming)
        {
            incoming.IncomingPackageEvent += HandleIncomingPackage;
            incoming.IncomingDailyPackageEvent += HandleIncomingDailyPackage;
            incoming.IncomingWeeklyPackageEvent += HandleIncomingWeeklyPackage;
            HandleIncoming(this);
        }
        internal void HandleIncoming(object sender)
        {
            Console.WriteLine($"Incoming added to the handler.");
        }

        internal void HandleIncomingPackage(object sender, IncomingPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageIds} ordered to goods receipt. Registered for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated incoming list: Package with ID {e.UpdatedIncomingList}");
        }
        internal void HandleIncomingDailyPackage(object sender, IncomingPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageIds} ordered daily to goods receipt. Registered daily for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated incoming list: Package with ID {e.UpdatedIncomingList}");
        }

        internal void HandleIncomingWeeklyPackage(object sender, IncomingPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageIds} ordered weekly to goods receipt. Registered weekly for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated incoming list: Package with ID {e.UpdatedIncomingList}");
        }
    }

    internal class IncomingPackageEventArgs : EventArgs
    {
        internal string Sender { get; }
        internal string Reciever { get; }
        internal double DeliveryHourAndMinute { get; }
        internal string PackageIds { get; }
        internal string UpdatedIncomingList { get; }
        
        internal IncomingPackageEventArgs(Incoming incoming, Contact sender, Contact reciever, double deliveryTime, PackageList packages)
        {
            Sender = $"{sender.FirstName} {sender.Surname}";
            Reciever = $"{reciever.FirstName} {reciever.Surname}";
            DeliveryHourAndMinute = deliveryTime;
            PackageIds = PackageList(packages);
            UpdatedIncomingList = ListPackages(incoming.IncomingPackagesList);
        }
        internal IncomingPackageEventArgs(Incoming incoming, Contact sender, Contact reciever, double deliveryTime, Package package)
        {
            Sender = $"{sender.FirstName} {sender.Surname}";
            Reciever = $"{reciever.FirstName} {reciever.Surname}";
            DeliveryHourAndMinute = deliveryTime;
            PackageIds = $"{package.PackageId}";
            UpdatedIncomingList = ListPackages(incoming.IncomingPackagesList);
        }
        internal string PackageList(PackageList packages)
        {
            StringBuilder packagePrint = new StringBuilder();
            foreach (Package package in packages)
            {
                packagePrint.Append(package.PackageId.ToString());
                if (packages.IndexOf(package) < packages.Count - 1)
                {
                    packagePrint.Append(", ");
                }
            }
            return packagePrint.ToString();
        }

        internal string ListPackages(List<Package> packages)
        {
            StringBuilder packagePrint = new StringBuilder();
            foreach (Package package in packages)
            {
                packagePrint.Append(package.PackageId.ToString());
                if (packages.IndexOf(package) < packages.Count - 1)
                {
                    packagePrint.Append(", ");
                }
            }
            return packagePrint.ToString();
        }
    }
}
