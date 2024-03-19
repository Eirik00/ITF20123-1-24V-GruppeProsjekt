using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class StorageZone : IStorageZone
    {
        private int storageZoneAccessLevel;
        private List<Aisle> aislesInZone;
        private StorageSpecification storageSpecification;

        public StorageZone(StorageSpecification storageSpecification)
        {
            this.aislesInZone = new List<Aisle>();
            this.storageSpecification = storageSpecification;
        }

        public StorageSpecification StorageSpecification => this.storageSpecification;

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

        public void addAisleToZone(Aisle aisle)
        {
            aislesInZone.Add(aisle);
            aisle.currentStorageZone = this;
        }
    }
}
