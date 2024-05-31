using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents an interface for packages in the warehouse system.
    /// </summary>
    internal interface IPackage
    {
        /// <summary>
        /// Get the package id
        /// </summary>
        int PackageId { get; }
        /// <summary>
        /// Gets the package length in Centimeter
        /// </summary>
        int PackageLengthInCm { get; }
        /// <summary>
        /// Gets the packagge height in Centimeter
        /// </summary>
        int PackageHeightInCm { get; }
        /// <summary>
        /// Gets the package depth in Centimeter
        /// </summary>
        int PackageDepthInCm { get; }
        /// <summary>
        /// Gets the package weight in grams
        /// </summary>
        int PackageWeightInGrams { get; }
        /// <summary>
        /// Gets the package specification
        /// </summary>
        StorageSpecification Specification { get; }
        /// <summary>
        /// Get and set the package sender
        /// </summary>
        Contact Sender { get; set; }
        /// <summary>
        /// Get and set the package reciever
        /// </summary>
        Contact Receiver { get; set; }
        /// <summary>
        /// Get the status of where the package is in the shipping process
        /// </summary>
        StatusList Status { get; }
        /// <summary>
        /// (Aisle, StorageSpesification, int) <c>GetLocation()</c> Gets the aisle the package is located,
        /// the storage spesification and the placement in the spesified aisle.
        /// </summary>
        /// <returns>Aisle <c>aisle</c>, int <c>storageSpecification</c>, int <c>place</c></returns>
        (Aisle? aisle, StorageSpecification specification, double place) GetLocation();
        /// <summary>
        /// void <c>ChangeStatus(StatusList, String)</c> changes the status of the package to where it is in the shipping process
        /// </summary>
        /// <param name="newStatus">The new status</param>
        /// <param name="description">*optional* description if needed</param>
        void ChangeStatus(StatusList newStatus, String description);
        /// <summary>
        /// PackageLog <c>GetPackageLog()</c> returns a PackageLog object corresponding to the package
        /// </summary>
        /// <returns>PackageLog</returns>
        PackageLog GetPackageLog();
        (int,int)? GetShelf();
        Aisle? PackageAisle {  get; }
        /// <summary>
        /// String <c>ToString()</c> gives out the package description in string format
        /// </summary>
        /// <returns>String</returns>
        String ToString();
    }
}

