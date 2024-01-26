using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackagesList : IPackagesList
    {
        private int listId;
        private HashSet<Package> packages;

        public PackagesList(int listId)
        {
            this.listId = listId;
            packages = new HashSet<Package>();

        }
        public void addPackage(Package package)
        {
            packages.Add(package);



        }

        public void removePackage(Package package)
        {
            packages.Remove(package);
        }

        public string seePackagesInList()
        {
            string temp = "The list contains packages with following Id: ";

            foreach (Package package in packages)
            {
                temp += package.getPackageId + ", ";
            }

            return temp;

        }
    }
}
