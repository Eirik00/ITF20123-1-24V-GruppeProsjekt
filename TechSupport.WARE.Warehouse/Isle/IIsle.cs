﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    internal interface IIsle
    {
        void AddPackage(Package package, int placement);
        void RemovePackage(Package package);
        int GetIsleId { get; }
        int GetPackagePlacement(Package package);

    }
}
