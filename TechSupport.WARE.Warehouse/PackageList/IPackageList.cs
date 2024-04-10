using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IPackageList
    {
        /// <summary>
        /// Adds a given Package instance to the PackageList.
        /// </summary>
        void AddPackage(Package package);

        /// <summary>
        /// Removes a given Package instance from the PackageList.
        /// </summary>
        void RemovePackage(Package package);

        /// <summary>
        /// Returns a string displaying the IDs of Package instances in the PackageList
        /// </summary>
        String SeePackagesInList();
 
        /// <summary>
        /// Returns a string displaying Package instances in the PackageList sorted by the their lenght in mm
        /// </summary>
        String ShowPackagesSortedByLenght();

        /// <summary>
        /// Returns a string displaying Package instances in the PackageList sorted by the their height in mm
        /// </summary>
        String ShowPackagesSortedByHeight();

        /// <summary>
        /// Returns a string displaying Package instances in the PackageList sorted by the their depth in mm
        /// </summary>
        String ShowPackagesSortedByDepth();

        /// <summary>
        /// Returns a string displaying Package instances in the PackageList sorted by the their weight in grams
        /// </summary>
        String ShowPackagesSortedByWeight();
    }
}
