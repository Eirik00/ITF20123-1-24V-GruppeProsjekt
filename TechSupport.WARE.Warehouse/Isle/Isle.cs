using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    public class Isle : IIsle
    {
        public Dictionary<int, Package> shelf;
        private int numberOfSpaces;
        private int lengthOfSpaceInMm;
        private int heightOfSpaceInMm;
        private int depthOfSpaceInMm;
        private int weightLimitInGrams;
        private int category;

        private string testingString = "string";

        private Package test = null;

        public Isle(int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int weightLimitInGrams, int category, int isleId)
        {
            this.numberOfSpaces = numberOfSpaces;
            this.lengthOfSpaceInMm = lengthOfSpaceInMm;
            this.heightOfSpaceInMm = heightOfSpaceInMm;
            this.depthOfSpaceInMm = depthOfSpaceInMm;
            this.weightLimitInGrams = weightLimitInGrams;
            this.category = category;
            this.isleId = isleId;
            shelf = new Dictionary<int, Package>();
            for (int i = 1; i <= numberOfSpaces; i++)
            {
                shelf.Add(i, null);
            }

        }

        public int isleId { get; set; }

        public void AddPackage(Package package, int placement)
        {
            package.ChangeStatus(StatusList.Storage);

            shelf[placement] = package;

            // for testing purposes later when simulating
            Console.WriteLine("Package Added");
        }

        public void RemovePackage(Package package)
        {
            for (int i = 1; i <= numberOfSpaces; ++i)
            {
                if (shelf[i] == package)
                {
                    shelf[i] = null;
                }
            }

        }
    }
}
