using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;
using TechSupport.WARE.Warehouse.Zone;

namespace TechSupport.WARE.Warehouse
{
    public class Aisle : IAisle
    {
        public Dictionary<(int, int), Package?> shelf;
        private int _numberOfSpaces, _totalWeight, _aisleId, _sections;
        private readonly int _lengthOfSpaceInMm, _heightOfSpaceInMm, _depthOfSpaceInMm, _weightLimitInGrams;
        private bool _accessToBothSides;



        //Events
        public delegate void PackageHandler(object sender, EventArgs e);
        public event EventHandler<Package> NewPackageAddedToShelf;
        public virtual void OnNewPackageAdded(Package e)
        {
            NewPackageAddedToShelf?.Invoke(this, e);
        }

        public Aisle(int amountOfSections, int totalAmountOfSpacesPerSection, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int weightLimitInGrams, StorageSpecification spesification, int aisleId)
        {
            _sections = amountOfSections;
            _numberOfSpaces = totalAmountOfSpacesPerSection * amountOfSections;
            _lengthOfSpaceInMm = lengthOfSpaceInMm;
            _heightOfSpaceInMm = heightOfSpaceInMm;
            _weightLimitInGrams = weightLimitInGrams;
            _spesification = spesification;
            _aisleId = aisleId;
            shelf = [];
            for (int i = 1; i <= amountOfSections; i++)
            {
                for (int j = 1; j <= totalAmountOfSpacesPerSection; j++)
                    shelf.Add((i, j), null);
            }
            if (_accessToBothSides)
            {
                _depthOfSpaceInMm = depthOfSpaceInMm / 2;
            }
            else
                _depthOfSpaceInMm = depthOfSpaceInMm;
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

        public void AddPackage(Package package, (int, int) placement)
        {
            List<(int, int)> available = new(this.GetAvailableSpaces());
            if (this._spesification != package.Specification)
                throw new InvalidOperationException("Current package spesification," +
                    $" StorageSpesification.{package.Specification}," +
                    " is not compatible with Aisle storage spesification," +
                    $" StorageSpesification.{this._spesification}");
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
            OnNewPackageAdded(package);
        }

        public void RemovePackage(Package package)
        {
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
        public StorageSpecification GetStorageSpecification => this._spesification;

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

        public StorageZone currentStorageZone
        {
            get => this.currentStorageZone;
            set => this.currentStorageZone = value;

        }
    }
}
