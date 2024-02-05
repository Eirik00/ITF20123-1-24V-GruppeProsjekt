using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackageList //: IPackageList
    {
        private int listId;
        private List<Package> packages;

        public PackageList(int listId)
        {
            this.listId = listId;
            this.packages = new List<Package>();
        }
        public void AddPackage(Package package)
        {
            if (packages.Contains(package))
            {
                Console.WriteLine("The package is allready in the list");
            }
            else
            {
                packages.Add(package);
            }
        }

        public void RemovePackage(Package package)
        {
            packages.Remove(package);
        }

        public string SeePackagesInList()
        {
            string temp = "The list contains packages with following Id: ";

            foreach (Package package in packages)
            {
                temp += package.PackageId + ", ";
            }

            return temp;
        }

        public String ShowPackagesSortedByLenght()
        {
            string tempString = "Packages in the list sorted by weight descending: \n";

            List<Package> tempList = packages;

            for(int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < tempList.Count; j++) {

                    if (tempList[i].PackageLengthInMm > tempList[j].PackageLengthInMm)
                    {
                        Package tempPackage = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempPackage;
                    }
                }
            }

            foreach(Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Lenght: " + package.PackageLengthInMm + "mm\n";
            }

            return tempString;
        }

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
                        Package tempPackage = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempPackage;
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Height: " + package.PackageHeightInMm + "mm\n";
            }

            return tempString;
        }

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
                        Package tempPackage = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempPackage;
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Depth: " + package.PackageDepthInMm + "mm\n";
            }

            return tempString;
        }

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
                        Package tempPackage = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = tempPackage;
                    }
                }
            }

            foreach (Package package in tempList)
            {
                tempString += "PackageId: " + package.PackageId + ", Weight: " + package.PackageWeightInGrams + " grams\n";
            }

            return tempString;
        }
    }
}
