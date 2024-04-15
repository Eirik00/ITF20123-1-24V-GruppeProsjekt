using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents a storage zone in the warehouse.
    /// </summary>
    public class StorageZone : IStorageZone
    {
        private int storageZoneAccessLevel;
        private List<Aisle> aislesInZone;
        private StorageSpecification storageSpecification;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageZone"/> class with the specified storage specification.
        /// </summary>
        /// <param name="storageSpecification">The storage specification for the storage zone.</param>
        public StorageZone(StorageSpecification storageSpecification)
        {
            this.aislesInZone = new List<Aisle>();
            this.storageSpecification = storageSpecification;
        }

        public StorageSpecification StorageSpecification => this.storageSpecification;

        /// <summary>
        /// Gets or sets the access level of the storage zone.
        /// </summary>
        /// <exception cref="FormatException">Thrown when the access level is set to a negative integer.</exception>
        public int StorageZoneAccessLevel
        {
            get => storageZoneAccessLevel;
            set
            {
                if (storageZoneAccessLevel < 0)
                    throw new FormatException($"Door access level: {storageZoneAccessLevel}, cannot be a negative integer.");
                this.storageZoneAccessLevel = value;
            }
        }

        /// <summary>
        /// Adds an aisle to the storage zone.
        /// </summary>
        /// <param name="aisle">The aisle to add to the storage zone.</param>
        public void addAisleToZone(Aisle aisle)
        {
            aislesInZone.Add(aisle);
            aisle.CurrentStorageZone = this;
        }
    }
}
