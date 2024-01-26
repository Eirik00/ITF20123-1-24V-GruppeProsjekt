using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IPackage
    {
        //        Isle isle { get; }
        //        Category category { get; }
        //        int Id { get; set; }
        StatusList Status { get; }
        Dictionary<DateTime, StatusList> StatusLog { get; }
        (Isle isle, string category, int place) GetLocation();
        void ChangeStatus(StatusList newStatus);
    }
}
