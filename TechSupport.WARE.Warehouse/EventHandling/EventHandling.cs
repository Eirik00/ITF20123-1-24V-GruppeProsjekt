using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse;

public class EventHandling : IEventHandling
{
    public void StatusChange(object sender, EventArgs e)
    {
        Console.WriteLine("The status was changed");
    }
    public void NewPackageAddedToShelf(object sender, Package package)
    {
        Console.WriteLine("New package added to shelf, with shipment number: " + package.ShipmentNumber );
    }
    public void NewPackageOrdered(object sender, PackageList packages)
    {
        Console.WriteLine(packages.Count + " Packages is being delivered to the warehouse.");
    }
    public void NewPackageSent(object sender, PackageList packages)
    {
        Console.WriteLine(packages.Count + " Packages is being sent out of warehouse.");
    }
}
