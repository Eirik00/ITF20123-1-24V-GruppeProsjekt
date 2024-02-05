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
        private DateTime timeStamp;
        private StatusList previousStatus;
        private StatusList newStatus;
        private String description;
        private Isle? isle;

        public PackageLogEntry(Isle isle, StatusList newStatus, StatusList previousStatus, String description)
        {
            this.timeStamp = DateTime.Now;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus;
            this.description = description;
            this.isle = isle;
        }
        public PackageLogEntry(Isle isle, StatusList previousStatus, StatusList newStatus)
        {
            this.timeStamp = DateTime.Now;
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
        public String ToString()
        {
            if(this.isle == null) 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: null";
            else 
                return "Package changed from satus: " + previousStatus + " to " + newStatus + " at " + timeStamp + " on isleid: " + this.isle.GetIsleId;

        }    
    }

    public class PackageLog : IPackageLog
    {
        private List<PackageLogEntry> packageHistory = new List<PackageLogEntry>();
        public int Entries => packageHistory.Count;

        public void LogChange(Isle isle, StatusList currentStatus, StatusList previousStatus, String description = "")
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
            List<PackageLogEntry> status1List = new List<PackageLogEntry>();
            List<PackageLogEntry> status2List = new List<PackageLogEntry>();
            TimeSpan timeSpan = TimeSpan.Zero;

            foreach(var entry in packageHistory)
            {
                if(entry.getNewStatus() == status)
                {
                    status1List.Add(entry);
                }
                if(entry.getPreviousStatus() == status)
                {
                    status2List.Add(entry);
                }
            }
            foreach(var entry in status1List)
            {
                try
                {
                    timeSpan += status2List[0].getDateTime() - entry.getDateTime();

                    status2List.RemoveAt(status2List.Count - 1);
                }
                catch(Exception e)
                {
                    timeSpan += DateTime.Now - entry.getDateTime();
                }
            }
            return timeSpan;
        }

        public TimeSpan GetTotalTimeInWarehouse()
        {
            return packageHistory[packageHistory.Count - 1].getDateTime() - packageHistory[0].getDateTime();
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
