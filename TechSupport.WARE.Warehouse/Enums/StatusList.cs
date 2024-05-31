using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// This is an Enum
    /// Enum <c>StatusList</c> represents the status of a package.
    /// <remarks>
    /// The enumerators in this enum declare various statuses:
    /// <list type="bullet">
    ///     <item><description>Invalid (0): Indicates an invalid status.</description></item>
    ///     <item><description>Initialized (1): The package has been initialized.</description></item>
    ///     <item><description>Ordered (2): The package has been ordered.</description></item>
    ///     <item><description>Reception (3): The package is in reception.</description></item>
    ///     <item><description>Storage (4): The package is in storage.</description></item>
    ///     <item><description>InProgress (5): The package is in progress.</description></item>
    ///     <item><description>Delivery (6): The package is out for delivery.</description></item>
    /// </list>
    /// </remarks>
    /// </summary>
    public enum StatusList { Invalid = 0, Initialized = 1, Ordered = 2, Reception = 3, Storage = 4, InProgress = 5, Delivery = 6 };
}
