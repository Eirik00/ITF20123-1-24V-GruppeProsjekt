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
        private ArrayList packages;

        public PackageList(int listId)
        {
            this.listId = listId;
            this.packages = new ArrayList();

        }
        public void addPackage(Package package)
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

        public void removePackage(Package package)
        {
            packages.Remove(package);
        }

        public string seePackagesInList()
        {
            string temp = "The list contains packages with following Id: ";

            foreach (Package package in packages)
            {
                temp += package.PackageId + ", ";
            }

            return temp;

        }
    }
}
