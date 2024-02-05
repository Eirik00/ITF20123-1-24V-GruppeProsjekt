using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;
using TechSupport.WARE.Warehouse.Pickup;

namespace Simulation
{
    internal class UseOfAPI
    {
        static void Main(string[] args)
        {
            Contact kjell = new Contact("Kjell", "Ware", "Kjell.Ware@warhouse.dk", "Danmark", "Warehouseveien 8", 80808080, 1890);
            Company warehouse = new Company("Warehouse", "Support@warhouse.dk", "Danmark", "Warehouseveien 8", 80909090, 1890);
            warehouse.AddContact(kjell);

            Contact svein = new Contact("Svein", "Bryggerson", "Svein.Bryggerson@ringnes.no", "Norge", "Ringnesveien 3", 90808020, 3270);
            Company ringnes = new Company("Ringnes", "Support@ringnes.no", "Norge", "Ringnesveien 3", 90707020, 3270);
            Ringnes.AddContact(svein);

            //Parameter: Id, lengde, høyde, dybde, vekt, fragile boolean, kategori for kjølelage, tørrvare eller farlig gods og statuslist update
            Package ol = new Package(1, 250, 250, 400, 1000, true, SpesificationList.Cold, StatusList.Reception);
            Package cider = new Package(2, 250, 250, 400, 10000, true, SpesificationList.Cold, StatusList.Reception);

            PackageList drikkevarer = new PackageList(1);
            drikkevarer.addPackage(ol);
            drikkevarer.addPackage(cider);

            //plass, lengde, høyde, dybde, vekt, kategori for: kjølelager, tørrvare eller farlig gods og id.
            Isle hylle1 = new Isle(50, 5000, 20000, 10000, 20000, 2, 1);
            hylle1.AddPackage(ol, 2);
            hylle1.AddPackage(cider, 3);

            Company rema1000 = new Company("Rema1000", "Support@rema1000.no", "Norge", "Remaveien 3", 90700020, 3250);
            Contact tore = new Contact("Svein", "Matvaresen", "Svein.Matvaresen@rema1000.no", "Norge", "Ringnesveien 3", 90008020, 3270);

            //Sending av varer ut av varehuset
            //Sendings tid gjort om fra int til double for å regne med tider som ikke er hele (14.25 for eksempel)
            //Engangs og daglig sending av varer tar in double tid, pakke, sender og mottaker som parmetere.
            //Gjentagende ukentlig tar inn DayOfWeek.DAG som parameter i tilleg for å spesifisere sendings dag.
            Export remaSending = new Export();
            remaSending.PackageExport(16.00, ol, warehouse, rema1000);
            remaSending.RecurringDailyExport(15.00, ol, warehouse, rema1000);
            remaSending.RecurringWeeklyExport(DayOfWeek.Monday, 14.00, cider, warehouse, rema1000);

            //Mottak av varer inn i varehuset
            //mottak tid gjort om til double fra int
            //Engangs mottak og daglig mottak av varer tar in double tid, pakke, sender og mottaker som parametere. Vi kan eventuelt fjerne mottaker parameteren vis det ikke er nødvendig
            //Gjentagende ukentlig tar inn DayOfWeek.DAG som parameter i tilleg for å spesifisere mottak dag.
            Import ringnesLevering = new Import();
            ringnesLevering.PackageImport(16.00, drikkevarer, ringnes, warehouse);
            ringnesLevering.DailyPackageImport(14.00, cider, ringnes, warehouse);
            ringnesLevering.WeeklyPackageImport(DayOfWeek.Tuesday, 14.00, drikkevarer, ringnes, warehouse);

            ol.GetPackageLog();

        }
    }
}
