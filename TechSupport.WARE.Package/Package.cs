using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    internal class Package
    {
        private int Id;
        private int packageLenghtInMm;
        private int packageHeightInMm;
        private int packageDepthInMm;
        private int packageWeighInMm;
        private bool isFragile;
        Dictionary<int, string> storageSpecification;
        Dictionary<int, string> status;
    }
}
