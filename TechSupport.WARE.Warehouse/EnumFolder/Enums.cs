using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 0: Invalid 1: Initialized, 2: Ordered, 3: Reception, 4: Storage, 5: In Progress, 6: Delivery
    /// </example>
    /// </summary>
    public enum StatusList { Invalid = 0, Initialized = 1, Ordered = 2, Reception = 3, Storage = 4, InProgress = 5, Delivery = 6 };
    /// <summary>
    /// enum <c>StatusList</c> is a set of enumerators which wil declare the status of the package.
    /// <example>
    /// 0: Invalid 1: ClimateControlled, 2: SmallItems, 3: HighValue
    /// </example>
    /// </summary>
    public enum StorageSpecification { Invalid = 0, ClimateControlled = 1, SmallItems = 2, HighValue = 3 };
}
