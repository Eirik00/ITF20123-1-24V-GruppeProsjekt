using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    public class Package
    {
        private int packageId;
        private int packageLenghtInMm;
        private int packageHeightInMm;
        private int packageDepthInMm;
        private int packageWeighInMm;
        private bool isFragile;
        private int storageSpecification;
        private int status;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeighInMm, bool isFragile, int storageSpecification, int status = 0)
        {
            this.packageId = packageId;
            this.packageLenghtInMm = packageLenghtInMm;
            this.packageHeightInMm = packageHeightInMm;
            this.packageDepthInMm = packageDepthInMm;
            this.packageWeighInMm = packageWeighInMm;
            this.isFragile = isFragile;
            this.storageSpecification = storageSpecification;
            this.status = status;
        }
    }
}
