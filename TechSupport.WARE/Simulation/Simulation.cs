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
            Package gtx980 = new Package(2, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage);
            Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Norway", "Stavernveien 2", 90202011, 3550);
            Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Norway", 3550);
            komplett.ContactPerson = kjell;
            Contact us = new Contact("Tore", "Tang", "Tore@warehouse.no", "Norway", "Stuegata 2", 90808080, 5055);
            Company warehouse = new Company("Warehouse", 90808080, "Stuegata 2", "Norway", 5035);
            warehouse.ContactPerson = us;
            int deliveryTimeKomplettInH = 12;
            int mottakTilHylle = 1;
            int waitForPersonell = 1;

            while (true) {
                Import today = new Import();
                PackageList GraphicCards = new PackageList(1);
                GraphicCards.AddPackage(gtx970);
                GraphicCards.AddPackage(gtx980);

                today.PackageImport(08.00, GraphicCards, komplett, komplett.ContactPerson, warehouse.ContactPerson);
                Thread.Sleep(deliveryTimeKomplettInH);

                Thread.Sleep(waitForPersonell);

                Isle isle1 = new Isle(50, 20000000, 50000, 100000, 2000000, StorageSpecification.DryStorage, 1);
                int count = 1;
                foreach(Package package in GraphicCards.Packages)
                {
                    Thread.Sleep(mottakTilHylle);
                    isle1.AddPackage(package, count);
                    count++;
                }



            }

        }
    }
}