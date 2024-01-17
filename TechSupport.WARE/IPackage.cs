using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    internal interface IPackage
    {
        Isle isle { get; }
        Category category { get; }

    }
}
