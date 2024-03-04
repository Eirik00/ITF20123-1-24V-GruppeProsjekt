using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.EventHandling
{
    internal class Events
    {
        internal Events() { }
        internal delegate void PackageHandler(object sender, EventArgs e);
        public event PackageHandler NewPackageAdded;
        internal virtual void OnNewPackageAdded(EventArgs e)
        {
            NewPackageAdded?.Invoke(this, e);
        }
    }
    internal class EventHandling
    {
        public void HandleNewPackageAdded(object sender, EventArgs e)
        {
            Console.WriteLine("New package added");
        }
    }
}
