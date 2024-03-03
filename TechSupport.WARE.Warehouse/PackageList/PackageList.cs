using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackageList(int listId) : IPackageList
    {
        private readonly int listId = listId;
        private List<Package> packages = [];
  
        /// <summary>
        /// Adds a package to the packagelist
        /// </summary>
        public void AddPackage(Package package)
        {
            if (packages.Contains(package))
            {
                throw new Exception("The package is allready in the list");
            }
            else
            {
                packages.Add(package);
            }
        }

        /// <summary>
        /// Removes a package from the list
        /// </summary>
        public void RemovePackage(Package package)
        {
            packages.Remove(package);
        }

        public List<Package> Packages => packages;

        /// <summary>
        /// Returns a formatted string of the IDs of all packages in the packagelist
        /// </summary>
        public string SeePackagesInList()
        {
            if (packages.Count == 0)
            {
                return "The list does not contain any packages";
            }

            else
            {

                string temp = "The list contains packages with following Id: ";

                foreach (Package package in packages)
                {
                    temp += package.PackageId + "   ";
                }

                return temp;

            }
            }


        /// <summary>
        /// Returns a formatted string of all packages in the packagelist sorted by their lenght in descending order
        /// </summary>
        public String ShowPackagesSortedByLenght()
        {
            string tempString = "Packages in the list sorted by lenght descending: \n";

            List<Package> tempList = packages;

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++)
                {

                    if (tempList[i].PackageLengthInMm > tempList[j].PackageLengthInMm)
                    {
                        (tempList[j], tempList[i]) = (tempList[i], tempList[j]);
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Lenght: " + package.PackageLengthInMm + "\n";
            }

            return tempString;
        }

        /// <summary>
        /// Returns a formatted string of all packages in the packagelist sorted by their height in descending order
        /// </summary>
        public String ShowPackagesSortedByHeight()
        {
            string tempString = "Packages in the list sorted by height descending: \n";

            List<Package> tempList = packages;

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++)
                {

                    if (tempList[i].PackageHeightInMm > tempList[j].PackageHeightInMm)
                    {
                        (tempList[j], tempList[i]) = (tempList[i], tempList[j]);
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Height: " + package.PackageHeightInMm + "mm\n";
            }

            return tempString;
        }

        /// <summary>
        /// Returns a formatted string of all packages in the packagelist sorted by their depth in descending order
        /// </summary>
        public String ShowPackagesSortedByDepth()
        {
            string tempString = "Packages in the list sorted by depth descending: \n";

            List<Package> tempList = packages;

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++)
                {

                    if (tempList[i].PackageDepthInMm > tempList[j].PackageDepthInMm)
                    {
                        (tempList[j], tempList[i]) = (tempList[i], tempList[j]);
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Depth: " + package.PackageDepthInMm + "mm\n";
            }

            return tempString;
        }

        /// <summary>
        /// Returns a formatted string of all packages in the packagelist sorted by their weight in descending order
        /// </summary>
        public String ShowPackagesSortedByWeight()
        {
            string tempString = "Packages in the list sorted by weight descending: \n";

            List<Package> tempList = packages;

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++)
                {

                    if (tempList[i].PackageWeightInGrams > tempList[j].PackageWeightInGrams)
                    {
                        (tempList[j], tempList[i]) = (tempList[i], tempList[j]);
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Weight: " + package.PackageWeightInGrams + " grams\n";
            }

            return tempString;
        }

        /// <summary>
        /// Returns a formatted string of the total weight of all packages in the packagelist
        /// </summary>
        public string TotalWeightOfAllPackages()
        {
            int tempweight = 0;

            foreach (Package package in packages)
            {
                tempweight += package.PackageWeightInGrams;
            }

            return "The total combined weight of all packages in the list is: " + tempweight + " grams.\n";
        }


        /*public string ShowPackagesSortedByVolume()
        {
            string tempString = "Packages in the list sorted by volume descending: \n";

            List<Package> tempList = packages;

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++)
                {

                    if (tempList[i].PackageVolumeInCubicMm > tempList[j].PackageVolumeInCubicMm)
                    {
                        Package tempPackage = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempPackage;
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Volume: " + package.PackageVolumeInCubicMm + "mm^3\n";
            }

            return tempString;
        }*/
    }
}
