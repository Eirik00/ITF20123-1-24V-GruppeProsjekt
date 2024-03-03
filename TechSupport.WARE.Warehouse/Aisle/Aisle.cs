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
        public Dictionary<int, Package?> shelf;
        private int aisleId;
        private int numberOfSpaces;
        private readonly int lengthOfSpaceInMm;
        private readonly int heightOfSpaceInMm;
        private readonly int depthOfSpaceInMm;
        private readonly int weightLimitInGrams;
        private int totalWeight;
        private StorageSpecification spesification;

        public Aisle(int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int weightLimitInGrams, StorageSpecification spesification, int aisleId)
        {
            this.numberOfSpaces = numberOfSpaces;
            this.lengthOfSpaceInMm = lengthOfSpaceInMm;
            this.heightOfSpaceInMm = heightOfSpaceInMm;
            this.depthOfSpaceInMm = depthOfSpaceInMm;
            this.weightLimitInGrams = weightLimitInGrams;
            this.spesification = spesification;
            this.aisleId = aisleId;
            shelf = [];
            for (int i = 1; i <= numberOfSpaces; i++)
            {
                shelf.Add(i, null);
            }

        }

        public int GetShelf(Package package) { 
            foreach(KeyValuePair<int, Package?> entry in this.shelf)
            {
                if (entry.Value == package) {
                    return entry.Key;
                }
            }
            return 0;
        }

        public void AddPackage(Package package, int placement)
        {
            List<int> available = new(this.GetAvailableSpaces());
            if (this.spesification != package.Specification)
                throw new InvalidOperationException("Current package spesification," +
                    $" StorageSpesification.{package.Specification}," +
                    " is not compatible with Aisle storage spesification," +
                    $" StorageSpesification.{this.spesification}");
            if (this.depthOfSpaceInMm < package.PackageDepthInMm)
                throw new NotEnoughSpaceException($"Package depth({package.PackageDepthInMm}mm)" +
                    $" is too big for Aisle({this.depthOfSpaceInMm}mm)");
            if (this.totalWeight + package.PackageWeightInGrams > this.weightLimitInGrams)
                throw new WeightLimitException($"Package({package.PackageWeightInGrams}g)" +
                    " is too heavy for the " +
                    $"Aisle({this.totalWeight}/{this.weightLimitInGrams}g)");
            if(!available.Contains(placement))
                throw new Exception("This shelf space does not exist or is already taken");
            this.totalWeight += package.PackageWeightInGrams;
            package.AddAisle(this);
            package.ChangeStatus(StatusList.Storage);

            shelf[placement] = package;

            // for testing purposes later when simulating
            Console.WriteLine("Package Added");
        }

        public void RemovePackage(Package package)
        {
            for (int i = 1; i <= numberOfSpaces; i++)
            {
                if (shelf[i] == package)
                {
                    shelf[i] = null;
                }
            }

        }
        public int GetAisleId => this.aisleId;
        public StorageSpecification GetStorageSpecification => this.spesification;

        public int GetPackagePlacement(Package package)
        {
            foreach((int num, Package? shelfPackage) in shelf)
            {
                if(shelfPackage == package)
                {
                    return num;
                }
            }
            return -1;
        }

        public List<int> GetAvailableSpaces()
        {
            Dictionary<int, Package?> tempList = new(shelf);
            List<int> freeSpaces = [];
            foreach (KeyValuePair<int, Package?> check in tempList)
            {
                if (check.Value == null)
                {
                    freeSpaces.Add(check.Key);
                }
            }
            return freeSpaces;
        }
    }
}
