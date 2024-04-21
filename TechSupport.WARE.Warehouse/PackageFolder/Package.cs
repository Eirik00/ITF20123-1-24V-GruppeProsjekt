﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
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
        /// <param name="packageLenghtInMm">The package length in millimeters.</param>
        /// <param name="packageHeightInMm">The package height in millimeters.</param>
        /// <param name="packageDepthInMm">The package depth in millimeters.</param>
        /// <param name="packageWeightInGrams">The package weight in grams.</param>
        /// <param name="isFragile">if set to <c>true</c> [is fragile].</param>
        /// <param name="specification">The package specification.</param>
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
        internal void AddAisle(Aisle aisle)
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