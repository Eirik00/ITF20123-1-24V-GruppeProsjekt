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
            //Package gtx970 = new Package(1, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage, 1);
            //Package gtx980 = new Package(2, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage, 2);
            //Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Norway", "Stavernveien 2", 90202011, 3550);
            //Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Norway", 3550);
            //komplett.ContactPerson = kjell;
            //Contact us = new Contact("Tore", "Tang", "Tore@warehouse.no", "Norway", "Stuegata 2", 90808080, 5055);
            //Company warehouse = new Company("Warehouse", 90808080, "Stuegata 2", "Norway", 5035);
            //warehouse.ContactPerson = us;
            //int deliveryTimeKomplettInH = 4000;
            //int mottakTilHylle = 1000;
            //int waitForPersonell = 1000;
            //int waitForOrder = 5000;
            //int deliveryTimeStavanger = 3000;
            //int packageTime = 1000;

            //Incoming today = new Incoming();
            //PackageList graphicCards = new PackageList(1);
            //graphicCards.AddPackage(gtx970);
            //graphicCards.AddPackage(gtx980);

            //DateTime startTime = DateTime.Now;

            //Console.WriteLine("True/False");

            //gtx970.GetPackageLog();

            //Console.WriteLine(StorageSpecification.DryStorage == StorageSpecification.ColdStorage);

            //while ((DateTime.Now - startTime).TotalSeconds < 60)
            //{
            //    today.IncomingPackage(08.00, graphicCards, komplett.ContactPerson, warehouse.ContactPerson);
            //    Thread.Sleep(deliveryTimeKomplettInH);
            //    Thread.Sleep(waitForPersonell);
            //    foreach (Package package in graphicCards.Packages)
            //    {
            //        package.ChangeStatus(StatusList.Reception);
            //    }

            //    Aisle isle1 = new Aisle(50, 20000000, 50000, 100000, 3000, StorageSpecification.DryStorage, 1);
            //    int count = 1;
            //    foreach (Package package in graphicCards.Packages)
            //    {
            //        Thread.Sleep(mottakTilHylle);
            //        isle1.AddPackage(package, count);
            //        count++;
            //    }

            //    Thread.Sleep(waitForOrder);
            //    Thread.Sleep(waitForPersonell);
            //    foreach (Package package in graphicCards.Packages)
            //    {
            //        package.PackageAisle.RemovePackage(package);
            //        package.ChangeStatus(StatusList.InProgress);
            //    }

            //    Thread.Sleep(packageTime);
            //    Outgoing toreGraphic = new Outgoing();
            //    Contact toreTang = new Contact("Tore", "Tang", "toreTang@hotmail.com", "Norway", "Stavangerveien 2", 90807060, 4020);
            //    toreGraphic.OutgoingPackage(06.00, graphicCards, warehouse.ContactPerson, toreTang);
            //    Thread.Sleep(deliveryTimeStavanger);


            //    foreach (Package packagesSent in graphicCards.Packages)
            //    {
            //        Console.WriteLine(packagesSent.GetPackageLog().ToString());
            //    }
            //}

            //Console.WriteLine("Simulasjon slutt");

            // Instantiate Packages
            Package gtx970 = new Package(1, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage, 1);
            Package gtx980 = new Package(2, 2500, 500, 1000, 2000, true, StorageSpecification.DryStorage, 2);

            // Create Contacts
            Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Norway", "Stavernveien 2", 90202011, 3550);
            Contact us = new Contact("Tore", "Tang", "Tore@warehouse.no", "Norway", "Stuegata 2", 90808080, 5055);

            // Create Companies and Assign Contact Persons
            Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Norway", 3550);
            komplett.ContactPerson = kjell;

            Company warehouse = new Company("Warehouse", 90808080, "Stuegata 2", "Norway", 5035);
            warehouse.ContactPerson = us;

            // Create Incoming and Outgoing Instances
            Incoming today = new Incoming();
            Outgoing outgoing = new Outgoing();

            // Create Package List and Add Packages
            PackageList graphicCards = new PackageList(1);
            graphicCards.AddPackage(gtx970);
            graphicCards.AddPackage(gtx980);

            // Test Incoming and Outgoing Methods
            // For example:
            today.IncomingPackage(14.30, graphicCards, komplett.ContactPerson, warehouse.ContactPerson);
            outgoing.OutgoingPackage(15.30, graphicCards, warehouse.ContactPerson, komplett.ContactPerson);

            // Check Results
            Console.WriteLine("Incoming Packages:");
            Console.WriteLine(today.ToString());

            Console.WriteLine("Outgoing Packages:");
            Console.WriteLine(outgoing.ToString());

            // Test other scenarios as needed

            // Test the dynamic shipment function
            int deliveryTimeKomplettInH = 4000;
            int mottakTilHylle = 1000;
            int waitForPersonell = 1000;
            int waitForOrder = 5000;
            int deliveryTimeStavanger = 3000;
            int packageTime = 1000;

            // Assuming there's a method to calculate dynamic shipment time
            int dynamicShipmentTime = CalculateDynamicShipmentTime(deliveryTimeKomplettInH, mottakTilHylle, waitForPersonell, waitForOrder, deliveryTimeStavanger, packageTime);
            Console.WriteLine($"Dynamic Shipment Time: {dynamicShipmentTime} minutes");

            // Test other functionalities as needed
        }

        // Example of a method to calculate dynamic shipment time
        static int CalculateDynamicShipmentTime(int deliveryTimeKomplettInH, int mottakTilHylle, int waitForPersonell, int waitForOrder, int deliveryTimeStavanger, int packageTime)
        {
            // Perform calculations based on the given parameters
            int dynamicShipmentTime = deliveryTimeKomplettInH + mottakTilHylle + waitForPersonell + waitForOrder + deliveryTimeStavanger + packageTime;
            return dynamicShipmentTime;
        }
    }
}