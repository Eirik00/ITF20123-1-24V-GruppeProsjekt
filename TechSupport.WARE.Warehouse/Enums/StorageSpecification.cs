using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
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
