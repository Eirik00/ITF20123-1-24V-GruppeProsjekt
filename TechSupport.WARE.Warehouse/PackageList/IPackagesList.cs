using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IPackagesList
    {
        public void addPackage(Package package);
        /*Adds package to the list, since package has no uniqe identifier as an atribute, 
         * it takes the name of the package object as atribute.*/

        public void removePackage(Package package);
        //Removes package from the list.

        public string seePackagesInList();
        /*Returns all packages that are currently in the list. As for now, showing packages in the list
         * based on certain filters (e.g. weight) is done in seperate methods, could possibly be done as a parameter 
         * for this method instead.*/

        public string showPackagesSortedByWeight();
        /* Displays the packages in the list sorted by weight, should not change the actuall order of the packages in the list,
         * the order of the list should not matter. */


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
    }
}
