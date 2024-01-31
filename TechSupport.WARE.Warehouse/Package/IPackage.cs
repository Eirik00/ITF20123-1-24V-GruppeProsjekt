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
        int GetPackageId { get; }
        /// <summary>
        /// Gets the package length in millimeter
        /// </summary>
        int GetPackageLengthInMm { get; }
        /// <summary>
        /// Gets the packagge height in millimeter
        /// </summary>
        int GetPackageHeightInMm { get; }
        /// <summary>
        /// Gets the package depth in millimeter
        /// </summary>
        int GetPackageDepthInMm { get; }
        /// <summary>
        /// Gets the package weight in grams
        /// </summary>
        int GetPackageWeightInGrams { get; }
        /// <summary>
        /// Get the package sender
        /// </summary>
        Contact Sender { get; set; }
        Contact Receiver { get; set; }
        StatusList GetStatus { get; }
        (Isle isle, int storageSpecification, int place) GetLocation();
        void ChangeStatus(StatusList newStatus, String description);
        List<PackageLogEntry> GetPackageLog();
        string ToString();
    }
}

