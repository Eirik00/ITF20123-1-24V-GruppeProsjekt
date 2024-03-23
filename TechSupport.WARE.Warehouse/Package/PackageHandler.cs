using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackageHandler
    {
        public PackageHandler(Package package)
        {
            package.PackageStatusChangedEvent += HandlePackageStatusChanged;
            HandlePackage(this, new PackageStatusChangedEventArgs(package));
        }
        internal void HandlePackage(object sender, PackageStatusChangedEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} added to the handler.");
        }
        internal void HandlePackageStatusChanged(object sender, PackageStatusChangedEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} had its status changed to: {e.Status}");
        }
    }

    internal class PackageStatusChangedEventArgs : EventArgs
    {
        internal int PackageId { get; }
        internal StatusList Status {  get; }
        internal PackageStatusChangedEventArgs(Package package)
        {
            PackageId = package.PackageId;
            Status = package.Status;
        }
    }
}
