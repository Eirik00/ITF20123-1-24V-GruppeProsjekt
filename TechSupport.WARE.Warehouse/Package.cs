using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    public enum StatusList { Reception = 1, Storage = 2, InProgress = 3, Delivery = 4 };
    public class Package : IPackage
    {
        private int packageId, packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeighInGrams;
        private bool isFragile;
        private int storageSpecification;
        private StatusList status;
        private Dictionary<DateTime, StatusList> statusLog;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeighInMm, bool isFragile, int storageSpecification, StatusList status = StatusList.Reception)
        {
            this.packageId = packageId;
            this.packageLenghtInMm = packageLenghtInMm;
            this.packageHeightInMm = packageHeightInMm;
            this.packageDepthInMm = packageDepthInMm;
            this.packageWeighInGrams = packageWeighInMm;
            this.isFragile = isFragile;
            this.storageSpecification = storageSpecification;
            this.status = status;

            this.statusLog = new Dictionary<DateTime, StatusList>();
        }

        public Dictionary<DateTime, StatusList> StatusLog
        {
            get
            {
                return statusLog;
            }
        }

        public StatusList Status
        {
            get
            {
                return status;
            }
        }

        public (Isle isle, String category, int place) GetLocation()
        {
            return (null, null, 0);
        }
        public void ChangeStatusList(StatusList newStatus)
        {
            this.status = newStatus;
        }

    }
}
