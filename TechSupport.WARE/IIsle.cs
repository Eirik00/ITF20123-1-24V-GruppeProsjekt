// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Testing Commit");

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    internal interface IIsle
    {
        int isleId { get; set; };
        Packages[] packagesList { get; set; };


    }
}
