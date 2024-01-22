using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    internal interface IPackage
    {
//        Isle isle { get; }
//        Category category { get; }
//        int Id { get; set; }

        (Isle isle, String category, int place) getLocation();
        int getStatus();
        string updateStatus();
    }
}
