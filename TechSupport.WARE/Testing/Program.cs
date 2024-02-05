using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TechSupport.WARE.Warehouse;


namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            PackageList packagelist = new PackageList(1);
            Package pakke1 = new Package(1, 58, 19, 7, 2000, false, 0);
            Package pakke2 = new Package(2, 42, 78, 212, 2000, false, 0);
            Package pakke3 = new Package(3, 36, 64, 112, 2000, false, 0);
            Package pakke4 = new Package(4, 41, 55, 7612, 2000, false, 0);
            Package pakke5 = new Package(5, 1235, 54, 312, 2000, false, 0);
            Package pakke6 = new Package(6, 2316, 33, 1352, 2000, false, 0);
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

            Console.WriteLine(packagelist.ShowPackagesSortedByLenght());
            Console.WriteLine(packagelist.ShowPackagesSortedByHeight());
            Console.WriteLine(packagelist.ShowPackagesSortedByDepth());
            Console.WriteLine(packagelist.ShowPackagesSortedByWeight());
        }
    }
}
