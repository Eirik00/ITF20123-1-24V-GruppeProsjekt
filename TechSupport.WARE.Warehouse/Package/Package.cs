using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 0: Invalid 1: Initialized, 2: Ordered, 3: Reception, 4: Storage, 5: In Progress, 6: Delivery
    /// </example>
    /// </summary>
    public enum StatusList {Invalid = 0, Initialized = 1, Ordered = 2, Reception = 3, Storage = 4, InProgress = 5, Delivery = 6 };
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 0: Invalid 1: ClimateControlled, 2: SmallItems, 3: HighValue
    /// </example>
    /// </summary>
    public enum StorageSpecification { Invalid = 0, ClimateControlled = 1, SmallItems = 2, HighValue = 3};

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
        private Aisle? packageAisle;

        //shipment number feature ''attempts''
        //Skal prøve å gi hver pakke instanse av Package som er oprettet en random forsendelse nummer.
        //forsendel nummeret er generert i konstrøktøren
        private static readonly Random random = new();
        private int shipmentNumber;

        //Events
        public delegate void PackageHandler(object sender, EventArgs e);
        public event PackageHandler StatusChange;
        public virtual void OnStatusChange(EventArgs e)
        {
            StatusChange?.Invoke(this, e);
        }

        public int ShipmentNumber
        {
            get { return shipmentNumber; }
            private set { shipmentNumber = value; }
        }

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeightInGrams, bool isFragile, StorageSpecification specification)
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
                this.status = StatusList.Initialized;
                this.DeliveryTime = default;
                ShipmentNumber = random.Next(10, 601);

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
        public Aisle? PackageAisle => packageAisle;
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
        /// (Aisle, String, int) <c>GetLocation</c> returns the current location and equivelent information of that package
        /// </summary>
        /// <returns>Aisle <c>aisle</c>, String <c>spesification</c>, int <c>place</c></returns>
        /// 
        internal void AddAisle(Aisle aisle)
        {
            if(this.Specification != aisle.currentStorageZone.StorageSpecification)
            {
                throw new Exception("Packages storage specification: " + this.specification + ", does not match the storage specification of the aisle: " + aisle.currentStorageZone.StorageSpecification);
            }
            this.packageAisle = aisle;
        }

        public (int,int)? GetShelf()
        {
            if (this.packageAisle == null)
            {
                return null;
            }
            return this.packageAisle.GetShelf(this);
        }
        public (Aisle? aisle, StorageSpecification specification, double place) GetLocation()
        {
            return (this.packageAisle, this.specification, 0);
        }
        public void ChangeStatus(StatusList newStatus, String description = "")
        {
            packageLog.LogChange(this.GetLocation().aisle, newStatus, status, description);
            this.status = newStatus;
            OnStatusChange(EventArgs.Empty);
        }

        public PackageLog GetPackageLog()
        {
            return packageLog;
        }

        //public override string ToString()
        //{
        //    throw new NotImplementedException();
        //}


       //Dette reflekteres i Outgoing og import klassene
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
