using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TechSupport.WARE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Isle isle = new Isle(20, 20000, 8000, 1000, 500000, 1, 1);

            Package myGamingPc = new Package(1, 2000, 1000, 500, 20000, true, 0);
            Package pakke1 = new Package(1, 2000, 1000, 500, 20000, true, 0);
            Package pakke2 = new Package(1, 2000, 1000, 500, 20000, true, 0);
            Package pakke3 = new Package(2, 3000, 1000, 500, 20000, true, 0);



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

            Console.WriteLine(pakke1.Status);
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
