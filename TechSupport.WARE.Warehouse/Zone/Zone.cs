using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Zone
{
    internal class Zone : IZone
    {
        private bool doorIsLocked;
        private List<Aisle> aislesInZone;
        private StorageSpecification storageSpecification;

        public Zone(StorageSpecification storageSpecification)
        {
            this.doorIsLocked = false;
            this.aislesInZone = new List<Aisle>();
            this.storageSpecification = storageSpecification;
        }

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
        }
    }
}
