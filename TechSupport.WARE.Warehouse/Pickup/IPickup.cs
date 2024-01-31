using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Pickup
{
    internal interface IPickup
    {
        /// <summary>
        /// int <c>getSizeAndAmount</c> counts the amount of equally sized packages and saves them in a Dictionary with value as int.
        /// </summary>
        Dictionary<List<int>, int> getSizeAndAmount();

        /// <summary>
        /// String <c>ToString</c> gives Datetime <c>time</c> for the pickup date, packages sizes and the amount of packages 
        /// with those sizes from an object of <c>Pickup</c>.
        /// </summary>
        String ToString();

        /// <summary>
        /// Gives a List with a List of the packages int <c>PacageHeightInMm</c>, 
        /// int <c>PacageLengthInMm</c> and int <c>PacageDepthInMm</c>
        /// </summary>
        List<List<int>> GetSizes { get; }
    }
}

