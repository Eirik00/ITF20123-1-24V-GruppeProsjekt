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


    /// <summary>
    /// This is an enum
    /// Enum <c>StorageSpecification</c> represents the storage specifications of a package.
    /// <remarks>
    /// The enumerators in this enum declare various storage specifications:
    /// <list type="bullet">
    ///     <item><description>Invalid (0): Indicates an invalid storage specification.</description></item>
    ///     <item><description>ClimateControlled (1): The package requires climate-controlled storage.</description></item>
    ///     <item><description>SmallItems (2): The package contains small items.</description></item>
    ///     <item><description>HighValue (3): The package contains high-value items.</description></item>
    /// </list>
    /// </remarks>
    /// </summary>
    public enum StorageSpecification { Invalid = 0, ClimateControlled = 1, SmallItems = 2, HighValue = 3 };
}
