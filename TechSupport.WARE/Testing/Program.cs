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

            Console.WriteLine(packagelist.ShowPackagesSortedByLenght());
            Console.WriteLine(packagelist.ShowPackagesSortedByHeight());
            Console.WriteLine(packagelist.ShowPackagesSortedByDepth());
            Console.WriteLine(packagelist.ShowPackagesSortedByWeight());
        }
    }
}