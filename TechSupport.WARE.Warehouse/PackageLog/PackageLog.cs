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
        private static DateTime timeStamp = DateTime.Now;
        private StatusList previousStatus;
        private StatusList newStatus;
        private String description;
        private Isle isle;

        public PackageLogEntry(Isle isle, StatusList newStatus, StatusList previousStatus, String description)
        {
            this.previousStatus = previousStatus;
            this.newStatus = newStatus;
            this.description = description;
            this.isle = isle;
        }
        public PackageLogEntry(Isle isle, StatusList previousStatus, StatusList newStatus)
        {
            this.previousStatus = previousStatus;
            this.newStatus = newStatus ;
            this.description = "No Reason Given";
            this.isle = isle;
        }

        public StatusList getPreviousStatus()
        {
            return previousStatus;
        }
        public StatusList getNewStatus() 
        { 
            return newStatus;
        }
        public DateTime getDateTime()
        {
            return timeStamp;
        }
        public String ToString() => "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: " + this.isle.GetIsleId();
    }

    public class PackageLog : IPackageLog
    {
        private List<PackageLogEntry> pacakgeHistory = new List<PackageLogEntry>();
        public int Entries => pacakgeHistory.Count;

        public void LogChange(Isle isle, StatusList currentStatus, StatusList previousStatus, String description = "")
        {
            if(description == "")
            {
                pacakgeHistory.Add(new PackageLogEntry(isle, previousStatus, currentStatus));
            }
            else
            {
                pacakgeHistory.Add(new PackageLogEntry(isle, previousStatus, currentStatus, description));
            }
        }
        public List<PackageLogEntry> GetEntries() => pacakgeHistory;
    }
}
