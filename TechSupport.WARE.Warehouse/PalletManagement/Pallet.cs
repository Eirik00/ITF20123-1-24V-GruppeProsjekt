﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.PalletManagement
{
    //palle klasse som skal holde max 30 pakker
    public class Pallet : IPallet
    {
        public int PalletId { get; private set; }
        private List<Package> packages = new List<Package>();
        public const int MaxCapacity = 30;

        public Pallet(int palletid)
        {
            PalletId = palletid;
        }
        //legger til en enkel pakke i palle så lenge pallen ikke er full
        public bool AddPackage(Package package)
        {
            if (packages.Count >= MaxCapacity) return false;
            packages.Add(package);
            return true;
        }
        //legger til en liste med pakker i palle så lenge den ikke er full
        public bool AddPackageList(PackageList packageList)
        {
            foreach (var package in packageList.Packages)
            {
                if (packages.Count >= MaxCapacity)
                {
                    return false;
                }
                packages.Add(package);
            }
            return true;
        }
        // fjerner pakker fra palle
        public bool RemovePackage(int packageId)
        {
            var package = packages.Find(p => p.PackageId == packageId);
            return package != null && packages.Remove(package);
        }

        public List<Package> Packages => new List<Package>(packages);
        public int PackageCount => packages.Count;
    }
}
