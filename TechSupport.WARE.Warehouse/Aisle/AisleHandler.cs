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
        }

        internal void HandlePackageAddedToAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Package: {e._packageId} was added to Aisle: {e._aisleId}, shelf: {e._shelf}");
        }
    }

    internal class AisleAndPackageEventArgs : EventArgs
    {
        internal int _aisleId { get; }
        internal int _packageId { get; }
        internal (int, int) _shelf { get; }

        internal AisleAndPackageEventArgs(Aisle aisle, Package package)
        {
            _aisleId = aisle.GetAisleId;
            _packageId = package.PackageId;
            _shelf = aisle.GetShelf(package);
        }
    }
}
