using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class PackageLogEntry
    {
        private readonly DateTime timeStamp;
        private readonly StatusList previousStatus;
        private readonly StatusList newStatus;
        private readonly String description;
        private readonly Isle? isle;
        public PackageLogEntry(Isle? isle, StatusList newStatus, StatusList previousStatus, String description)
        {
            this.timeStamp = DateTime.Now;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus;
            this.description = description;
            this.isle = isle;
        }
        public PackageLogEntry(Isle? isle, StatusList previousStatus, StatusList newStatus)
        {
            this.timeStamp = DateTime.Now;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus ;
            this.description = "No Reason Given";
            this.isle = isle;
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
            if(this.isle == null) 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: null";
            else 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: " + this.isle.GetIsleId;

        }    
    }

    public class PackageLog : IPackageLog
    {
        private readonly List<PackageLogEntry> packageHistory = [];
        public int Entries => packageHistory.Count;

        public void LogChange(Isle? isle, StatusList currentStatus, StatusList previousStatus, String description = "")
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
