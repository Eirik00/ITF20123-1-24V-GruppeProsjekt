using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse;

public class EventHandling : IEventHandling
{
    public void NewPackageAdded(object sender, EventArgs e)
    {
        Console.WriteLine("The status was changed");
    }

}
