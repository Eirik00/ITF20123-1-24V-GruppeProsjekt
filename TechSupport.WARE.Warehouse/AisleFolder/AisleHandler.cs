using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.SimulationFolder;

namespace TechSupport.WARE.Warehouse
{
    public class AisleHandler
    {
        private Simulation _simulation;
        public AisleHandler(Aisle aisle, Simulation sim)
        {
            aisle.PackageAddedToAisle += HandlePackageAddedToAisle;
            _simulation = sim;
            HandleAisle(this, new AisleAndPackageEventArgs(aisle));
        }
        public AisleHandler(Aisle aisle)
        {
            aisle.PackageAddedToAisle += HandlePackageAddedToAisle;
            HandleAisle(this, new AisleAndPackageEventArgs(aisle));
        }

        internal void HandleAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Aisle with ID {e.AisleId} added to the handler.");
            if( _simulation.GetSimulateBool )
            {
                Console.WriteLine($"Simulation for Aisle with ID {e.AisleId} has started.");
            }
        }
        internal void HandlePackageAddedToAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} was added to Aisle with ID {e.AisleId} on shelf {e.Shelf}");
            if( _simulation.GetSimulateBool )
            {
                if(e.StorageSpecification == StorageSpecification.ClimateControlled)
                {
                    int elapsedTime = 70;
                    _simulation.AddToTotalSimulationTime(elapsedTime);
                    Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                }
                if (e.StorageSpecification == StorageSpecification.HighValue)
                {
                    int elapsedTime = 70;
                    _simulation.AddToTotalSimulationTime(elapsedTime);
                    Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                }
                if (e.StorageSpecification == StorageSpecification.SmallItems)
                {
                    int elapsedTime = 110;
                    _simulation.AddToTotalSimulationTime(elapsedTime);
                    Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                }
            }
        }
    }
    internal class AisleAndPackageEventArgs : EventArgs
    {
        internal int AisleId { get; }
        internal int PackageId { get; }
        internal (int, int) Shelf { get; }
        internal StorageSpecification StorageSpecification { get; }

        internal AisleAndPackageEventArgs(Aisle aisle, Package package)
        {
            AisleId = aisle.GetAisleId;
            PackageId = package.PackageId;
            Shelf = aisle.GetShelf(package);
            StorageSpecification = aisle.CurrentStorageZone.StorageSpecification;
        }
        internal AisleAndPackageEventArgs(Aisle aisle)
        {
            AisleId = aisle.GetAisleId;
        }
    }
}
