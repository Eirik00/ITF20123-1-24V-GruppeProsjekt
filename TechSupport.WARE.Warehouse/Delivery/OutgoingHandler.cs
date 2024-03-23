using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class OutgoingHandler
    {
        public OutgoingHandler(Outgoing outgoing)
        {
            outgoing.OutgoingPackageEvent += HandleOutgoingPackage;
            outgoing.OutgoingDailyPackageEvent += HandleOutgoingDailyPackage;
            outgoing.OutgoingWeeklyPackageEvent += HandleOutgoingWeeklyPackage;
        }

        internal void HandleOutgoingPackage(object sender, OutgoingPackageEventArgs e)
        {
            Console.WriteLine($"Package: {e.PackageIds} has been assigned pickup. Pickup registered for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated outgoing list: {e.UpdatedOutgoingList}");
        }

        internal void HandleOutgoingDailyPackage(object sender, OutgoingPackageEventArgs e)
        {
            Console.WriteLine($"Package: {e.PackageIds} has been assigned daily pickup. Registered daily pickup for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated outgoing list: {e.UpdatedOutgoingList}");
        }

        internal void HandleOutgoingWeeklyPackage(object sender, OutgoingPackageEventArgs e)
        {
            Console.WriteLine($"Package: {e.PackageIds} has been assigned weekly pickup. Registered weekly pickup for {e.DeliveryHourAndMinute} from sender {e.Sender} to {e.Reciever}.");
            Console.WriteLine($"Updated outgoing list: {e.UpdatedOutgoingList}");
        }

    }

    internal class OutgoingPackageEventArgs : EventArgs
    {
        internal string Sender { get; }
        internal string Reciever { get; }
        internal double DeliveryHourAndMinute { get; }
        internal string PackageIds { get; }
        internal string UpdatedOutgoingList { get; }

        internal OutgoingPackageEventArgs(Outgoing outgoing, Contact sender, Contact reciever, double deliveryTime, PackageList packages)
        {
            Sender = $"{sender.FirstName} {sender.Surname}";
            Reciever = $"{reciever.FirstName} {reciever.Surname}";
            DeliveryHourAndMinute = deliveryTime;
            PackageIds = PackageList(packages);
            UpdatedOutgoingList = ListPackages(outgoing.OutgoingPackagesList);
        }
        internal OutgoingPackageEventArgs(Outgoing outgoing, Contact sender, Contact reciever, double deliveryTime, Package package)
        {
            Sender = $"{sender.FirstName} {sender.Surname}";
            Reciever = $"{reciever.FirstName} {reciever.Surname}";
            DeliveryHourAndMinute = deliveryTime;
            PackageIds = $"{package.PackageId}";
            UpdatedOutgoingList = ListPackages(outgoing.OutgoingPackagesList);
        }
        private string PackageList(PackageList packages)
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

        //Må endres i Outgoing til List med Package eller PackageList
        private string ListPackages(List<PackageList> packages)
        {
            StringBuilder packagePrint = new StringBuilder();
            foreach (PackageList packagelist in packages)
            {
                foreach (Package package in packagelist)
                {
                    packagePrint.Append(package.PackageId.ToString());
                    if (packages.IndexOf(packagelist) < packages.Count - 1)
                    {
                        packagePrint.Append(", ");
                    }
                }
                    
            }
            return packagePrint.ToString();
        }
    }
}
