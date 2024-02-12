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
        //Packages to be exported are added to this list
        private List<PackageList> exportPackagesList;

        public Export()
        {
            exportPackagesList = [];
        }

        /// <summary>
        /// Export packages with specified delivery information.
        /// </summary>
        /// <param name="deliveryHour">The time of the day the delivery is estimated to arrive to receiver for example 14.00</param>
        /// <param name="packages">packages to export.</param>
        /// <param name="sender">The sender's contact information.</param>
        /// <param name="receiver">The receiver's contact information.</param>
        public void PackageExport(double deliveryHour, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(packages);    
            }
            //This should delete the sent packet from the original list it was in, plain and simple
<<<<<<< HEAD
            //packages.Packages.Clear();
            Console.WriteLine($"Vare Levering registrert for Kl. {deliveryHour} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
=======
            packages.Packages.Clear();
            Console.WriteLine($"Vare Levering registrert for Kl. {deliveryHour} av Sender {sender.firstName} {sender.surname} til {receiver.firstName} {receiver.surname}.");
>>>>>>> 4dee81aa44bae1c33fbdc02598e72c00508bd9a2
        }

        /// <summary>
        /// Registers a recurring daily export of packages.
        /// </summary>
        /// <param name="deliveryHour">The time of the day the delivery is meant to arrive</param>
        /// <param name="packages">The packages to export.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void RecurringDailyExport(double deliveryHour, PackageList packages, Contact sender, Contact receiver)
        {
            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(packages);

                
            }
            Console.WriteLine($"Gjentagende Daglig Export Registrert for Kl. {deliveryHour}:00 av {sender.firstName} {sender.surname} til {receiver.firstName} {receiver.surname}.");
            //This should delete the sent packet from the original list it was in, plain and simple
            packages.Packages.Clear();
        }

        /// <summary>
        /// Rehisters a recurring weekly export of packages.
        /// </summary>
        /// <param name="deliveryDay">The day of the week for the delivery.</param>
        /// <param name="deliveryHour">The hour of delivery.</param>
        /// <param name="packages">The packages to export.</param>
        /// <param name="sender">The contact information of the sender.</param>
        /// <param name="receiver">The contact information of the receiver.</param>
        public void RecurringWeeklyExport(DayOfWeek deliveryDay, double deliveryHour, PackageList packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

            foreach (Package package in packages.Packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(packages);
            }
            //This should delete the sent packet from the original list it was in, plain and simple
            packages.Packages.Clear();
            Console.WriteLine($"Gjentagende Ukentlig Vare Export Registrert for {deliveryDay} Kl {deliveryHour}:00 av {sender.firstName} {sender.surname} til {receiver.firstName} {receiver.surname}.");
        }


        //GetNextWeekDay metoden brukes i ukentlig sending,denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres sending den dagen til neste uke.
        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override string ToString()
        {
            StringBuilder exportDetails = new();
            exportDetails.AppendLine("Leverte Varer:");

            foreach (var packageList in ExportPackagesList)
            {
                foreach (var package in packageList.Packages)
                {
                    exportDetails.AppendLine(package.ToString());
                    exportDetails.AppendLine("Status Change Log:");
                    foreach (var statusChange in package.GetPackageLog().GetEntries())
                    {
                        exportDetails.AppendLine($"  - Previous Status: {statusChange.GetPreviousStatus()}, New Status: {statusChange.GetNewStatus()}, Time: {statusChange.GetDateTime()}");
                    }
                    exportDetails.AppendLine();
                }
            }

            return exportDetails.ToString();
        }


        public List<PackageList> ExportPackagesList => exportPackagesList;
    }
}