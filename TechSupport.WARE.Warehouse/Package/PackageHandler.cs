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
            package.PackageStatusChanged += HandlePackageStatusChanged;
        }

        internal void HandlePackageStatusChanged(object sender, PackageStatusChangedEventArgs e)
        {
            Console.WriteLine($"Package: {e.PackageId}'s status has changed to: {e.Status}");
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
