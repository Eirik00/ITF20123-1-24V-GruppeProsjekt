using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class AisleHandler
    {
        public AisleHandler(Aisle aisle)
        {
            aisle.PackageAddedToAisle += HandlePackageAddedToAisle;
            HandleAisle(this, new AisleAndPackageEventArgs(aisle));
        }

        internal void HandleAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Aisle with ID {e.AisleId} added to the handler.");
        }
        internal void HandlePackageAddedToAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} was added to Aisle with ID {e.AisleId} on shelf {e.Shelf}");
        }
    }
    internal class AisleAndPackageEventArgs : EventArgs
    {
        internal int AisleId { get; }
        internal int PackageId { get; }
        internal (int, int) Shelf { get; }

        internal AisleAndPackageEventArgs(Aisle aisle, Package package)
        {
            AisleId = aisle.GetAisleId;
            PackageId = package.PackageId;
            Shelf = aisle.GetShelf(package);
        }
        internal AisleAndPackageEventArgs(Aisle aisle)
        {
            AisleId = aisle.GetAisleId;
        }
    }
}
