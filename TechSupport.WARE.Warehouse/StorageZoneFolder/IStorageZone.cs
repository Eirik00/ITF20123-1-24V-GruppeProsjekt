using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IStorageZone
    {

        /// <summary>
        /// Adds aisle to the zone.
        /// </summary>
        public void addAisleToZone(Aisle aisle);
    }
}
