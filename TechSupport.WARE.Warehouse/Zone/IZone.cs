using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Zone
{
    internal interface IZone
    {
        /// <summary>
        /// Sets the door to be locked.
        /// </summary>
        public void lockDoor();

        /// <summary>
        /// Sets the door to be unlocked.
        /// </summary>
        public void unlockDoor();

        /// <summary>
        /// Adds aisle to the zone.
        /// </summary>
        public void addAisleToZone(Aisle aisle);
    }
}
