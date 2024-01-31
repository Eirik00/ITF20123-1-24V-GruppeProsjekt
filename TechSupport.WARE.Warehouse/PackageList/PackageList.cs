using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackageList : IPackageList
    {
        private int listId;
        private HashSet<Package> packages;

        public PackageList(int listId)
        {
            this.listId = listId;
            packages = new HashSet<Package>();

        }
        public void AddPackage(Package package)
        {
            packages.Add(package);
        }

        public void RemovePackage(Package package)
        {
            packages.Remove(package);
        }

        public String SeePackagesInList()
        {
            String temp = "The list contains packages with following Id: ";

            foreach (Package package in packages)
            {
                temp += package.GetPackageId + ", ";
            }

            return temp;
        }

        public String ShowPackagesSortedByLenght()
        {
            // To be implemented
            return "";
        }
        public String ShowPackagesSortedByHeight()
        {
            // To be implemented
            return "";
        }
        public String ShowPackagesSortedByDepth()
        {
            // To be implemented
            return "";
        }
        public String ShowPackagesSortedByWeight()
        {
            // To be implemented
            return "";
        }

        public String ShowPackagesSortedByVolume()
        {
            // To be implemented
            return "";
        }
    }
}
