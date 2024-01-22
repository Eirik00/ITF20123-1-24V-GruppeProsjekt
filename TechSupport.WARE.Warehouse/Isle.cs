using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE
{
    public class Isle : IIsle
    {
        private int numberOfSpaces;
        private int lengthOfSpaceInMm;
        private int heightOfSpaceInMm;
        private int depthOfSpaceInMm;
        private int weightLimitInGrams;
        private int category;

        public Isle(int numberOfSpaces, int lengthOfSpaceInMm, int heightOfSpaceInMm, int depthOfSpaceInMm, int weightLimitInGrams, int category, int isleId)
        {
            this.numberOfSpaces = numberOfSpaces;
            this.lengthOfSpaceInMm = lengthOfSpaceInMm;
            this.heightOfSpaceInMm = heightOfSpaceInMm;
            this.depthOfSpaceInMm = depthOfSpaceInMm;
            this.weightLimitInGrams = weightLimitInGrams;
            this.category = category;
            this.isleId = isleId;
        }

        public int isleId { get; set; }

        public void addPackage()
        {
            throw new NotImplementedException();
        }

        public void removePackage()
        {
            throw new NotImplementedException();
        }
    }
}
