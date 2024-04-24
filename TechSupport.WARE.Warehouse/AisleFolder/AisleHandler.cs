using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.SimulationFolder;
using TechSupport.WARE.Warehouse.SimulationFolder.Simulation;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Manages aisle operations and handles events related to package management within an aisle.
    /// </summary>
    public class AisleHandler
    {
        private Simulation _simulation;

        /// <summary>
        /// Initializes a new instance of the AisleHandler class and subscribes event handlers to the specified aisle. 
        /// 
        /// It has overloading for Simulation.
        /// </summary>
        /// <param name="aisle">The aisle to manage.</param>
        public AisleHandler(Aisle aisle)
        {
            aisle.PackageAddedToAisle += HandlePackageAddedToAisle;
            aisle.PackageRemovedFromAisle += HandlePackageRemovedFromAisle;
            HandleAisle(this, new AisleAndPackageEventArgs(aisle));
        }

        /// <summary>
        /// Initializes a new instance of the AisleHandler class and subscribes event handlers to the specified aisle. 
        /// 
        /// It has overloading for Simulation.
        /// </summary>
        /// <param name="aisle">The aisle to manage.</param>
        /// <param name="sim">Simulation context for handling events.</param>
        public AisleHandler(Aisle aisle, Simulation sim)
        {
            aisle.PackageAddedToAisle += HandlePackageAddedToAisle;
            aisle.PackageRemovedFromAisle += HandlePackageRemovedFromAisle;
            _simulation = sim;
            HandleAisle(this, new AisleAndPackageEventArgs(aisle));
        }

        internal void HandleAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Aisle with ID {e.AisleId} added to the handler.");
            if (_simulation != null)
            {
                if (_simulation.GetSimulateBool)
                {
                    Console.WriteLine($"Simulation for Aisle with ID {e.AisleId} has started.");
                }
            }
        }
        internal void HandlePackageAddedToAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} was added to Aisle with ID {e.AisleId} on shelf {e.Shelf}");
            if (_simulation != null)
            {
                if (_simulation.GetSimulateBool)
                {
                    if (e.StorageSpecification == StorageSpecification.ClimateControlled)
                    {
                        int elapsedTime = _simulation.AddPackageToClimateControlledAisleEstimate;
                        _simulation.AddSimulationTimeToEmployee(e.Employee, elapsedTime);
                        Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                    }
                    if (e.StorageSpecification == StorageSpecification.HighValue)
                    {
                        int elapsedTime = _simulation.AddPackageToHighValueGoodsAisleEstimate;
                        _simulation.AddSimulationTimeToEmployee(e.Employee, elapsedTime);
                        Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                    }
                    if (e.StorageSpecification == StorageSpecification.SmallItems)
                    {
                        int elapsedTime = _simulation.AddPackageToSmallItemsAisleEstimate;
                        _simulation.AddSimulationTimeToEmployee(e.Employee, elapsedTime);
                        Console.WriteLine($"Time elapsed for placing Package {e.PackageId} is {elapsedTime}s.");
                    }
                }
            }
        }
        internal void HandlePackageRemovedFromAisle(object sender, AisleAndPackageEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} was removed from Aisle with ID {e.AisleId} on shelf {e.Shelf}");
            if (_simulation != null)
            {
                if (_simulation.GetSimulateBool)
                {
                    if (e.Status == StatusList.Delivery)
                    {
                        if (e.Aisle != null)
                        {
                            if (e.StorageSpecification == StorageSpecification.HighValue)
                            {
                                int timeEstimate = _simulation.FromHighValueGoodsAisleToDeliveryEstimate;
                                _simulation.AddSimulationTimeToEmployee (e.Employee, timeEstimate);
                                Console.WriteLine($"Time elapsed for preparing Package with ID: {e.PackageId} for delivery: {timeEstimate}Seconds");
                            }
                            if (e.StorageSpecification == StorageSpecification.ClimateControlled)
                            {
                                int timeEstimate = _simulation.FromClimateControlledAisleToDeliveryEstimate;
                                _simulation.AddSimulationTimeToEmployee(e.Employee, timeEstimate);
                                Console.WriteLine($"Time elapsed for preparing Package with ID: {e.PackageId} for delivery: {timeEstimate}Seconds");
                            }
                            if (e.StorageSpecification == StorageSpecification.SmallItems)
                            {
                                int timeEstimate = _simulation.FromSmallItemsAisleToDeliveryEstimate;
                                _simulation.AddSimulationTimeToEmployee(e.Employee, timeEstimate);
                                Console.WriteLine($"Time elapsed for preparing Package with ID: {e.PackageId} for delivery: {timeEstimate}Seconds");
                            }
                        }
                        else
                        {
                            int timeEstimate = _simulation.FromAisleToDeliveryEstimate;
                            _simulation.AddSimulationTimeToEmployee(e.Employee, timeEstimate);
                            Console.WriteLine($"Time elapsed for preparing Package with ID: {e.PackageId} for delivery: {timeEstimate}Seconds");
                        }
                    }
                }
            }
        }
    }

        internal class AisleAndPackageEventArgs : EventArgs
        {
            internal int AisleId { get; }
            internal Aisle Aisle { get; }
            internal int PackageId { get; }
            internal (int, int) Shelf { get; }
            internal StorageSpecification StorageSpecification { get; }
            internal StatusList Status { get; }
            internal Employee Employee { get; }

            internal AisleAndPackageEventArgs(Aisle aisle, Package package, Employee employee)
            {
                Aisle = aisle;
                AisleId = aisle.GetAisleId;
                PackageId = package.PackageId;
                Shelf = aisle.GetShelf(package);
                StorageSpecification = aisle.CurrentStorageZone.StorageSpecification;
                Employee = employee;
                Status = package.Status;
            }
            internal AisleAndPackageEventArgs(Aisle aisle)
            {
                AisleId = aisle.GetAisleId;
            }
        }
    }
