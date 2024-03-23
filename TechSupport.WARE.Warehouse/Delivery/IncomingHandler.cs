using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class IncomingHandler
    {
        public IncomingHandler(Incoming incoming)
        {
            incoming.IncomingPackageEvent += HandleIncomingPackage;
        }

        internal void HandleIncomingPackage(object sender, IncomingPackageEventArgs e)
        {
            Console.WriteLine($"Pakke: {e.PackageIds} bestillt til Vare Mottak. Registerert til Kl {e.DeliveryHourAndMinute} av sender {e.Sender} til {e.Reciever}");
            Console.WriteLine($"Oppdatert bestillingsliste for Vare Mottak: {e.UpdatedIncomingList}");
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
