using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Isle isle = new Isle(2, 200, 200, 200, 200000, 4, 1);
            /*
            Package testPackage = new Package(2, 2, 2, 2, 2, false, StorageSpecification.ColdStorage, StatusList.Invalid);
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

            PackageList packagelist = new PackageList(1);
            Package pakke1 = new Package(1, 58, 19, 7, 2000, false, 0);
            Package pakke2 = new Package(2, 42, 78, 212, 2000, false, 0);
            Package pakke3 = new Package(3, 36, 64, 112, 980, false, 0);
            Package pakke4 = new Package(4, 41, 55, 7612, 2000, false, 0);
            Package pakke5 = new Package(5, 1235, 54, 312, 2000, false, 0);
            Package pakke6 = new Package(6, 2316, 33, 1352, 2030, false, 0);
            Package pakke7 = new Package(7, 217, 3452, 8612, 2000, false, 0);
            Package pakke8 = new Package(8, 8423, 124, 4312, 2000, false, 0);
            packagelist.AddPackage(pakke1);
            packagelist.AddPackage(pakke5);
            packagelist.AddPackage(pakke8);
            packagelist.AddPackage(pakke7);
            packagelist.AddPackage(pakke4);
            packagelist.AddPackage(pakke2);
            packagelist.AddPackage(pakke6);
            packagelist.AddPackage(pakke3);
            Console.WriteLine(packagelist.SeePackagesInList());
            packagelist.AddPackage(pakke1);
            Console.WriteLine(packagelist.SeePackagesInList());
            packagelist.RemovePackage(pakke1);
            packagelist.AddPackage(pakke1);
            Console.WriteLine(packagelist.SeePackagesInList());

            Export rema = new Export();

            Console.WriteLine(packagelist.ShowPackagesSortedByLenght());
            Console.WriteLine(packagelist.ShowPackagesSortedByHeight());
            Console.WriteLine(packagelist.ShowPackagesSortedByDepth());
            Console.WriteLine(packagelist.ShowPackagesSortedByWeight());

            //Package class has atributes: int productId, int packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeightInGrams,
            //category storageSpecifications[], boolean isFragile

            //productId is not uniqe, represents the type of product the package is. (e.g. productId 16327 represents televisions)
            //If no productId is given it defaults NULL. For instance if the package is a personal item that does not belong in any category.
            //The parameter is meant for systems belonging to stores and such, not post offices.

            //When a new package is registered it automatically gets put to Reception.

            // Package myPackage = new Package(123456, 400, 500, 200, 400000, 2, 0);

            //Isle class has parameters: int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int capableWeightInGrams,
            //category storageSpecifications[]

            //Isle isle1 = new Isle(50, 400, 500, 1000, 100000, 2);

            //isle.addPackage takes the parameters: Package package, int space
            //(if no space parameter is given package is placed in lowest vailable space)

            // isle1.addPackage(myPackage, 1);

            //locate returns isle, category and space
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