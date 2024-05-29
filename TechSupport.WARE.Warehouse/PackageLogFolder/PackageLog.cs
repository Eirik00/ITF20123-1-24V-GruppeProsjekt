using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents an entry in the package log.
    /// </summary>
    public class PackageLogEntry
    {
        private readonly DateTime timeStamp;
        private readonly StatusList previousStatus;
        private readonly StatusList newStatus;
        private readonly String description;
        private readonly Aisle? aisle;
        internal PackageLogEntry(Aisle? aisle, StatusList previousStatus, StatusList newStatus, String description)
        {
            this.timeStamp = DateTime.Now;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus;
            this.description = description;
            this.aisle = aisle;
        }
        internal PackageLogEntry(Aisle? aisle, StatusList previousStatus, StatusList newStatus)
        {
            this.timeStamp = DateTime.Now;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus ;
            this.description = "No Reason Given";
            this.aisle = aisle;
        }
        /// <summary>
        /// StatusList <c>GetPreviousStatus()</c> returns the previous status before status change
        /// </summary>
        /// <returns>StatusList</returns>
        public StatusList GetPreviousStatus()
        {
            return previousStatus;
        }
        /// <summary>
        /// StatusList <c>GetNewStatus()</c> returns the status after the status change.
        /// </summary>
        /// <returns>StatusList</returns>
        public StatusList GetNewStatus() 
        { 
            return newStatus;
        }
        /// <summary>
        /// DateTime <c>GetDateTime()</c> returns the datestamp on when the status change took place.
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTime()
        {
            return timeStamp;
        }
        public override string ToString()
        {
            if(this.aisle == null) 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: null";
            else 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: " + this.aisle.AisleId;

        }
    }
    /// <summary>
    /// A class that contains a list of PackageLogEntries
    /// <para>
    /// and has the following methods:
    /// <br><item>    - <description>LogChange</description></item></br>
    /// <br><item>    - <description>GetEntries</description></item></br>
    /// <br><item>    - <description>GetTimeSpanOnStatus</description></item></br>
    /// <br><item>    - <description>GetTotalTimeInWarehouse</description></item></br>
    /// </para>
    /// </summary>
    public class PackageLog : IPackageLog
    {
        
        internal PackageLog() { } // For å ungå at brukeren kan lage packagelog
        private readonly List<PackageLogEntry> packageHistory = [];
        public int Entries => packageHistory.Count;

        /// <summary>
        /// Logs a change in package status.
        /// </summary>
        /// <param name="isle">The aisle where the package is located.</param>
        /// <param name="currentStatus">The current status of the package.</param>
        /// <param name="previousStatus">The previous status of the package.</param>
        /// <param name="description">An optional description of the change.</param>
        public void LogChange(Aisle? isle, StatusList currentStatus, StatusList previousStatus, String description = "")
        {
            if(description == "")
            {
                packageHistory.Add(new PackageLogEntry(isle, previousStatus, currentStatus));
            }
            else
            {
                packageHistory.Add(new PackageLogEntry(isle, previousStatus, currentStatus, description));
            }
        }
        public List<PackageLogEntry> GetEntries() => packageHistory;

        /// <summary>
        /// Gets the time span during which the package was in a specific status.
        /// </summary>
        /// <param name="status">The status to check.</param>
        /// <returns>The time span the package spent in the specified status.</returns>
        public TimeSpan GetTimeSpanOnStatus(StatusList status)
        {
            List<PackageLogEntry> status1List = [];
            List<PackageLogEntry> status2List = [];
            TimeSpan timeSpan = TimeSpan.Zero;

            foreach(var entry in packageHistory)
            {
                if(entry.GetNewStatus() == status)
                {
                    status1List.Add(entry);
                }
                if(entry.GetPreviousStatus() == status)
                {
                    status2List.Add(entry);
                }
            }
            foreach(var entry in status1List)
            {
                try
                {
                    timeSpan += status2List[0].GetDateTime() - entry.GetDateTime();

                    status2List.RemoveAt(status2List.Count - 1);
                }
                catch(Exception)
                {
                    timeSpan += DateTime.Now - entry.GetDateTime();
                }
            }
            return timeSpan;
        }

        /// <summary>
        /// Gets the total time the package spent in the warehouse.
        /// </summary>
        /// <returns>The total time the package spent in the warehouse.</returns>
        public TimeSpan GetTotalTimeInWarehouse()
        {
            return packageHistory[^1].GetDateTime() - packageHistory[0].GetDateTime();
        }

        public override string ToString()
        {
            String returnString = "";
            foreach (var entry in packageHistory)
            {
                returnString += entry.ToString() + " \n";
            }
            return returnString;
        }
    }
}
