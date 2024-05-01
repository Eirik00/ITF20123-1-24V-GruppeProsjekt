using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public interface ISimulation
    {
        int GetTotalTimeInSeconds();
        void PrintAllEmployeesTimesInSeconds();
        void StopSimulation();
        void ResumeSimulation();
    }
}
