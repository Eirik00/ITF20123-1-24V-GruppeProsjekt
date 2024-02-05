using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Export : IExport
    {
        // The list to store packages planned for delivery
        // Will possibly change to dictionary later
        private List<Package> exportPackagesList = new List<Package>();

        public Export()
        {
            exportPackagesList = new List<Package>();
        }

        public void PackageExport(TimeSpan deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;

                // Calculate the delivery time by combining today's date with the provided time of day
                DateTime deliveryTime = DateTime.Today + deliveryHour;

                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);
            }

            Console.WriteLine($"Vare Levering registrert for Kl. {deliveryHour} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        //public void RecurringDailyExport(TimeSpan deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        //{
        //    foreach (var package in packages)
        //    {
        //        package.Sender = sender;
        //        package.Receiver = receiver;
        //        package.DeliveryTime = DateTime.Today.Add(deliveryHour);
        //        package.ChangeStatus(StatusList.Delivery);
        //        exportPackagesList.Add(package);
        //    }
        //    Console.WriteLine($"Gjentagende Daglig Vare Levering registrert for {deliveryHour} av Sender {sender.firstName + "\n" + sender.surname} til {receiver.firstName + "\n" + receiver.surname}.");
        //}

        public void RecurringDailyExport(int deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Daglig Export Registrert for Kl. {deliveryHour}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        //public void RecurringWeeklyExport(DayOfWeek[] deliveryDays, TimeSpan deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        //{
        //    foreach (var deliveryDay in deliveryDays)
        //    {
        //        var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).Add(deliveryHour);
        //        foreach (var package in packages)
        //        {
        //            package.Sender = sender;
        //            package.Receiver = receiver;
        //            package.DeliveryTime = nextDeliveryDate;
        //            package.ChangeStatus(StatusList.Delivery);
        //            exportPackagesList.Add(package);
        //        }
        ///        Console.WriteLine($"Gjentagende Ukentlig Levering registrert for {deliveryDay} på {deliveryTime} av Sender {sender.firstName + "\n" + sender.surname} til {receiver.firstName + "\n" + receiver.surname}.");
        //    }
        //}

        public void RecurringWeeklyExport(DayOfWeek deliveryDay, int deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Ukentlig Vare Export Registrert for {deliveryDay} Kl {deliveryHour}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }



        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override string ToString()
        {
            StringBuilder exportDetails = new StringBuilder();
            exportDetails.AppendLine("Leverse Varer:");

            foreach (var package in ExportPackagesList)
            {
                exportDetails.AppendLine(package.ToString());
                exportDetails.AppendLine("Status Change Log:");
                foreach (var statusChange in package.GetPackageLog())
                {
                    exportDetails.AppendLine($"  - Previous Status: {statusChange.getPreviousStatus()}, New Status: {statusChange.getNewStatus()}, Time: {statusChange.getDateTime()}");
                }
                exportDetails.AppendLine();
            }

            return exportDetails.ToString();
        }


        public List<Package> ExportPackagesList => exportPackagesList;
    }
}