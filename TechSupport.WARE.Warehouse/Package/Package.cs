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
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 1: ColdStorage, 2: DryStorage, 3: DangerousProducts
    /// </example>
    /// </summary>
    public enum StorageSpecification { Invalid = 0, ColdStorage = 1, DryStorage = 2, DangerousProducts = 3};

    public class Package : IPackage
    {

        /// <summary>
        ///     Package object.
        /// </summary>
        private readonly static List<int> idCheck = [];
        private readonly int packageId, packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeighInGrams;
        private readonly bool isFragile;
        private readonly StorageSpecification specification;
        private StatusList status;
        private Contact sender;
        private Contact receiver;
        public DateTime DeliveryTime;
        readonly PackageLog packageLog = new();
        private Isle? packageIsle;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeightInGrams, bool isFragile, StorageSpecification specification, StatusList status = StatusList.Invalid, DateTime deliveryTime = default)
        {
            if (idCheck.Contains(packageId))
            {
                throw new Exception("The id: " + packageId + " is not unique...");
            }
            else
            {
                this.packageId = packageId;
                this.packageLenghtInMm = packageLenghtInMm;
                this.packageHeightInMm = packageHeightInMm;
                this.packageDepthInMm = packageDepthInMm;
                this.packageWeighInGrams = packageWeightInGrams;
                this.isFragile = isFragile;
                this.specification = specification;
                this.status = status;
                this.sender = new Contact("", "", "", "", "", 0, 0);
                this.receiver = new Contact("", "", "", "", "", 0, 0);
                this.DeliveryTime = deliveryTime;

                packageLog.LogChange(null, StatusList.Invalid, status, "Package initialized");
                idCheck.Add(packageId);
            }
        }

        //LAG TOSTRING
        public int PackageId => packageId;
        public int PackageLengthInMm => packageLenghtInMm;
        public int PackageHeightInMm => packageHeightInMm;
        public int PackageDepthInMm => packageDepthInMm;
        public int PackageWeightInGrams => packageWeighInGrams;
        public StorageSpecification Specification => specification;
        public Contact Sender { 
            get { return this.sender; }
            set { this.sender = value; } 
        }
        public Contact Receiver { 
            get { return this.receiver; }
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

        public (Isle? isle, StorageSpecification specification, int place) GetLocation()
        {
            return (this.packageIsle, this.specification, 0);
        }
        public void ChangeStatus(StatusList newStatus, String description = "")
        {
            packageLog.LogChange(this.GetLocation().isle, newStatus, status, description);
            this.status = newStatus;
        }

        public PackageLog GetPackageLog()
        {
            return packageLog;
        }

        //public override string ToString()
        //{
        //    throw new NotImplementedException();
        //}


       //Dette reflekteres i Export og import klassene
       //Prøve å endre Package/import/export status
       //Jeg har ikke fått til å teste dette da visual studio ikke klarer å bygge prosjektet for et eller annet grunn
       //slettes dersom ikke funker
        public override string ToString()
        {
            return $"Package ID: {PackageId}\n" +
                   $"  Status: {Status}\n" +
                   $"  Delivery Time: {DeliveryTime}\n" +
                   $"  Sender: {Sender.FirstName} {Sender.Surname}\n" +
                   $"  Receiver: {Receiver.FirstName} {Receiver.Surname}\n";
        }

 
    }
}
