﻿using System;
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
        private StorageSpecification category;

        public Aisle(int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int weightLimitInGrams, StorageSpecification category, int aisleId)
        {
            this.numberOfSpaces = numberOfSpaces;
            this.lengthOfSpaceInMm = lengthOfSpaceInMm;
            this.heightOfSpaceInMm = heightOfSpaceInMm;
            this.depthOfSpaceInMm = depthOfSpaceInMm;
            this.weightLimitInGrams = weightLimitInGrams;
            this.category = category;
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
            if(available.Contains(placement))
            {
            package.AddAisle(this);
            package.ChangeStatus(StatusList.Storage);

            shelf[placement] = package;

            // for testing purposes later when simulating
            Console.WriteLine("Package Added");
            }
            else
            {
                throw new Exception("This shelf space does not exist or is already taken");
            }
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