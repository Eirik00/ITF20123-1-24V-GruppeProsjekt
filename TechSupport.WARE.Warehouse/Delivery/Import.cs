using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Import : IImport
    {
        private List<Package> importPackagesList;

        public Import()
        {
            importPackagesList = new List<Package>();
        }

        public void PackageImport(DateTime deliveryTime, List<Package> packages, Contact sender)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Vare Mottak registerert for Kl {deliveryTime} av sender {sender.FirstName} {sender.Surname} til Varehuset.");
        }


        public void DailyPackageImport(int deliveryHour, List<Package> packages, Contact sender)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                DateTime deliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Daglig Vare Mottak Registrert for Kl {deliveryHour} fra sender {sender.FirstName} {sender.Surname} til Varehuset");
        }

        public void WeeklyPackageImport(DayOfWeek deliveryDay, int deliveryHour, List<Package> packages, Contact sender)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

            foreach (var package in packages)
            {
                package.Sender = sender;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                ImportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Ukentlig Vare Mottak Registrert for {deliveryDay} Kl. {deliveryHour} av sender {sender.FirstName} {sender.Surname} til Varehuset.");
        }


        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override string ToString()
        {
            StringBuilder importDetails = new StringBuilder();
            importDetails.AppendLine("Motatte Varer:");

            foreach (var package in ImportPackagesList)
            {
                importDetails.AppendLine(package.ToString());
                importDetails.AppendLine("Status Change Log:");
                foreach (var statusChange in package.GetPackageLog())
                {
                    importDetails.AppendLine($"  - Previous Status: {statusChange.getPreviousStatus()}, New Status: {statusChange.getNewStatus()}, Time: {statusChange.getDateTime()}");
                }
                importDetails.AppendLine();
            }

            return importDetails.ToString();
        }


        public List<Package> ImportPackagesList => importPackagesList;
    }
}