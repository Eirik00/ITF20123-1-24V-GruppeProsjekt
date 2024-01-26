using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE
{
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 1: Reception, 2: Storage, 3: In Progrress, 4: Delivery
    /// </example>
    /// </summary>
    public enum StatusList { Reception = 1, Storage = 2, InProgress = 3, Delivery = 4 };

    public class Package : IPackage
    {

        /// <summary>
        ///     Package object.
        /// </summary>
        private int packageId, packageLenghtInMm, packageHeightInMm, packageDepthInMm, packageWeighInGrams;
        private bool isFragile;
        private int storageSpecification;
        private StatusList status;
        private Dictionary<DateTime, StatusList> statusLog;
        private Contact sender;
        private Contact receiver;

        public Package(int packageId, int packageLenghtInMm, int packageHeightInMm, int packageDepthInMm, int packageWeighInMm, bool isFragile, int storageSpecification, Contact sender, Contact receiver, StatusList status = StatusList.Reception)
        {
            this.packageId = packageId;
            this.packageLenghtInMm = packageLenghtInMm;
            this.packageHeightInMm = packageHeightInMm;
            this.packageDepthInMm = packageDepthInMm;
            this.packageWeighInGrams = packageWeighInMm;
            this.isFragile = isFragile;
            this.storageSpecification = storageSpecification;
            this.status = status;
            this.sender = sender;
            this.receiver = receiver;

            this.statusLog = new Dictionary<DateTime, StatusList>();
            statusLog.Add(DateTime.Now, status);
        }


        /// <summary>
        /// Dictionary <c>StatusLog</c> is used to get the history of the package status changes.
        /// <example>
        /// <code>
        ///     Package.StatusLog
        /// </code>
        ///     >>>returns the package status history
        /// </example>
        /// </summary>
        /// 


        //LAG TOSTRING
        public int getPackageId => this.packageId;
        public Dictionary<DateTime, StatusList> StatusLog
        {
            get
            {
                return statusLog;
            }

        }

        /// <summary>
        /// StatusList <c>Status</c> returns the current status of the package
        /// </summary>
        public StatusList Status
        {
            get
            {
                return status;
            }
        }
        /// <summary>
        /// (Isle, String, int) <c>GetLocation</c> returns the current location and equivelent information of that package
        /// </summary>
        /// <returns>Isle <c>isle</c>, String <c>category</c>, int <c>place</c></returns>
        /// 

        public (Isle isle, String category, int place) GetLocation()
        {
            return (null, null, 0);
        }
        public void ChangeStatus(StatusList newStatus)
        {
            statusLog.Add(DateTime.Now, this.status);
            this.status = newStatus;
            this.statusLog.Add(DateTime.Now, this.status);
        }

    }
}
