using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.PalletManagement;

namespace TechSupport.WARE.Warehouse

{
    public class Simulation
    {
        internal int _totalSimulationTime = 0;
        internal Dictionary<int, int> _simulationDictionary = new Dictionary<int, int>();
        internal bool _simulate = true;

        public Simulation() { }

        public int GetTotalTimeInSeconds()
        {
            int highestTime = 0;
            foreach(KeyValuePair<int, int> entry in _simulationDictionary)
            {
                if(entry.Value > highestTime)
                {
                    highestTime = entry.Value;
                }
            }
            return highestTime + _totalSimulationTime;
        }
        public void PrintAllEmployeesTimeInSeconds()
        {
            foreach(KeyValuePair<int,int> entry in _simulationDictionary)
            {
                Console.WriteLine($"Worker: {entry.Key} : {entry.Value}Seconds");
            }
        }
        internal void AddToTotalSimulationTime(int addTime)
        {
            _totalSimulationTime += addTime;
        }
        internal void AddSimulationTimeToEmployee(Employee employee, int time)
        {
            if(!_simulationDictionary.ContainsKey(employee.EmployeeID))
            {
                _simulationDictionary.Add(employee.EmployeeID, time);
            }
            else
                _simulationDictionary[employee.EmployeeID] += time;
        }
        internal bool GetSimulateBool {  get => _simulate; }
        public void StopSimulation()
        {
            if (_simulate)
            {
                _simulate = false;
            }
        }
        public void StartSimulation() 
        { 
            if (!_simulate) 
            {
                _simulate = true;
            }
        }

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
