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
        }
    }
}