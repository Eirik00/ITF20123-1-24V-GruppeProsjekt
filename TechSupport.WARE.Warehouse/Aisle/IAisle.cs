using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    internal interface IAisle
    {
        /// <summary>
        /// void <c>AddPackage</c> adds a package to the isle thats being referenced
        /// </summary>
        /// <param name="package">The package object</param>
        /// <param name="placement">Placement of the package in the isle</param>
        void AddPackage(Package package, (int,int) placement);

        /// <summary>
        /// void <c>RemovePackage</c> removes the package from the isle
        /// </summary>
        /// <param name="package">The package object to be removed</param>
        void RemovePackage(Package package);

        /// <summary>
        /// Gets the isle id
        /// </summary>
        int GetAisleId { get; }
        StorageSpecification GetStorageSpecification { get; } 

        (int,int) GetShelf(Package package);

        /// <summary>
        /// int <c>GetPacakgePlacement</c> gets the placement of the package refrenced
        /// </summary>
        /// <param name="package">Package object</param>
        /// <returns>Returns the placement as an int</returns>
        (int,int) GetPackagePlacement(Package package);

    }
}
