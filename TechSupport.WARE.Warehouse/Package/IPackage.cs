﻿using System;
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
        int GetPackageId { get; }
        int GetPackageLengthInMm { get; }
        int GetPackageHeightInMm { get; }
        int GetPackageDepthInMm { get; }
        int GetPackageWeightInGrams { get; }
        Contact GetSender { get; }
        Contact SetSender { set; }
        Contact GetReceiver { get; }
        Contact SetReceiver { set; }
        StatusList GetStatus { get; }
        (Isle isle, int storageSpecification, int place) GetLocation();
        void ChangeStatus(StatusList newStatus, String description);
        List<PackageLogEntry> GetPackageLog();
        string ToString();
    }
}
