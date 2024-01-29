using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Pickup
{
    public class Pickup
    {
        private DateTime time;
        private List<List<int>> sizes = new List<List<int>>();
        public Pickup(Delivery delivery)
        {
            foreach (Package i in delivery.DeliveryPackageList)
            {
                List<int> temp = new List<int>();
                temp.Add(i.PackageHeightInMm);
                temp.Add(i.PackageLengthInMm);
                temp.Add(i.PackageDepthInMm);
                this.sizes.Add(temp);
            }
            this.time = delivery.DeliveryPackageList[0].DeliveryTime;
        }
        public List<List<int>> Sizes => sizes;
        public Dictionary<List<int>, int> getSizeAndAmount()
        {
            Dictionary<List<int>, int> amountOfMeasures = new Dictionary<List<int>, int>();
            List<int> lastElement = new List<int>();
            foreach (List<int> i in sizes)
            {
                if (lastElement.SequenceEqual(i))
                {
                    amountOfMeasures[lastElement] += 1;
                }
                else
                {
                    lastElement = new List<int>(i);
                    amountOfMeasures.Add(lastElement, 1);
                }
            }
            return amountOfMeasures;
        }

        public override string ToString()
        {
            StringBuilder pickupDetails = new StringBuilder();
            pickupDetails.AppendLine("Pickup time: " + this.time.ToString());
            pickupDetails.AppendLine("Størrelser og antall:");
            Dictionary<List<int>,int> temp = this.getSizeAndAmount();

            foreach (KeyValuePair<List<int>, int> kvp in temp)
            {
                pickupDetails.AppendLine(kvp.Key[0] + ", " + kvp.Key[1] + ", " + kvp.Key[2] + ", " + " : " + kvp.Value);
            }
            return pickupDetails.ToString();
        }
    }
}
