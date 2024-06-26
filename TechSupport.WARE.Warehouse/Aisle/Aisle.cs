using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;
using System.Text.Json.Serialization;

namespace TechSupport.WARE.Warehouse
{
    public class Aisle : IAisle
    {
        // De/ -Serialization for shelf
        public List<KeyValuePair<string, Package?>> ShelfSerialized { get; set; }
        [JsonIgnore]
        public Dictionary<(int, int), Package?> shelf
        {
            get
            {
                var dictionary = new Dictionary<(int, int), Package?>();
                foreach (var item in ShelfSerialized)
                {
                    var keyParts = item.Key.Split(',');
                    var key = (int.Parse(keyParts[0]), int.Parse(keyParts[1]));
                    dictionary.Add(key, item.Value);
                }
                return dictionary;
            }
            set
            {
                var list = new List<KeyValuePair<string, Package?>>();
                foreach(var item in value)
                {
                    var key = $"{item.Key.Item1},{item.Key.Item2}";
                    list.Add(new KeyValuePair<string, Package?>(key, item.Value));
                }
                ShelfSerialized = list;
            }
        }

        private int _numberOfSpaces, _totalWeightOnAisleInGrams, _aisleId, _sections;
        private readonly int _lengthOfSpaceInCm, _heightOfSpaceInCm, _depthOfSpaceInCm, _weightLimitInKg;
        private bool _accessToBothSides;
        private StorageZone _currentStorageZone;
        private List<Package> _packagesInAisle;


