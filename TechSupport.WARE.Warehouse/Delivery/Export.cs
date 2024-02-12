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
        //Pakker som skal exporteres blir lagt til i denne listen
        private List<Package> exportPackagesList = new List<Package>();

        public Export()
        {
            exportPackagesList = new List<Package>();
        }

        public void PackageExport(double deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);

                /*//Dette skal slette det sendte pakken fra den originale listen den var i, enkelt og greit
                //Eneste negative er at vi må angi listen pakken var i manuelt
                //Jeg fant ut at vi kan bare ligge til alle listene våre og kun listen som inneholder pakken blir forandret 
                pakkeListeNavnHer.Remove(package);
                pakkeListeNavnHer2.Remove(package);
                pakkeListeNavnHer3.Remove(package);
                pakkeListeNavnHer4.Remove(package);*/
            }

            Console.WriteLine($"Vare Levering registrert for Kl. {deliveryHour} av Sender {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        public void RecurringDailyExport(double deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.AddHours(deliveryHour);
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);

                /*//Dette skal slette det sendte pakken fra den originale listen den var i, enkelt og greit
                //Eneste negative er at vi må angi den listen pakkene var i manuelt
                //Jeg fant ut at vi kan bare ligge til alle listene våre, på denne måten kun listen som inneholder pakken blir forandret 
                pakkeListeNavnHer.Remove(package);
                pakkeListeNavnHer2.Remove(package);
                pakkeListeNavnHer3.Remove(package);
                pakkeListeNavnHer4.Remove(package);*/
            }
            Console.WriteLine($"Gjentagende Daglig Export Registrert for Kl. {deliveryHour}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }

        public void RecurringWeeklyExport(DayOfWeek deliveryDay, double deliveryHour, List<Package> packages, Contact sender, Contact receiver)
        {
            var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = nextDeliveryDate;
                package.ChangeStatus(StatusList.Delivery);
                ExportPackagesList.Add(package);

                /*//Dette skal slette det sendte pakken fra den originale listen den var i, enkelt og greit
                //Eneste negative er at vi må angi listen pakken var i manuelt
                //Jeg fant ut at vi kan bare ligge til alle listene våre og kun listen som inneholder pakken blir forandret 
                pakkeListeNavnHer.Remove(package);
                pakkeListeNavnHer2.Remove(package);
                pakkeListeNavnHer3.Remove(package);
                pakkeListeNavnHer4.Remove(package);*/
            }
            Console.WriteLine($"Gjentagende Ukentlig Vare Export Registrert for {deliveryDay} Kl {deliveryHour}:00 av {sender.FirstName} {sender.Surname} til {receiver.FirstName} {receiver.Surname}.");
        }


        //GetNextWeekDay metoden brukes i ukentlig sending,denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres sending den dagen til neste uke.
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
                foreach (var statusChange in package.GetPackageLog().GetEntries())
                {
                    exportDetails.AppendLine($"  - Previous Status: {statusChange.GetPreviousStatus()}, New Status: {statusChange.GetNewStatus()}, Time: {statusChange.GetDateTime()}");
                }
                exportDetails.AppendLine();
            }

            return exportDetails.ToString();
        }


        public List<Package> ExportPackagesList => exportPackagesList;
    }
}