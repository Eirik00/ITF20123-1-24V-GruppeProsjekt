using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    public class Aisle : IAisle
    {
        public Dictionary<(int, int), Package?> shelf;
        private int _numberOfSpaces, _totalWeight, _aisleId, _sections;
        private readonly int _lengthOfSpaceInMm, _heightOfSpaceInMm, _depthOfSpaceInMm, _weightLimitInGrams;
        private bool _accessToBothSides;
        private StorageZone _currentStorageZone;


        //Events
        internal event EventHandler<AisleAndPackageEventArgs> PackageAddedToAisle;
        internal virtual void OnPackageAddedToAisle(AisleAndPackageEventArgs e)
        {
            PackageAddedToAisle?.Invoke(this, e);
        }

        public Aisle(int amountOfShelves, int totalAmountOfSpacesPerShelf, int lengthOfAisleInMm, int heightOfAisleInMm, int depthOfAisleInMm, int totalWeightLimitInGrams, StorageSpecification spesification, int aisleId)
        {
            _sections = amountOfShelves;
            _numberOfSpaces = totalAmountOfSpacesPerShelf * amountOfShelves;
            _lengthOfSpaceInMm = lengthOfAisleInMm;
            _heightOfSpaceInMm = heightOfAisleInMm;
            _weightLimitInGrams = totalWeightLimitInGrams;
            _aisleId = aisleId;
            shelf = [];
            _currentStorageZone = new StorageZone(StorageSpecification.Invalid);
            for (int i = 1; i <= amountOfShelves; i++)
            {
                for (int j = 1; j <= totalAmountOfSpacesPerShelf ; j++)
                    shelf.Add((i,j), null);
            }
            if (_accessToBothSides)
            {
                _depthOfSpaceInMm = depthOfAisleInMm/2;
            }
            else
                _depthOfSpaceInMm = depthOfAisleInMm;
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
                throw new NotEmptyException($"Aisle: {GetAisleId} is not empty!");
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
                throw new NotEmptyException($"Aisle: {GetAisleId} is not empty!");
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

        public void AddPackage(Package package, (int, int) placement, Employee mover)
        {
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
            if (this._totalWeight + package.PackageWeightInGrams > this._weightLimitInGrams)
                throw new WeightLimitException($"Package({package.PackageWeightInGrams}g)" +
                    " is too heavy for the " +
                    $"Aisle({this._totalWeight}/{this._weightLimitInGrams}g)");
            if (!available.Contains(placement))
                throw new InvalidOperationException("This shelf space does not exist or is already taken");

            /*
            List<int> aisleDimensions = new List<int>{this.depthOfSpaceInMm, this.heightOfSpaceInMm, this.lengthOfSpaceInMm};
            List<int> packageDimensions = new List<int> { package.PackageDepthInMm, package.PackageHeightInMm, package.PackageLengthInMm };
            for(int i = 0; i < aisleDimensions.Count; i++)
            {
                if (aisleDimensions[0] <= packageDimensions[0] && aisleDimensions[1] <= packageDimensions[1] && aisleDimensions[2] <= packageDimensions[2]) ;

            }*/

            int[] dimensions = { this._depthOfSpaceInMm, this._heightOfSpaceInMm, this._lengthOfSpaceInMm };

            Array.Sort(dimensions);


            if (package.PackageLengthInMm <= dimensions[0])
            {
                dimensions[0] = 0;
            }
            else if (package.PackageLengthInMm <= dimensions[1])
            {
                dimensions[1] = 0;
            }
            else if (package.PackageLengthInMm <= dimensions[2])
            {
                dimensions[2] = 0;
            }
            else throw new NotEnoughSpaceException("Package is too large for this shelf");

            if (package.PackageHeightInMm <= dimensions[0])
            {
                dimensions[0] = 0;
            }
            else if (package.PackageHeightInMm <= dimensions[1])
            {
                dimensions[1] = 0;
            }
            else if (package.PackageHeightInMm <= dimensions[2])
            {
                dimensions[2] = 0;
            }
            else throw new NotEnoughSpaceException("Package is too large for this shelf");

            if (package.PackageDepthInMm <= dimensions[0])
            {
                dimensions[0] = 0;
            }
            else if (package.PackageDepthInMm <= dimensions[1])
            {
                dimensions[1] = 0;
            }
            else if (package.PackageDepthInMm <= dimensions[2])
            {
                dimensions[2] = 0;
            }
            else throw new NotEnoughSpaceException("Package is too large for this shelf");

            this._totalWeight += package.PackageWeightInGrams;
            package.AddAisle(this);
            package.ChangeStatus(StatusList.Storage);

            shelf[placement] = package;

            // for testing purposes later when simulating
            OnPackageAddedToAisle(new AisleAndPackageEventArgs(this, package));
        }

        public void RemovePackage(Package package, Employee mover)
        {
            if (mover.AccessLevel < _currentStorageZone.StorageZoneAccessLevel)
            {
                throw new Exception("Movers access level: " + mover.AccessLevel + ", is not high enough for the storage zone: " + _currentStorageZone.StorageZoneAccessLevel);
            }
            for (int i = 1; i <= _sections; i++)
            {
                for (int j = 1; j <= _numberOfSpaces; j++)
                {
                    if (shelf[(i, j)] == package)
                    {
                        shelf[(i, j)] = null;
                    }
                }
            }

        }
        public int GetAisleId => this._aisleId;

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

        public StorageZone CurrentStorageZone
        {
            get => this._currentStorageZone;
            set => this._currentStorageZone = value;

        }
    }
}
