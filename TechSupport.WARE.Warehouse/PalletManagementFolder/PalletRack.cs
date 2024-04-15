using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    /// <summary>
    /// Represents a pallet rack in the warehouse.
    /// </summary>
    public class PalletRack : IPalletRack
    {
        private const int NumberOfRacks = 3;
        private const int Floors = 8;
        private const int PositionsPerFloor = 11;
        private Dictionary<int, List<Pallet>>[] racks;
        private TruckManager truckManager;

        //events
        public event EventHandler<PalletRackEventArgs> RackUpdated;

        protected virtual void OnRackUpdated(PalletRackEventArgs e)
        {
            RackUpdated?.Invoke(this, e);
        }

        //events end

        /// <summary>
        /// Initializes a new instance of the PalletRack class.
        /// </summary>
        /// <param name="truckManager">The truck manager.</param>
        public PalletRack(TruckManager truckManager)
        {
            this.truckManager = truckManager;
            RacksSetUp();
        }

        /// <summary>
        /// Sets up the rack structure based on the predefined configuration.
        /// </summary>
        private void RacksSetUp()
        {
            racks = new Dictionary<int, List<Pallet>>[NumberOfRacks];
            for (int i = 0; i < NumberOfRacks; i++)
            {
                racks[i] = new Dictionary<int, List<Pallet>>();
                for (int floor = 1; floor <= Floors; floor++)
                {
                    racks[i][floor] = new List<Pallet>();
                }
            }
        }

        /// <summary>
        /// Adds a pallet to the specified rack and floor.
        /// </summary>
        /// <param name="rackNumber">The number of the rack.</param>
        /// <param name="floor">The floor number.</param>
        /// <param name="pallet">The pallet to add.</param>
        /// <returns>True if the pallet was successfully added; otherwise, false.</returns>
        public bool AddPalletToRack(int rackNumber, int floor, IPallet pallet)
        {
            if (!IsRackPositionValid(rackNumber, floor)) return false;

            OnRackUpdated(new PalletRackEventArgs { RackNumber = rackNumber, Floor = floor, Action = "Pallet Added", PalletId = pallet.PalletId });
            
            if (truckManager.UseTruck())
            {
                racks[rackNumber - 1][floor].Add((Pallet)pallet);
                truckManager.ReturnTruck();

                int timeToMovePallet = CalculateTimeForFloor(floor);
                Console.WriteLine($"Palle {pallet.PalletId} lagt til i reol {rackNumber}, etasje {floor}. henting/plassering tid: {timeToMovePallet} sekunder.");

                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a pallet based on its ID from a specified rack and floor.
        /// </summary>
        /// <param name="rackNumber">The number of the rack.</param>
        /// <param name="floor">The floor number.</param>
        /// <param name="palletId">The ID of the pallet to remove.</param>
        /// <returns>True if the pallet was successfully removed; otherwise, false.</returns>
        public bool RemovePallet(int rackNumber, int floor, int palletId)
        {
            if (!IsRackPositionValid(rackNumber, floor)) return false;
            var pallet = racks[rackNumber - 1][floor].FirstOrDefault(p => p.PalletId == palletId);
            if (pallet != null)
            {
                racks[rackNumber - 1][floor].Remove(pallet);
                OnRackUpdated(new PalletRackEventArgs { RackNumber = rackNumber, Floor = floor, Action = "Pallet Removed", PalletId = palletId });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the total count of pallets across all racks and floors.
        /// </summary>
        /// <returns>The total count of pallets.</returns>
        public int TotalPallets()
        {
            int total = 0;
            foreach (var rack in racks)
            {
                foreach (var floor in rack)
                {
                    total += floor.Value.Count;
                }
            }
            return total;
        }

        /// <summary>
        /// Gets the count of pallets in a specific rack and floor.
        /// </summary>
        /// <param name="rackNumber">The rack number.</param>
        /// <param name="floor">The floor number.</param>
        /// <returns>The count of pallets in the specified rack and floor.</returns>.
        public int PalletsInShelf(int rackNumber, int floor)
        {
            if (!IsRackPositionValid(rackNumber, floor)) return 0;
            return racks[rackNumber - 1][floor].Count;
        }

        /// <summary>
        /// Checks if a specified rack number and floor number are within valid ranges.
        /// </summary>
        /// <param name="rackNumber">The rack number.</param>
        /// <param name="floor">The floor number.</param>
        /// <returns>True if the rack position is valid; otherwise, false.</returns>
        private bool IsRackPositionValid(int rackNumber, int floor)
        {
            return rackNumber >= 1 && rackNumber <= NumberOfRacks && floor >= 1 && floor <= Floors &&
                   racks[rackNumber - 1][floor].Count < PositionsPerFloor;
        }

        /// <summary>
        /// Calculates the time it takes to move a pallet based on the floor it is on.
        /// </summary>
        /// <param name="floor">The floor number.</param>
        /// <returns>The time in seconds to move the pallet.</returns>
        private int CalculateTimeForFloor(int floor)
        {
            int baseTimeInSeconds = 240;
            return baseTimeInSeconds + (floor - 1) * 30;
        }

        /// <summary>
        /// Finds a pallet by its ID across all racks and floors.
        /// </summary>
        /// <param name="id">The ID of the pallet to find.</param>
        /// <returns>The found pallet; otherwise, null.</returns>
        public IPallet FindPallet(int palletid)
        {
            foreach (var rack in racks)
            {
                foreach (var floor in rack)
                {
                    var foundPallet = floor.Value.FirstOrDefault(p => p.PalletId == palletid);
                    if (foundPallet != null) return foundPallet;
                }
            }
            return null;
        }

        public static int NextPalletId { get; private set; } = 1;
        public static int GetNextPalletId() => NextPalletId++;

        public int ReadyForShipmentPalletsCount => TotalPallets();
    }
}

public class PalletRackEventArgs : EventArgs
{
    public int RackNumber { get; set; }
    public int Floor { get; set; }
    public string Action { get; set; }
    public int PalletId { get; set; }
}
