using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using TechSupport.WARE.Warehouse;
using System.Diagnostics.Tracing;
using TechSupport.WARE.Warehouse.PalletManagement;
using System.Numerics;

namespace TechSupport.WARE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Aisles
            Aisle aisleHighValue = new(1, 1, 20000, 20000, 20000, 2000000000, 1);
            Aisle aisleSmallItems = new(1, 1, 20000, 20000, 20000, 2000000000, 2);
            Simulation sim1 = new();
            AisleHandler aisleHandler1 = new AisleHandler(aisleHighValue, sim1);
            AisleHandler aisleHandler2 = new AisleHandler(aisleSmallItems, sim1);
            StorageZone storageZoneHighValue = new StorageZone(StorageSpecification.HighValue);
            StorageZone storageZoneSmallValue = new StorageZone(StorageSpecification.SmallItems);
            storageZoneHighValue.addAisleToZone(aisleHighValue);
            storageZoneSmallValue.addAisleToZone(aisleSmallItems);

            //Employees
            int amountOfWorkers = 5;
            Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
            for (int i = 1; i <= amountOfWorkers; i++)
            {
                employees.Add(i, new(i, 3, "Dummy", "Dummy", "Dummy@dummy.com", "Dummy", "Dummy", 90909090, 9090));
            }

            //Contact
            Contact dummyContact = new("dummy", "dummy", "dummy@email.com", "dummy", "dummy", 90909090, 9090);

            //Packages
            Package packageHighValue = new(1, 10, 10, 10, 10, false, StorageSpecification.HighValue);
            PackageHandler packageHandler1 = new(packageHighValue);
            Package packageSmallItems = new(2, 10, 10, 10, 10, false, StorageSpecification.SmallItems);
            PackageHandler packageHandler2 = new(packageSmallItems);

            //Outgoing
            Outgoing outgoing1 = new(new());

            //Timer
            int runningTime = 5;
            DateTime timer = DateTime.Now.AddSeconds(runningTime);

            //Simulation-while
            while (DateTime.Now < timer)
            {
                aisleHighValue.AddPackage(packageHighValue, 1, 1, employees[1]);
                aisleSmallItems.AddPackage(packageSmallItems, 1, 1, employees[2]);
                outgoing1.OutgoingPackage(22.10, packageHighValue, dummyContact, dummyContact, employees[1]);
                outgoing1.OutgoingPackage(22.10, packageSmallItems, dummyContact, dummyContact, employees[2]);
                System.Threading.Thread.Sleep(500);
            }

            Console.WriteLine("\nSimulation results\n--------------");
            int totalSimTime = sim1.GetTotalTimeInSeconds();
            Console.WriteLine($"Total time to finish all tasks: {totalSimTime}Seconds");
            Console.WriteLine("Time per worker: ");
            sim1.PrintAllEmployeesTimesInSeconds();
            sim1.StopSimulation();
            Console.WriteLine("--------------\nSimulation done...\n");

            //Aisle aisle1 = new(20, 20, 20000, 20000, 20000, 2000000000, 1);
            //StorageZone storageZone = new StorageZone(StorageSpecification.HighValue);
            //storageZone.addAisleToZone(aisle1);

            ////Simulation
            //

            ////Pakker
            //Dictionary<int, Package> packages = new Dictionary<int, Package>();
            //for(int i = 1; i <= 20; i++)
            //{
            //    packages.Add(i, new(i,200,200,200,200,false,StorageSpecification.HighValue));
            //}

            ////PackageHandlers
            //Dictionary<int, PackageHandler> handlers = new Dictionary<int, PackageHandler>();
            //int idPackagHandler = 1;
            //foreach (KeyValuePair<int,Package> entry in packages)
            //{
            //    handlers.Add(idPackagHandler, new(packages[entry.Key]));
            //    idPackagHandler++;
            //}

            ////Arbeidere
            //

            ////Legge på hylle
            //int hylleplass = 1;
            //foreach (KeyValuePair<int, Package> entry in packages.Take(7))
            //{
            //    aisle1.AddPackage(entry.Value, (1, hylleplass), employees[1]);
            //    hylleplass++;
            //}
            //foreach (KeyValuePair<int, Package> entry in packages.Skip(7).Take(3))
            //{
            //    aisle1.AddPackage(entry.Value, (1, hylleplass), employees[2]);
            //    hylleplass++;
            //}
            //foreach (KeyValuePair<int, Package> entry in packages.Skip(10).Take(10))
            //{
            //    aisle1.AddPackage(entry.Value, (1, hylleplass), employees[2]);
            //    hylleplass++;
            //}

            ////Gjøres klar til sending
            //Outgoing outgoing = new(new());
            //Contact dummyContact = new("dummy", "dummy", "dummy@dummy.com", "dummy", "dummy", 90909090, 9090);
            //foreach (KeyValuePair<int, Package> entry in packages.Take(7))
            //{
            //    outgoing.OutgoingPackage(22.30, entry.Value, dummyContact, dummyContact, employees[1]);
            //}
            //foreach (KeyValuePair<int, Package> entry in packages.Skip(7).Take(3))
            //{
            //    outgoing.OutgoingPackage(22.30, entry.Value, dummyContact, dummyContact, employees[2]);
            //}
            //foreach (KeyValuePair<int, Package> entry in packages.Skip(10).Take(10))
            //{
            //    outgoing.OutgoingPackage(22.30, entry.Value, dummyContact, dummyContact, employees[1]);
            //}




            //TruckManager truckManager = new TruckManager();
            //TruckManagerHandler truckManagerHandler = new(truckManager);
            //Pallet pallet1 = new(2);
            //PalletRack palletRack = new(truckManager);
            //palletRack.AddPalletToRack(1, 2, pallet1);
            //palletRack.RemovePallet(1, 2, 1);



            /*Simulation simulation = new Simulation();
            simulation.simulateSinglePackageMovement();*/
            /*TruckManager truckManager = new TruckManager();
            Package pakke = new Package(2, 2, 2, 2, 2, true, StorageSpecification.Invalid);
            PackageHandler packageHandler = new PackageHandler(pakke);
            pakke.ChangeStatus(StatusList.Invalid);
            Aisle hylle = new Aisle(4, 10, 3000, 5000, 5000, 2000000, StorageSpecification.Invalid, 1);
            AisleHandler aisleHandler = new AisleHandler(hylle);
            Employee arne = new Employee(1, 2, "Arne", "Tang", "ole@email.com", "tollvei12", "Norway", 992229929, 3232);
            hylle.AddPackage(pakke, (1,1), arne);
            Incoming incoming = new();
            IncomingHandler incomingHandler = new IncomingHandler(incoming);
            PackageList packages = new PackageList();
            packages.Add(pakke);
            incoming.IncomingPackage(14.30, packages, new Contact("tt", "lala", "toer@.com", "gaggvei", "Norge", 90909090, 2322), new Contact("tt", "lala", "toer@.com", "gaggvei", "Norge", 90909090, 2322));
            Outgoing outgoing = new(truckManager);
            OutgoingHandler outgoingHandler = new OutgoingHandler(outgoing);
            outgoing.OutgoingPackage(14.30, packages, new Contact("tt", "lala", "toer@.com", "gaggvei", "Norge", 90909090, 2322), new Contact("tt", "lala", "toer@.com", "gaggvei", "Norge", 90909090, 2322));
            */////Aisle isle = new(2, 200, 200, 200, 200000, StorageSpecification.ClimateControlled, 1);
            /*
            Package testPackage = new Package(2, 2, 2, 2, 2, false, StorageSpecification.ClimateControlled, StatusList.Invalid);
            isle.AddPackage(testPackage, 0);
            Thread.Sleep(1000);
            Console.WriteLine("1" + DateTime.Now);
            testPackage.ChangeStatus(StatusList.InProgress);
            Thread.Sleep(10000);
            Console.WriteLine("1" + DateTime.Now);
            testPackage.ChangeStatus(StatusList.Delivery);
            Thread.Sleep(5000);
            Console.WriteLine("1" + DateTime.Now);
            testPackage.ChangeStatus(StatusList.Storage);
            Thread.Sleep(4000);
            Console.WriteLine("1" + DateTime.Now);
            testPackage.ChangeStatus(StatusList.Delivery);
            Console.WriteLine(testPackage.GetPackageLog().GetTimeSpanOnStatus(StatusList.Delivery));
            foreach (var entry in testPackage.GetPackageLog().GetEntries())
            {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine(testPackage.GetPackageLog);
            */

            //            Contact kjell = new Contact("Kjell", "Datamann", "kjell@komplett.no", "Norway", "Stavernveien 2", 90202011, 3550);
            //Company komplett = new Company("Komplett", 9849249, "Stavernveien 2", "Norway", 3550);
            //komplett.ContactPerson = kjell;
            //Contact us = new Contact("Tore", "Tang", "Tore@warehouse.no", "Norway", "Stuegata 2", 90808080, 5055);
            //Company warehouse = new Company("Warehouse", 90808080, "Stuegata 2", "Norway", 5035);
            //warehouse.ContactPerson = us;

            //PackageList packagelist = new();
            //Package pakke1 = new(1, 58, 19, 7, 2000, false, 0);
            //Package pakke2 = new(2, 42, 78, 212, 2000, false, 0);
            //Package pakke3 = new(3, 36, 64, 112, 980, false, 0);
            //Package pakke4 = new(4, 41, 55, 7612, 2000, false, 0);
            //Package pakke5 = new(5, 1235, 54, 312, 2000, false, 0);
            //Package pakke6 = new(6, 2316, 33, 1352, 2030, false, 0);
            //Package pakke7 = new(7, 217, 3452, 8612, 2000, false, 0);
            //Package pakke8 = new(8, 8423, 124, 4312, 2000, false, 0);
            //packagelist.Add(pakke1);
            //packagelist.Add(pakke5);
            //packagelist.Add(pakke8);
            //packagelist.Add(pakke7);
            //packagelist.Add(pakke4);
            //packagelist.Add(pakke2);
            //packagelist.Add(pakke6);
            //packagelist.Add(pakke3);
            //Console.WriteLine(packagelist.SeePackagesInList());
            //packagelist.Add(pakke1);
            //Console.WriteLine(packagelist.SeePackagesInList());
            //packagelist.Remove(pakke1);
            //packagelist.Add(pakke1);
            //Console.WriteLine(packagelist.SeePackagesInList());
            //Package gtx970 = new Package(1, 2500, 500, 1000, 2000, true, StorageSpecification.SmallItems);
            //Package gtx980 = new Package(2, 2500, 500, 1000, 2000, true, StorageSpecification.SmallItems);
            //PackageList graphicCards = new PackageList();
            //graphicCards.Add(gtx970);
            //graphicCards.Add(gtx980);


            ////Outgoing rema = new();

            //Console.WriteLine(packagelist.ShowPackagesSortedByLenght());
            //Console.WriteLine(packagelist.ShowPackagesSortedByHeight());
            //Console.WriteLine(packagelist.ShowPackagesSortedByDepth());
            //Console.WriteLine(packagelist.ShowPackagesSortedByWeight());

            //Outgoing toreGraphic = new Outgoing();
            //Contact toreTang = new Contact("Tore", "Tang", "toreTang@hotmail.com", "Norway", "Stavangerveien 2", 90807060, 4020);
            //toreGraphic.OutgoingPackage(0600, graphicCards, warehouse.ContactPerson, toreTang);

            //Package class has atributes: int productId, int packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeightInGrams,
            //spesification storageSpecifications[], boolean isFragile

            //productId is not uniqe, represents the type of product the package is. (e.g. productId 16327 represents televisions)
            //If no productId is given it defaults NULL. For instance if the package is a personal item that does not belong in any spesification.
            //The parameter is meant for systems belonging to stores and such, not post offices.

            //When a new package is registered it automatically gets put to Reception.

            // Package myPackage = new Package(123456, 400, 500, 200, 400000, 2, 0);

            //Isle class has parameters: int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int capableWeightInGrams,
            //spesification storageSpecifications[]

            //Isle isle1 = new Isle(50, 400, 500, 1000, 100000, 2);

            //isle.addPackage takes the parameters: Package package, int space
            //(if no space parameter is given package is placed in lowest vailable space)

            // isle1.addPackage(myPackage, 1);

            //locate returns isle, spesification and space
            //myPackage.getLocation();

            //return status, is it Reception, Storage, picking or delivery
            // myPackage.getStatus();

            //update status, 1:Reception, 2:Storage, 3:Picking, 4:Delivery
            //myPackage.updateStatus(2);

            //Delivery class has parameters: datetime timeOfDelivery, arraylist packagesInDelivery

            //Delivery delivery_1 = new Delivery();

            //ArrayList deliveryList_1 = new ArrayList();
            //deliveryList_1.Add(myPackage);

            //addPackages(ArrayList/object of Packages)
            //  delivery_1.addPackages(deliveryList_1, new DateTime(2024, 01, 17, 18, 30, 0));

            //Reception collection = new Reception;
        }
    }
}