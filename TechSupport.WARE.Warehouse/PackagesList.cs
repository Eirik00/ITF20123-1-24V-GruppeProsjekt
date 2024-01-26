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
        private ArrayList packages;

        public PackagesList(int listId, ArrayList packages)
        {
            this.listId = listId;
            this.packages = packages;

        }
        public void addPackageToList(Package package)
        {
            this.packages.Add(package);
        }
    }
}
