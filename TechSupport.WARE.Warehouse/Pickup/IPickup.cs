using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Pickup
{
    internal interface IPickup
    {
        Dictionary<List<int>, int> getSizeAndAmount();
        string ToString();
        List<List<int>> Sizes { get; }
    }
}
