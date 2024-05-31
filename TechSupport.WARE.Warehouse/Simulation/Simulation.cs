using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse

{
    public class Simulation : ISimulation
    {
        private int _totalSimulationTime = 0;
        private Dictionary<int, int> _simulationWorkerTimes = new Dictionary<int, int>();
        private bool _simulate = true;
        private int _addPackageToHighValueGoodsAisleEstimate = 70;
        private int _addPackageToClimateControlledAisleEstimate = 70;
        private int _addPackageToSmallItemsAisleEstimate = 110;
        private int _fromHighValueGoodsAisleToDeliveryEstimate = 210;
        private int _fromClimateControlledAisleToDeliveryEstimate = 210;
        private int _fromSmallItemsAisleToDeliveryEstimate = 110;
        private int _fromAisleToDeliveryEstimate = 55;

        /// <summary>
        /// Initializes a new instance of the Simulation class with Simulation bool set to true.
        /// </summary>
        public Simulation() { }

        public int GetTotalTimeInSeconds()
        {
            int highestTime = 0;
            foreach(KeyValuePair<int, int> entry in _simulationWorkerTimes)
            {
                if(entry.Value > highestTime)
                {
                    highestTime = entry.Value;
                }
            }
            return highestTime + _totalSimulationTime;
        }
        public void PrintAllEmployeesTimesInSeconds()
        {
            foreach(KeyValuePair<int,int> entry in _simulationWorkerTimes)
            {
                Console.WriteLine($"Worker: {entry.Key} : {entry.Value}Seconds");
            }
        }
        public void StopSimulation()
        {
            if (_simulate)
            {
                _simulate = false;
            }
        }
        public void ResumeSimulation()
        {
            if (!_simulate)
            {
                _simulate = true;
            }
        }
        public int AddPackageToHighValueGoodsAisleEstimate
        {
            get => _addPackageToHighValueGoodsAisleEstimate;
            set => _addPackageToHighValueGoodsAisleEstimate |= value;
        }
        public int AddPackageToClimateControlledAisleEstimate
        {
            get => _addPackageToClimateControlledAisleEstimate;
            set => _addPackageToClimateControlledAisleEstimate = value;
        }
        public int AddPackageToSmallItemsAisleEstimate
        {
            get => _addPackageToSmallItemsAisleEstimate;
            set => _addPackageToSmallItemsAisleEstimate = value;
        }
        public int FromHighValueGoodsAisleToDeliveryEstimate
        {
            get => _fromHighValueGoodsAisleToDeliveryEstimate;
            set => _fromHighValueGoodsAisleToDeliveryEstimate = value;
        }
        public int FromClimateControlledAisleToDeliveryEstimate
        {
            get => _fromClimateControlledAisleToDeliveryEstimate;
            set => _fromClimateControlledAisleToDeliveryEstimate = value;
        }
        public int FromSmallItemsAisleToDeliveryEstimate
        {
            get => _fromSmallItemsAisleToDeliveryEstimate;
            set => _fromSmallItemsAisleToDeliveryEstimate = value;
        }
        public int FromAisleToDeliveryEstimate
        {
            get => _fromAisleToDeliveryEstimate;
            set => _fromAisleToDeliveryEstimate = value;
        }
        internal void AddToTotalSimulationTime(int addTime)
        {
            _totalSimulationTime += addTime;
        }
        internal void AddSimulationTimeToEmployee(Employee employee, int time)
        {
            if(!_simulationWorkerTimes.ContainsKey(employee.EmployeeID))
            {
                _simulationWorkerTimes.Add(employee.EmployeeID, time);
            }
            else
                _simulationWorkerTimes[employee.EmployeeID] += time;
        }
        internal bool GetSimulateBool {  get => _simulate; }

        //public void simulateSinglePackageMovement()
        //{
        //    try
        //    {
        //        Package package = new(1, 0, 0, 0, 0, false, StorageSpecification.Invalid);
        //        PackageHandler packageHandler = new PackageHandler(package);

        //        Aisle simulationAisle = new(10, 10, 8000, 5000, 2000, 20000000, StorageSpecification.Invalid, 1);
        //        Employee simEmployee = new(1, 2, "Sim", "Man", "fake@email.com", "SomewhereStreet 20", "North Pole", 11223344, 1234);
        //        Contact customer = new("Incoming", "Man", "Incoming@Package.com", "IDK 3", "Somewhere", 12345678, 2222);

        //        Incoming incomingOrder = new();
        //        IncomingHandler incomingOrderHandler = new(incomingOrder);

        //        TruckManager truckManager = new TruckManager();
        //        Outgoing outgoingOrder = new(truckManager);
        //        OutgoingHandler outgoingOrderHandler = new(outgoingOrder);

        //        simulationAisle.AddPackage(package, (1, 1), simEmployee);

        //        incomingOrder.IncomingPackage(14.30, package, customer, simEmployee);

        //        outgoingOrder.OutgoingPackage(15.30, package, customer, simEmployee);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //}
        //public bool simulateMultiPackageMovement()
        //{
        //    try
        //    {

        //        PackageList packageList = new PackageList();
        //        for (int i = 0; i < 5; i++)
        //        {
        //            packageList.Add(new(i, 0, 0, 0, 0, false, StorageSpecification.Invalid));
        //            PackageHandler packageHandler = new PackageHandler(packageList[i]);
        //        }
        //        Aisle simulationAisle = new(10, 10, 8000, 5000, 2000, 20000000, StorageSpecification.Invalid, 1);
        //        Employee simEmployee = new(1, 2, "Sim", "Man", "fake@email.com", "SomewhereStreet 20", "North Pole", 11223344, 1234);
        //        Contact customer = new("Incoming", "Man", "Incoming@Package.com", "IDK 3", "Somewhere", 12345678, 2222);

        //        Incoming incomingOrder = new();
        //        IncomingHandler incomingOrderHandler = new(incomingOrder);

        //        TruckManager truckManager = new TruckManager();
        //        Outgoing outgoingOrder = new(truckManager);
        //        OutgoingHandler outgoingOrderHandler = new(outgoingOrder);

        //        for (int i = 0; i < packageList.Count; i++)
        //            simulationAisle.AddPackage(packageList[i], (i + 1, 1), simEmployee);


        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //}
    }
}
