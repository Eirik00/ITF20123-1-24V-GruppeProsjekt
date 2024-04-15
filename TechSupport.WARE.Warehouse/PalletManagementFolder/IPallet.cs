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

        /// <summary>
        /// Adds a package to the pallet.
        /// </summary>
        /// <param name="package">The package to add.</param>
        /// <returns>True if the package was successfully added; otherwise, false.</returns>
        bool AddPackage(Package package);

        /// <summary>
        /// Adds a list of packages to the pallet.
        /// </summary>
        /// <param name="packageList">The list of packages to add.</param>
        /// <returns>True if the packages were successfully added; otherwise, false.</returns>
        public bool AddPackageList(PackageList packageList);

        /// <summary>
        /// Removes a package from the pallet.
        /// </summary>
        /// <param name="packageId">The ID of the package to remove.</param>
        /// <returns>True if the package was successfully removed; otherwise, false.</returns>
        bool RemovePackage(int packageId);

        /// <summary>
        /// Gets the list of packages on the pallet.
        /// </summary>
        List<Package> Packages { get; }

        /// <summary>
        /// Gets the number of packages on the pallet.
        /// </summary>
        int PackageCount { get; }
    }
}
