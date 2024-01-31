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
        int PackageId { get; }
        int PackageLengthInMm { get; }
        int PackageHeightInMm { get; }
        int PackageDepthInMm { get; }
        int PackageWeightInGrams { get; }
        Contact Sender { get; set; }
        Contact Receiver { get; set; }
        StatusList Status { get; }
        (Isle isle, int storageSpecification, int place) GetLocation();
        void ChangeStatus(StatusList newStatus, String description);
        List<PackageLogEntry> GetPackageLog();
        string ToString();
    }
}

