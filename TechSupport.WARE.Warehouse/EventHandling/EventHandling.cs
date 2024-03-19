using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse;

public class EventHandling : IEventHandling
{
    //Package
    public void StatusChangeEvent(object sender, EventArgs e)
    {
        Console.WriteLine("The status was changed");
    }
    //Aisle
    public void NewPackageAddedToShelfEvent(object sender, Package package)
    {
        Console.WriteLine("New package added to shelf, with shipment number: " + package.ShipmentNumber);
    }
    //Delivery
    public void NewPackageOrderedEvent(object sender, PackageList packages)
    {
        Console.WriteLine(packages.Count + " Packages is being delivered to the warehouse.");
    }
    public void NewPackageSentEvent(object sender, PackageList packages)
    {
        Console.WriteLine(packages.Count + " Packages is being sent out of warehouse.");
    }
    //Simulation
}
