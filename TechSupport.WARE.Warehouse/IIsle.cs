using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    internal interface IIsle
    {
        void AddPackage(Package package, int placement);
        void RemovePackage(Package package);
    }
}
