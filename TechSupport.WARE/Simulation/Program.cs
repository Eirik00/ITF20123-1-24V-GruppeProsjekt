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
    internal class Program
    {
        static void Main(string[] args)
        {
            //isle isle = new isle(20, 20000, 8000, 1000, 500000, 1, 1);

            //package mygamingpc = new package(1, 2000, 1000, 500, 20000, true, 0);
            //package pakke1 = new package(1, 2000, 1000, 500, 20000, true, 0);
            //package pakke2 = new package(1, 2000, 1000, 500, 20000, true, 0);
            //package pakke3 = new package(2, 3000, 1000, 500, 20000, true, 0);



            //isle.addpackage(mygamingpc, 1);
            //isle.addpackage(pakke1, 10);
            //isle.addpackage(pakke2, 7);
            //isle.addpackage(pakke3, 20);

            //for (int i = 1; i < isle.shelf.count + 1; i++)
            //{
            //    console.writeline(i + ": " + isle.shelf[i]);
            //}
            //isle.removepackage(pakke1);

            //for (int i = 1; i < isle.shelf.count + 1; i++)
            //{
            //    console.writeline(i + ": " + isle.shelf[i]);
            //}

            //pakke1.changestatus(statuslist.delivery);
            //dictionary<datetime, statuslist> pakke1historikk = pakke1.statuslog;

            //foreach (var status in pakke1historikk)
            //{
            //    console.writeline($"{status.key} kjedde det en endring hvor statusen var {status.value}");
            //}
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
            Contact arne = new Contact("Tore", "Torvald", "Arne@gmail.com", "Norway", "Skogata 3", 90112040, 3424);
            Console.WriteLine(arne.Address);

            Package nypakk = new Package(23, 24, 24, 24, 24, true, 1, arne, arne);
            Console.WriteLine(nypakk.Status);
        }
    }
}
