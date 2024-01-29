using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TechSupport.WARE.Warehouse;
using TechSupport.WARE.Warehouse.Pickup;

namespace TechSupport.WARE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Isle isle = new Isle(20, 20000, 8000, 1000, 500000, 1, 1);
            Contact tore = new Contact("rjkjkjkdfg", "vfhf", "tuisddfs", "dfssdfe", "sdajhksdh", 9281645, 2742);
            Contact sender = new Contact("Ole", "Dole", "ole@hiof.no", "Halden", "123 Halden Veien", 1234567890, 12345);
            Contact mottaker = new Contact("Per", "Person", "per@hiof.no", "Halden", "123 Halden Gate1", 987643210, 11111);

            Package myGamingPc = new Package(1, 2000, 1000, 500, 20000, true, 0);
            Package pakke1 = new Package(1, 2000, 1000, 500, 20000, true, 0);
            Package pakke2 = new Package(2, 2000, 1000, 500, 20000, true, 0);
            Package pakke3 = new Package(3, 3000, 1000, 500, 20000, true, 0);
            Package pakke4 = new Package(4, 4000, 2000, 550, 22000, true, 0);



            isle.AddPackage(myGamingPc, 1);
            isle.AddPackage(pakke1, 10);
            isle.AddPackage(pakke2, 7);
            isle.AddPackage(pakke3, 20);

            for(int i = 1; i < isle.shelf.Count+1; i++)
            {
                Console.WriteLine(i + ": " + isle.shelf[i]);
            }
            isle.RemovePackage(pakke1);

            for (int i = 1; i < isle.shelf.Count+1; i++)
            {
                Console.WriteLine(i + ": " + isle.shelf[i]);
            }

            pakke1.ChangeStatus(StatusList.Delivery);

            List<PackageLogEntry> pakke1Historikk = pakke1.GetPackageLog();

            foreach(var entry in pakke1Historikk)
            {
                Console.WriteLine(entry.ToString());
            }

            PackagesList packagesList = new PackagesList(1);

            packagesList.addPackage(pakke1);
            packagesList.addPackage(pakke2);
            packagesList.addPackage(pakke3);
            packagesList.addPackage(pakke3);

            Console.WriteLine(packagesList.seePackagesInList());

            packagesList.removePackage(pakke2);

            Console.WriteLine(packagesList.seePackagesInList());

            //////////////////////////////////////// Delivery Simulation Start ///////////////////////////////////////////////////
            Delivery delivery = new Delivery();

            delivery.PackageDelivery(DateTime.Now.AddDays(1), new List<Package> { myGamingPc }, sender, mottaker);

            delivery.RecurringDailyPackageDelivery(TimeSpan.FromHours(14), new List<Package> { pakke2 }, sender, mottaker);

            DayOfWeek[] deliveryDays = { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday };
            delivery.RecurringWeeklyPackageDelivery(deliveryDays, TimeSpan.FromHours(10), new List<Package> { pakke3 }, sender, mottaker);

            //ToString utskrift
            Console.WriteLine(delivery.ToString());

            Pickup fedEx = new Pickup(delivery);
            Console.WriteLine(fedEx.ToString());
            ///////////////////////////////////////////// Delivery Simulation End ///////////////////////////////////////////////////////////////////////

            //Package class has atributes: int productId, int packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeightInGrams,
            //category storageSpecifications[], boolean isFragile

            //productId is not uniqe, represents the type of product the package is. (e.g. productId 16327 represents televisions)
            //If no productId is given it defaults NULL. For instance if the package is a personal item that does not belong in any category.
            //The parameter is meant for systems belonging to stores and such, not post offices.

            //When a new package is registered it automatically gets put to Reception.

            //            Package myPackage = new Package(123456, 400, 500, 200, 400000, 2, 0);

            //Isle class has parameters: int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int capableWeightInGrams,
            //category storageSpecifications[]

            //            Isle isle1 = new Isle(50, 400, 500, 1000, 100000, 2);

            //isle.addPackage takes the parameters: Package package, int space
            //(if no space parameter is given package is placed in lowest vailable space)

            //            isle1.addPackage(myPackage, 1);

            //locate returns isle, category and space
            //            myPackage.getLocation();

            //return status, is it Reception, Storage, picking or delivery
            //            myPackage.getStatus();

            //update status, 1:Reception, 2:Storage, 3:Picking, 4:Delivery
            //            myPackage.updateStatus(2);

            //Delivery class has parameters: datetime timeOfDelivery, arraylist packagesInDelivery

            //Delivery delivery_1 = new Delivery();

            //            ArrayList deliveryList_1 = new ArrayList();
            //            deliveryList_1.Add(myPackage);

            //addPackages(ArrayList/object of Packages)
            //            delivery_1.addPackages(deliveryList_1);

            //Reception collection = new Reception;
        }
    }
}
