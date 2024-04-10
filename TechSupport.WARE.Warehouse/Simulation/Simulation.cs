using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.PalletManagement;

namespace TechSupport.WARE.Warehouse.Simulation
{
    public class Simulation
    {
        public void simulateSinglePackageMovement()
        {
            try
            {
                Package package = new(1, 0, 0, 0, 0, false, StorageSpecification.Invalid);
                PackageHandler packageHandler = new PackageHandler(package);

                Aisle simulationAisle = new(10, 10, 8000, 5000, 2000, 20000000, StorageSpecification.Invalid, 1);
                Employee simEmployee = new(1, 2, "Sim", "Man", "fake@email.com", "SomewhereStreet 20", "North Pole", 11223344, 1234);
                Contact customer = new("Incoming", "Man", "Incoming@Package.com", "IDK 3", "Somewhere", 12345678, 2222);

                Incoming incomingOrder = new();
                IncomingHandler incomingOrderHandler = new(incomingOrder);

                TruckManager truckManager = new TruckManager();
                Outgoing outgoingOrder = new(truckManager);
                OutgoingHandler outgoingOrderHandler = new(outgoingOrder);

                simulationAisle.AddPackage(package, (1, 1), simEmployee);

                incomingOrder.IncomingPackage(14.30, package, customer, simEmployee);

                outgoingOrder.OutgoingPackage(15.30, package, customer, simEmployee);
            }
            catch(Exception e)
            {
                throw;
            }

        }
        public bool simulateMultiPackageMovement()
        {
            try
            {
                PackageList packageList = new PackageList();
                for (int i = 0; i < 5; i++)
                {
                    packageList.Add(new(i, 0, 0, 0, 0, false, StorageSpecification.Invalid));
                    PackageHandler packageHandler = new PackageHandler(packageList[i]);
                }
                Aisle simulationAisle = new(10, 10, 8000, 5000, 2000, 20000000, StorageSpecification.Invalid, 1);
                Employee simEmployee = new(1, 2, "Sim", "Man", "fake@email.com", "SomewhereStreet 20", "North Pole", 11223344, 1234);
                Contact customer = new("Incoming", "Man", "Incoming@Package.com", "IDK 3", "Somewhere", 12345678, 2222);

                Incoming incomingOrder = new();
                IncomingHandler incomingOrderHandler = new(incomingOrder);

                TruckManager truckManager = new TruckManager();
                Outgoing outgoingOrder = new(truckManager);
                OutgoingHandler outgoingOrderHandler = new(outgoingOrder);

                for (int i = 0; i < packageList.Count; i++)
                    simulationAisle.AddPackage(packageList[i], (i + 1, 1), simEmployee);


                return true;
            }catch(Exception e)
            {
                return false;
            }

        }
    }
}
