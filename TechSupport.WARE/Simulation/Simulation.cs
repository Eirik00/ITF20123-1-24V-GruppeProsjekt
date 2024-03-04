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
            Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Stavernveien 2", "Norway", 90202011, 3550);
            Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Norway", 3550);
            komplett.ContactPerson = kjell;
            Contact us = new Contact("Tore", "Tang", "Tore@warehouse.no", "Stuegata 2", "Norway", 90808080, 5055);
            Company warehouse = new Company("Warehouse", 90808080, "Stuegata 2", "Norway", 5035);
            warehouse.ContactPerson = us;
            int deliveryTimeKomplettInH = 4000;
            int mottakTilHylle = 1000;
            int waitForPersonell = 1000;
            int waitForOrder = 5000;
            int deliveryTimeStavanger = 3000;
            int packageTime = 1000;

            Incoming today = new Incoming();
            PackageList graphicCards = new PackageList();
            graphicCards.Add(gtx970);
            graphicCards.Add(gtx980);

            DateTime startTime = DateTime.Now;

            Console.WriteLine("True/False");

            gtx970.GetPackageLog();

            Console.WriteLine(StorageSpecification.DryStorage == StorageSpecification.ColdStorage);

            while ((DateTime.Now - startTime).TotalSeconds < 60)
            {
                today.IncomingPackage(08.00, graphicCards, komplett.ContactPerson, warehouse.ContactPerson);
                Thread.Sleep(deliveryTimeKomplettInH);
                Thread.Sleep(waitForPersonell);
                foreach (Package package in graphicCards.Packages)
                {
                    package.ChangeStatus(StatusList.Reception);
                }

                Aisle isle1 = new Aisle(50000, 20000000, 50000, 100000, 3000000, StorageSpecification.DryStorage, 1);
                int count = 1;
                foreach (Package package in graphicCards.Packages)
                {
                    Thread.Sleep(mottakTilHylle);
                    isle1.AddPackage(package, count);
                    count++;
                }

                Thread.Sleep(waitForOrder);
                Thread.Sleep(waitForPersonell);
                foreach (Package package in graphicCards.Packages)
                {
                    package.PackageAisle.RemovePackage(package);
                    package.ChangeStatus(StatusList.InProgress);
                }

                Thread.Sleep(packageTime);
                Outgoing toreGraphic = new Outgoing();
                Contact toreTang = new Contact("Tore", "Tang", "toreTang@hotmail.com", "Norway", "Stavangerveien 2", 90807060, 4020);
                toreGraphic.OutgoingPackage(06.00, graphicCards, warehouse.ContactPerson, toreTang);
                Thread.Sleep(deliveryTimeStavanger);


                foreach (Package packagesSent in graphicCards.Packages)
                {
                    Console.WriteLine(packagesSent.GetPackageLog().ToString());
                }
            }

            Console.WriteLine("Simulasjon slutt");

            ////shipmentnumber testing:
            //// Create package objects
            //Package package1 = new Package(1, 100, 50, 30, 2000, false, StorageSpecification.DryStorage);
            //Package package2 = new Package(2, 150, 70, 40, 3000, true, StorageSpecification.ColdStorage);
            //Package package3 = new Package(3, 120, 60, 35, 2500, false, StorageSpecification.DryStorage);

            //// Output shipment numbers
            //Console.WriteLine($"Package 1 Shipment Number: {package1.ShipmentNumber}");
            //Console.WriteLine($"Package 2 Shipment Number: {package2.ShipmentNumber}");
            //Console.WriteLine($"Package 3 Shipment Number: {package3.ShipmentNumber}");
        }
    }
}