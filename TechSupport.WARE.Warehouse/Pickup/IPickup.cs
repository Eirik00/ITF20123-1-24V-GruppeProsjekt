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
        /// int <c>getSizeAndAmount</c> This method counts the amount of equally sized packages and saves them in a Dictionary with value as int.
        /// </summary>
        /// <returns>Dictionary<c>List<int>, int</c></returns>
        Dictionary<List<int>, int> getSizeAndAmount();

        /// <summary>
        /// String <c>ToString</c> This method gives you Datetime <c>time</c> for the pickup date, packages sizes and the amount of packages with those sizes from an object of <c>Pickup</c>.
        /// </summary>
        /// <returns>String</returns>
        String ToString();

        /// <summary>
        /// List<List<int>> <c>Sizes</c> This method gives you a List with a List of the packages int <c>PacageHeightInMm</c>, int <c>PacageLengthInMm</c> and int <c>PacageDepthInMm</c>
        /// </summary>
        /// <returns>List<c>List<int></c></returns>
        List<List<int>> Sizes { get; }
    }
}

