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
        public void removePackage(Package package);

        public string seePackagesInList();
    }
}
