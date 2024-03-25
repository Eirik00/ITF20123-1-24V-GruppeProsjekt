using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    //klasse for pall reoler
    public class PalletRack : IPalletRack
    {
        private const int NumberOfRacks = 3;
        private const int Floors = 8;
        private const int PositionsPerFloor = 11;
        private Dictionary<int, List<Pallet>>[] racks;
        private TruckManager truckManager;

        //ewvents
        public event EventHandler<PalletRackEventArgs> RackUpdated;

        protected virtual void OnRackUpdated(PalletRackEventArgs e)
        {
            RackUpdated?.Invoke(this, e);
        }

        //events end

        public PalletRack(TruckManager truckManager)
        {
            this.truckManager = truckManager;
            InitializeRacks();
        }
        //Setter opp reol strukturen basert på den forhåndsdefinerte konfigurasjonen(konfigs i WARE_ Nye krav delinnlevering 3) PDFen.
        private void InitializeRacks()
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
        // Legger til en pall til et spesifisert reol og etasje.
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
        // Fjerner en pall basert på dens ID fra et spesifisert reol og etasje.
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
        // teller totalt antall paller over alle reoler og etasjer.
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
        // Henter antall paller i en spesifikk reol og etasje.
        public int PalletsInShelf(int rackNumber, int floor)
        {
            if (!IsRackPositionValid(rackNumber, floor)) return 0;
            return racks[rackNumber - 1][floor].Count;
        }

        // Sjekker om et spesifisert reolnummer og etasje er innenfor gyldige områder.
        private bool IsRackPositionValid(int rackNumber, int floor)
        {
            return rackNumber >= 1 && rackNumber <= NumberOfRacks && floor >= 1 && floor <= Floors &&
                   racks[rackNumber - 1][floor].Count < PositionsPerFloor;
        }
        // Beregner tiden det tar å flytte en pall basert på etasjen den er på. Tidene er beskrevet i WARE_Ny krav pdfen
        private int CalculateTimeForFloor(int floor)
        {
            int baseTimeInSeconds = 240;
            return baseTimeInSeconds + (floor - 1) * 30;
        }
        // Finner en pall basert på dens ID over alle reoler og etasjer.
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
