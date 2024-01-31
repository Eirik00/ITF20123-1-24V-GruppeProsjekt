using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 1: Reception, 2: Storage, 3: In Progrress, 4: Delivery
    /// </example>
    /// </summary>
    public enum StatusList {Invalid = 0, Reception = 1, Storage = 2, InProgress = 3, Delivery = 4 };

    public class Package : IPackage
    {

        /// <summary>
        ///     Package object.
        /// </summary>
        private int packageId, packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeighInGrams;
        private bool isFragile;
        private int storageSpecification;
        private StatusList status;
        private Contact sender;
        private Contact receiver;
        public DateTime DeliveryTime;
        PackageLog packageLog = new PackageLog();
        private Isle? packageIsle;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeighInMm, bool isFragile, int storageSpecification, StatusList status = StatusList.Reception, DateTime deliveryTime = default)
        {
            this.packageId = packageId;
            this.packageLenghtInMm = packageLenghtInMm;
            this.packageHeightInMm = packageHeightInMm;
            this.packageDepthInMm = packageDepthInMm;
            this.packageWeighInGrams = packageWeighInMm;
            this.isFragile = isFragile;
            this.storageSpecification = storageSpecification;
            this.status = status;
            this.sender = new Contact("", "", "", "", "", 0, 0);
            this.receiver = new Contact("", "", "", "", "", 0, 0);
            this.DeliveryTime = deliveryTime;

            packageLog.LogChange(null, StatusList.Invalid, StatusList.Reception, "Package Recieved");
        }

        //LAG TOSTRING
        public int PackageId => packageId;
        public int PackageLengthInMm => packageLenghtInMm;
        public int PackageHeightInMm => packageHeightInMm;
        public int PackageDepthInMm => packageDepthInMm;
        public int PackageWeightInGrams => packageWeighInGrams;
        public Contact Sender { 
            get { return sender; }
            set { this.sender = value; } 
        }
        public Contact Receiver { 
            get { return receiver; }
            set { this.receiver = value; }
        }

        /// <summary>
        /// StatusList <c>Status</c> returns the current status of the package
        /// </summary>
        public StatusList Status => status;
        /// <summary>
        /// (Isle, String, int) <c>GetLocation</c> returns the current location and equivelent information of that package
        /// </summary>
        /// <returns>Isle <c>isle</c>, String <c>category</c>, int <c>place</c></returns>
        /// 
        internal void AddIsle(Isle isle)
        {
            this.packageIsle = isle;
        }

        public (Isle isle, int storageSpecification, int place) GetLocation()
        {
            return (this.packageIsle, this.storageSpecification, 0);
        }
        public void ChangeStatus(StatusList newStatus, String description = "")
        {
            packageLog.LogChange(this.GetLocation().isle, newStatus, status, description);
            this.status = newStatus;
        }
        public List<PackageLogEntry> GetPackageLog()
        {
            return packageLog.GetEntries();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
