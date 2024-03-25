using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    public interface IPallet
    {
        int PalletId { get; }

        event EventHandler<PalletEventArgs> PalletUpdated;

        bool AddPackage(Package package);
        public bool AddPackageList(PackageList packageList);
        bool RemovePackage(int packageId);
        List<Package> Packages { get; }
        int PackageCount { get; }
    }
}
