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
        private List<Package> DeliveryPackagesList;

        public Delivery()
        {
            DeliveryPackagesList = new List<Package>();
        }

        public void PackageDelivery(DateTime deliveryTime, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = deliveryTime;
                package.ChangeStatus(StatusList.Delivery);
                DeliveryPackagesList.Add(package);
            }
            Console.WriteLine($"Vare Levering registrert for {deliveryTime} av Sender {sender.firstName + "\n" + sender.surname} til {receiver.firstName + "\n" + receiver.surname}.");
        }

        public void RecurringDailyPackageDelivery(TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var package in packages)
            {
                package.Sender = sender;
                package.Receiver = receiver;
                package.DeliveryTime = DateTime.Today.Add(deliveryTime);
                package.ChangeStatus(StatusList.Delivery);
                DeliveryPackagesList.Add(package);
            }
            Console.WriteLine($"Gjentagende Daglig Vare Levering registrert for {deliveryTime} av Sender {sender.firstName + "\n" + sender.surname} til {receiver.firstName + "\n" + receiver.surname}.");
        }

        public void RecurringWeeklyPackageDelivery(DayOfWeek[] deliveryDays, TimeSpan deliveryTime, List<Package> packages, Contact sender, Contact receiver)
        {
            foreach (var deliveryDay in deliveryDays)
            {
                var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).Add(deliveryTime);
                foreach (var package in packages)
                {
                    package.Sender = sender;
                    package.Receiver = receiver;
                    package.DeliveryTime = nextDeliveryDate;
                    package.ChangeStatus(StatusList.Delivery);
                    DeliveryPackagesList.Add(package);
                }
                Console.WriteLine($"Gjentagende Ukentlig Levering registrert for {deliveryDay} på {deliveryTime} av Sender {sender.firstName + "\n" + sender.surname} til {receiver.firstName + "\n" + receiver.surname}.");
            }
        }

        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public override String ToString()
        {
            StringBuilder deliveryDetails = new StringBuilder();
            deliveryDetails.AppendLine("Planlagte Leveranser:");

            foreach (var package in DeliveryPackagesList)
            {
                //deliveryDetails.AppendLine($"{package} - Planlagt for levering {package.DeliveryTime} av {package.Sender.firstName} {package.Sender.surname}");
                foreach (var statusChange in package.GetPackageLog())
                {
                    deliveryDetails.AppendLine($"  Status endret fra {statusChange.getPreviousStatus()} til {statusChange.getNewStatus()} på {statusChange.getDateTime()}");
                }
            }

            return deliveryDetails.ToString();
        }

        public List<Package> DeliveryPackageList => DeliveryPackagesList;
    }
}