using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IPackageLog
    {
        /// <summary>
        /// int <c>Entries</c> contains the ammount of status changes to the package history
        /// </summary>
        int Entries { get; }
        
        /// <summary>
        /// void <c>LogChange</c> This method is to decleare a change in the package status.
        /// It will add the isle, current status, previous status and description(if given)
        /// </summary>
        /// <param name="isle">The Isle which the package is located</param>
        /// <param name="currentStatus">The package current status/ its new status</param>
        /// <param name="previousStatus">The packages previous status before the status change</param>
        /// <param name="description">*Optional* Description/reasoning for the status change</param>
        void LogChange(Isle isle, StatusList currentStatus, StatusList previousStatus, String description);

        /// <summary>
        /// List <c>PackageLogEntry</c> GetEntries() will return the <c>PackageLogEntry</c> objects in a list that are linked to the package.
        /// </summary>
        /// <returns>List <c>PackageLogEntry</c></returns>
        List<PackageLogEntry> GetEntries();

        TimeSpan GetTimeSpanOnStatus(StatusList status);

        TimeSpan GetTotalTimeInWarehouse();
    }
}