        //Events
        internal event EventHandler<AisleAndPackageEventArgs> PackageAddedToAisle;
        internal event EventHandler<AisleAndPackageEventArgs> PackageRemovedFromAisle;
        internal virtual void OnPackageAddedToAisle(AisleAndPackageEventArgs e)
        {
            PackageAddedToAisle?.Invoke(this, e);
        }
        internal virtual void OnPackageRemovedFromAisle(AisleAndPackageEventArgs e)
        {
            PackageRemovedFromAisle?.Invoke(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the Aisle class with specified parameters.
        /// </summary>
        /// <param name="amountOfShelves">Number of shelves in the aisle.</param>
        /// <param name="totalAmountOfSpacesPerShelf">Amount of spaces per shelf.</param>
        /// <param name="lengthOfAisleInCm">Length of each aisle in centimeters.</param>
        /// <param name="heightOfAisleInCm">Height of each aisle in centimeters.</param>
        /// <param name="depthOfAisleInCm">Depth of each aisle in centimeters.</param>
        /// <param name="totalWeightLimitInKg">Total weight limit per aisle in grams.</param>
        /// <param name="aisleId">Unique identifier for the aisle.</param>
        public Aisle(int AmountOfShelves, int TotalAmountOfSpacesPerShelf, int LengthOfAisleInCm, int HeightOfAisleInCm, int DepthOfAisleInCm, int TotalWeightLimitInKg, int AisleId)
        {
            _sections = AmountOfShelves;
            _numberOfSpaces = TotalAmountOfSpacesPerShelf * AmountOfShelves;
            _lengthOfSpaceInCm = LengthOfAisleInCm;
            _heightOfSpaceInCm = HeightOfAisleInCm;
            _weightLimitInKg = TotalWeightLimitInKg;
            _aisleId = AisleId;
            _currentStorageZone = new StorageZone(StorageSpecification.Invalid);
            _packagesInAisle = new List<Package>();
            ShelfSerialized = new List<KeyValuePair<string, Package?>>();
            for (int i = 1; i <= AmountOfShelves; i++)
            {
                for (int j = 1; j <= TotalAmountOfSpacesPerShelf; j++)
                    ShelfSerialized.Add(new KeyValuePair<string, Package?>($"{i},{j}", null));
            }
            if (_accessToBothSides)
            {
                _depthOfSpaceInCm = DepthOfAisleInCm / 2;
            }
            else
                _depthOfSpaceInCm = DepthOfAisleInCm;
        }

        public (int, int) GetShelf(Package package)
        {
            foreach (KeyValuePair<(int, int), Package?> entry in this.shelf)
            {
                if (entry.Value == package)
                {
                    return entry.Key;
                }
            }
            return (0, 0);
        }

        public void ChangeAmountOfShelves(int shelves)
        {
            if (GetAvailableSpaces().Count != _numberOfSpaces * _sections) 
            {
                throw new NotEmptyException($"Aisle: {_aisleId} is not empty!");
            }
            else
            {
                _sections = shelves;
                shelf = [];
                for (int i = 1; i <= _sections; i++)
                {
                    for (int j = 1; j <= _numberOfSpaces; j++)
                        shelf.Add((i, j), null);
                }
            }
        }

        public void ChangeTotalAmountOfSpacesPerShelf(int amountOfSpaces)
        {
            if (GetAvailableSpaces().Count != _numberOfSpaces * _sections)
            {
                throw new NotEmptyException($"Aisle: {_aisleId} is not empty!");
            }
            else
            {
                _numberOfSpaces = amountOfSpaces;
                shelf = [];
                for (int i = 1; i <= _sections; i++)
                {
                    for (int j = 1; j <= _numberOfSpaces; j++)
                        shelf.Add((i, j), null);
                }
            }
        }

        /// <summary>
        /// Adds a package to the storage zone at a specified shelf placement.
        /// </summary>
        /// <param name="package">The package to be added to the storage zone.</param>
        /// <param name="shelfPlacement">The shelf placement where the package will be stored.</param>
        /// <param name="spaceInShelfPlacement">The specific space within the shelf placement where the package will be stored.</param>
        /// <param name="mover">The employee responsible for moving the package.</param>
        /// <exception cref="Exception">Thrown when the mover's access level is insufficient for the storage zone.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the package's specification is not compatible with the storage zone, when the package is too heavy for the aisle, or when the specified shelf space does not exist or is already taken.</exception>
        /// <exception cref="WeightLimitException">Thrown when the package exceeds the weight limit of the aisle.</exception>
        /// <exception cref="NotEnoughSpaceException">Thrown when the package is too large for the specified shelf space.</exception>
        public void AddPackage(Package package, int shelfPlacement, int spaceInShelfPlacement, Employee mover)
        {
            (int, int) placement = (shelfPlacement, spaceInShelfPlacement);
            List<(int, int)> available = new(this.GetAvailableSpaces());
            if (mover.AccessLevel < _currentStorageZone.StorageZoneAccessLevel)
            {
                throw new Exception("Movers access level: " + mover.AccessLevel + ", is not high enough for the storage zone: " + _currentStorageZone.StorageZoneAccessLevel);
            }
            if (_currentStorageZone.StorageSpecification != package.Specification)
                throw new InvalidOperationException("Current package spesification," +
                    $" StorageSpesification.{package.Specification}," +
                    " is not compatible with Aisle storage spesification," +
                    $" StorageSpesification.{_currentStorageZone.StorageSpecification}");
            if ((this._totalWeightOnAisleInGrams + package.PackageWeightInGrams)/1000 > this._weightLimitInKg)
                throw new WeightLimitException($"Package({package.PackageWeightInGrams}g)" +
                    " is too heavy for the " +
                    $"Aisle({this._totalWeightOnAisleInGrams}/{this._weightLimitInKg}g)");
            if (!available.Contains(placement))
                throw new InvalidOperationException("This shelf space does not exist or is already taken");

            /*
            List<int> aisleDimensions = new List<int>{this.depthOfSpaceInCm, this.heightOfSpaceInCm, this.lengthOfSpaceInCm};
            List<int> packageDimensions = new List<int> { package.PackageDepthInCm, package.PackageHeightInCm, package.PackageLengthInCm };
            for(int i = 0; i < aisleDimensions.Count; i++)
            {
                if (aisleDimensions[0] <= packageDimensions[0] && aisleDimensions[1] <= packageDimensions[1] && aisleDimensions[2] <= packageDimensions[2]) ;

            }*/

            int[] aisle_dimensions = { this._depthOfSpaceInCm, this._heightOfSpaceInCm, (this._lengthOfSpaceInCm/this._numberOfSpaces) };
            int[] package_dimensions = { package.PackageDepthInCm, package.PackageHeightInCm, package.PackageLengthInCm };

            Array.Sort(aisle_dimensions);
            Array.Sort(package_dimensions);

            if(package_dimensions[0] > aisle_dimensions[0] || package_dimensions[1] > aisle_dimensions[1] || package_dimensions[2] > aisle_dimensions[2])
            {
                throw new NotEnoughSpaceException("Package is too large for this shelf");
            }

            this._totalWeightOnAisleInGrams += package.PackageWeightInGrams;
            package.AddAisle(this);
            package.ChangeStatus(StatusList.Storage);

            this._packagesInAisle.Add(package);

            shelf[placement] = package;

            // for testing purposes later when simulating
            OnPackageAddedToAisle(new AisleAndPackageEventArgs(this, package, mover));
        }

        public void RemovePackage(Package package, Employee mover)
        {
            if (mover.AccessLevel < _currentStorageZone.StorageZoneAccessLevel)
            {
                throw new Exception("Movers access level: " + mover.AccessLevel + ", is not high enough for the storage zone: " + _currentStorageZone.StorageZoneAccessLevel);
            }
            OnPackageRemovedFromAisle(new AisleAndPackageEventArgs(this, package, mover));
            for (int i = 1; i <= _sections; i++)
            {
                for (int j = 1; j <= _numberOfSpaces/_sections; j++)
                {
                    if (shelf[(i, j)] == package)
                    {
                        shelf[(i, j)] = null;
                    }
                }
            }
            this._packagesInAisle.Remove(package);
            package.AddAisle(null);
        }

        /// <summary>
        /// Returns Aisleid (int)
        /// </summary>
        public int AisleId => this._aisleId;
        /// <summary>
        /// Height in Centimeteres
        /// </summary>
        public int HeightOfAisleInCm => this._heightOfSpaceInCm;
        /// <summary>
        /// Length in Centimeteres
        /// </summary>
        public int LengthOfAisleInCm => this._lengthOfSpaceInCm;
        /// <summary>
        /// Depth in Centimeteres
        /// </summary>
        public int DepthOfAisleInCm => this._depthOfSpaceInCm;
        /// <summary>
        /// Weight in Grams
        /// </summary>
        public int TotalWeightLimitInKg => this._weightLimitInKg;
        /// <summary>
        /// Ammount of shelves
        /// </summary>
        public int AmountOfShelves => _sections;
        /// <summary>
        /// Ammount of spaces on shelf
        /// </summary>
        public int TotalAmountOfSpacesPerShelf => _numberOfSpaces / _sections;


        public (int, int) GetPackagePlacement(Package package)
        {
            foreach (((int, int) num, Package? shelfPackage) in shelf)
            {
                if (shelfPackage == package)
                {
                    return num;
                }
            }
            return (-1, -1);
        }

        public List<(int, int)> GetAvailableSpaces()
        {
            Dictionary<(int, int), Package?> tempList = new(shelf);
            List<(int, int)> freeSpaces = [];
            foreach (KeyValuePair<(int, int), Package?> check in tempList)
            {
                if (check.Value == null)
                {
                    freeSpaces.Add(check.Key);
                }
            }
            return freeSpaces;
        }

        //Possibly redundant when _packagesInAisle has a getter
        public List<Package> getPackagesInAisle()
        {
            return this._packagesInAisle;
        }

        public StorageZone CurrentStorageZone
        {
            get => this._currentStorageZone;
            set => this._currentStorageZone = value;

        }

        public List<Package> PackagesInAisle
        {
            get => this._packagesInAisle;
        }
    }
}
