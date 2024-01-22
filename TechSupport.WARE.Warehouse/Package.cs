using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    public enum statusList { Reception = 1, Storage = 2, InProgress = 3, Delivery = 4 };
    public class Package
    {
        private int packageId;
        private int packageLenghtInMm;
        private int packageHeightInMm;
        private int packageDepthInMm;
        private int packageWeighInGrams;
        private bool isFragile;
        private int storageSpecification;
        private statusList status;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeighInMm, bool isFragile, int storageSpecification, statusList status = statusList.Reception)
        {
            this.packageId = packageId;
            this.packageLenghtInMm = packageLenghtInMm;
            this.packageHeightInMm = packageHeightInMm;
            this.packageDepthInMm = packageDepthInMm;
            this.packageWeighInGrams = packageWeighInMm;
            this.isFragile = isFragile;
            this.storageSpecification = storageSpecification;
            this.status = status;
        }

        public string getStatus(Package package)
        {
            return this.status.ToString;
        }

    }
}
