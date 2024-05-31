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
        /*Returns all packages that are currently in the list. As for now, showing packages in the list
         * based on certain filters (e.g. weight) is done in seperate methods, could possibly be done as a parameter 
         * for this method instead.*/

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

        /* Displays the packages in the list sorted by various atributes, should not change the actuall order of the packages in the list,
         * the order of the list should not matter. */

        /// <summary>
        /// Returns a string displaying Package instances in the PackageList sorted by the their volume
        /// </summary>
        // string ShowPackagesSortedByVolume();
        /* Displays the packages sorted by the Volume of the packages, volume is not an inherent atribute of the Package class,
         * but should be easy to calculate based on lenght, height, and depth. */
    }
}



/* Methods for removing packages based on certain criteria could also be added, as for example a method for removing
 * all packages above a certain weight limit, but it is unsure how usefull and neccesary such a method would be. */

/* As stated it is unclear wether or not functionality that could be implemented for this class really needs to be,
 * since objects of this class is only meant to function as a uniform way of sending in a list of package objects to
 * other functions that might require it. Functionality for displaying packages based on certain criteria could
 * be delegated to for example the Isle class. */

/* In the case that objects of this class are to function as a way to store collections of packages without them 
 * having to be placed in isles, it would of course be valuable to reveal information about the packages in a list,
 * but again it could prove redundant if the purpouse of this class is only to be a temporarily used list for sending
 * multiple packages to other functions. */
