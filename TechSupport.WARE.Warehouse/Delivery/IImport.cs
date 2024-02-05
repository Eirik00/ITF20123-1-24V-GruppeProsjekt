using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IImport
    {
        List<Package> ImportPackagesList { get; }

        /// <summary>
        /// <c>PackageExport</c> sets the time for a delivery with a List of packages. It also sets the Contact sender and Contact reciever for the delivery.
        /// </summary>
        void PackageImport(DateTime deliveryTime, List<Package> packages, Contact sender);

        /// <summary>
        /// 
        /// </summary>
        void DailyPackageImport(int deliveryHour, List<Package> packages, Contact sender);

        /// <summary>
        /// 
        /// </summary>
        void WeeklyPackageImport(DayOfWeek deliveryDay, int deliveryHour, List<Package> packages, Contact sender);

        /// <summary>
        /// 
        /// </summary>
        String ToString();
    }
}

