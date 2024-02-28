using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IPackage
    {
        /// <summary>
        /// Get the package id
        /// </summary>
        int PackageId { get; }
        /// <summary>
        /// Gets the package length in millimeter
        /// </summary>
        int PackageLengthInMm { get; }
        /// <summary>
        /// Gets the packagge height in millimeter
        /// </summary>
        int PackageHeightInMm { get; }
        /// <summary>
        /// Gets the package depth in millimeter
        /// </summary>
        int PackageDepthInMm { get; }
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
        /// Get the package status
        /// </summary>
        StatusList Status { get; }
        /// <summary>
        /// (Isle, StorageSpesification, int) <c>GetLocation()</c> Gets the isle the package is located,
        /// the storage spesification and the placement in the spesified isle.
        /// </summary>
        /// <returns>Isle <c>isle</c>, int <c>storageSpecification</c>, int <c>place</c></returns>
        (Isle? isle, StorageSpecification specification, int place) GetLocation();
        /// <summary>
        /// void <c>ChangeStatus(StatusList, String)</c> changes the status of the package to where it is in the process
        /// </summary>
        /// <param name="newStatus">The new status</param>
        /// <param name="description">*optional* description if needed</param>
        void ChangeStatus(StatusList newStatus, String description);
        /// <summary>
        /// List <c>GetPackageLog()</c> gets the package log object
        /// </summary>
        /// <returns>PackageLog</returns>
        PackageLog GetPackageLog();
        int? GetShelf();
        Isle? PackageIsle {  get; }
        /// <summary>
        /// String <c>ToString()</c> gives out the package description in string format
        /// </summary>
        /// <returns>String</returns>
        String ToString();
    }
}

