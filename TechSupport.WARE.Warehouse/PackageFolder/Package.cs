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
     /// This is an Enum
     /// Enum <c>StatusList</c> represents the status of a package.
     /// <remarks>
     /// The enumerators in this enum declare various statuses:
     /// <list type="bullet">
     ///     <item><description>Invalid (0): Indicates an invalid status.</description></item>
     ///     <item><description>Initialized (1): The package has been initialized.</description></item>
     ///     <item><description>Ordered (2): The package has been ordered.</description></item>
     ///     <item><description>Reception (3): The package is in reception.</description></item>
     ///     <item><description>Storage (4): The package is in storage.</description></item>
     ///     <item><description>InProgress (5): The package is in progress.</description></item>
     ///     <item><description>Delivery (6): The package is out for delivery.</description></item>
     /// </list>
     /// </remarks>
     /// </summary>
    public enum StatusList { Invalid = 0, Initialized = 1, Ordered = 2, Reception = 3, Storage = 4, InProgress = 5, Delivery = 6 };

    /// <summary>
    /// This is an enum
    /// Enum <c>StorageSpecification</c> represents the storage specifications of a package.
    /// <remarks>
    /// The enumerators in this enum declare various storage specifications:
    /// <list type="bullet">
    ///     <item><description>Invalid (0): Indicates an invalid storage specification.</description></item>
    ///     <item><description>ClimateControlled (1): The package requires climate-controlled storage.</description></item>
    ///     <item><description>SmallItems (2): The package contains small items.</description></item>
    ///     <item><description>HighValue (3): The package contains high-value items.</description></item>
    /// </list>
    /// </remarks>
    /// </summary>
    public enum StorageSpecification { Invalid = 0, ClimateControlled = 1, SmallItems = 2, HighValue = 3 };

    public class Package : IPackage
    {

        /// <summary>
        ///     Package object.
        /// </summary>
        private readonly static List<int> idCheck = [];
        private readonly int packageId, packageLenghtInCm, packageHeightInCm, packageDepthInCm, packageWeighInGrams;
        private readonly bool isFragile;
        private readonly StorageSpecification specification;
        private StatusList status;
        private Contact sender;
        private Contact receiver;
        public DateTime DeliveryTime;
        readonly PackageLog packageLog = new();
        private Aisle? packageAisle = null;

        //shipment number feature ''attempts''
        //Skal prøve å gi hver pakke instanse av Package som er oprettet en random forsendelse nummer.
        //forsendel nummeret er generert i konstrøktøren
        private static readonly Random random = new();
        private int shipmentNumber;

        //Events
        internal event EventHandler<PackageStatusChangedEventArgs> PackageStatusChangedEvent;
        internal event EventHandler<PackageStatusChangedEventArgs> PackageSenderChangedEvent;
        internal event EventHandler<PackageStatusChangedEventArgs> PackageReceiverChangedEvent;
        internal virtual void OnPackageStatusChanged(PackageStatusChangedEventArgs e)
        {
            PackageStatusChangedEvent?.Invoke(this, e);
        }
        internal virtual void OnPackageSenderChanged(PackageStatusChangedEventArgs e)
        {
            PackageSenderChangedEvent?.Invoke(this, e);
        }
        internal virtual void OnPackageReceiverChanged(PackageStatusChangedEventArgs e)
        {
            PackageReceiverChangedEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <param name="packageLenghtInCm">The package length in Centimeters.</param>
        /// <param name="packageHeightInCm">The package height in Centimeters.</param>
        /// <param name="packageDepthInCm">The package depth in Centimeters.</param>
        /// <param name="packageWeightInGrams">The package weight in grams.</param>
        /// <param name="isFragile">if set to <c>true</c> [is fragile].</param>
        /// <param name="specification">The spesification of the package as ENUM.</param>
        public Package(int packageId, int packageLenghtInCm, int packageHeightInCm, int packageDepthInCm, int packageWeightInGrams, bool isFragile, StorageSpecification specification)
        {
            if (idCheck.Contains(packageId))
            {
                throw new Exception("The id: " + packageId + " is not unique...");
            }
            else
            {
                this.packageId = packageId;
                this.packageLenghtInCm = packageLenghtInCm;
                this.packageHeightInCm = packageHeightInCm;
                this.packageDepthInCm = packageDepthInCm;
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
        public int PackageLengthInCm => packageLenghtInCm;
        public int PackageHeightInCm => packageHeightInCm;
        public int PackageDepthInCm => packageDepthInCm;
        public int PackageWeightInGrams => packageWeighInGrams;
        public bool IsFragile => isFragile;
        public StorageSpecification Specification => specification;
        public Aisle? PackageAisle => packageAisle;
        public Contact Sender { 
            get { return this.sender; }
            set 
            {
                Contact oldValue = this.receiver;
                this.sender = value;
                OnPackageSenderChanged(new PackageStatusChangedEventArgs(this, oldValue));
            } 
        }
        public Contact Receiver { 
            get { return this.receiver; }
            set 
            {
                Contact oldValue = this.receiver;
                this.receiver = value;
                OnPackageReceiverChanged(new PackageStatusChangedEventArgs(this, oldValue));    
            }
        }
        public int ShipmentNumber
        {
            get { return shipmentNumber; }
            private set { shipmentNumber = value; }
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
        internal void AddAisle(Aisle? aisle)
        {
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
            OnPackageStatusChanged(new PackageStatusChangedEventArgs(this));
        }

        public PackageLog GetPackageLog()
        {
            return packageLog;
        }

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
