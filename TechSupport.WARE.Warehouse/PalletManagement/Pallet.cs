using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Provides data for the PalletUpdated event.
    /// </summary>
    public class PalletsEventArgs : EventArgs
    {
        public string ?PalletAction { get; set; }
        public int PackageId { get; set; }
    }

    /// <summary>
    /// Represents a pallet that can hold packages.
    /// </summary>
    public class Pallet : IPallet
    {
        public int PalletId { get; private set; }
        private List<Package> packages = new List<Package>();
        public const int MaxCapacity = 30;

        //events
        public event EventHandler<PalletsEventArgs> PalletUpdated;

        protected virtual void OnPalletUpdated(PalletsEventArgs e)
        {
            PalletUpdated?.Invoke(this, e);
        }
        //events end

        /// <summary>
        /// Initializes a new instance of the Pallet class with the specified ID.
        /// </summary>
        /// <param name="palletid">The ID of the pallet.</param>
        public Pallet(int palletid)
        {
            PalletId = palletid;
        }

        /// <summary>
        /// Adds a package to the pallet.
        /// </summary>
        /// <param name="package">The package to add.</param>
        /// <returns>True if the package was successfully added; otherwise, false.</returns>
        public bool AddPackage(Package package)
        {
            if (packages.Count >= MaxCapacity) return false;
            packages.Add(package);
            OnPalletUpdated(new PalletsEventArgs { PalletAction = "Added Pallet", PackageId = package.PackageId });
            return true;
        }

        /// <summary>
        /// Adds a list of packages to the pallet.
        /// </summary>
        /// <param name="packageList">The list of packages to add.</param>
        /// <returns>True if all packages were successfully added; otherwise, false.</returns>
        public bool AddPackageList(PackageList packageList)
        {
            foreach (var package in packageList.Packages)
            {
                if (packages.Count >= MaxCapacity)
                {
                    OnPalletUpdated(new PalletsEventArgs { PalletAction = "Attempted PackageList Add", PackageId = package.PackageId });
                    return false;
                }
                packages.Add(package);
            }
            OnPalletUpdated(new PalletsEventArgs { PalletAction = "PackageList Added", PackageId = -1 });
            return true;
        }

        /// <summary>
        /// Removes a package from the pallet.
        /// </summary>
        /// <param name="packageId">The ID of the package to remove.</param>
        /// <returns>True if the package was successfully removed; otherwise, false.</returns>
        public bool RemovePackage(int packageId)
        {
            var package = packages.Find(p => p.PackageId == packageId);
            if (package != null && packages.Remove(package))
            {
                OnPalletUpdated(new PalletsEventArgs { PalletAction = "Removed", PackageId = packageId });
                return true;
            }
            return false;
        }

        public List<Package> Packages => new List<Package>(packages);
        public int PackageCount => packages.Count;
    }
}