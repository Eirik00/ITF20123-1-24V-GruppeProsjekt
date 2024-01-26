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
            this.packages = new HashSet<Package>();

        }
        public void addPackage(Package package)
        {
            try
            {
                this.packages.Add(package);
            }
            catch(Exception exception){

            
            }
           
        }

        public void removePackage(Package package)
        {
            this.packages.Remove(package);
        }

        public String seePackagesInList()
        {
            String temp = "The list contains packages with following Id: ";

            foreach(Package package in this.packages)
            {
                temp += package.getPackageId + ", ";
            }

            return temp;
            
        }
    }
}
