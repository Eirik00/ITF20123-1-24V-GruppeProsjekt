using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Zone
{
    public class StorageZone : IStorageZone
    {
        private bool doorIsLocked;
        private List<Aisle> aislesInZone;
        private StorageSpecification storageSpecification;

        public StorageZone(StorageSpecification storageSpecification)
        {
            this.doorIsLocked = false;
            this.aislesInZone = new List<Aisle>();
            this.storageSpecification = storageSpecification;
        }

        public StorageSpecification StorageSpecification => this.storageSpecification;

        public void lockDoor()
        {
            doorIsLocked = true;
        }

        public void unlockDoor()
        {
            doorIsLocked = false;
        }

        public void addAisleToZone(Aisle aisle)
        {
            aislesInZone.Add(aisle);
            aisle.currentStorageZone = this;
        }
    }
}
