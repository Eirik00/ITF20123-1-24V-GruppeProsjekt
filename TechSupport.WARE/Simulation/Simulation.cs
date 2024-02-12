using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE
{
    internal class Simulation
    {
        static void Main(string[] args)
        {
            Package gtx970 = new Package(1, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage);
            Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Norway", "Stavernveien 2", 90202011, 3550);
            Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Stavern", 3550, "")
            int deliveryTimeInH = 10;

        }
    }
}