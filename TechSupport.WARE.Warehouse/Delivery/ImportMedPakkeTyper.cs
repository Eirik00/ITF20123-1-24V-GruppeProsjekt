//For å kommentere alt, bruk Ctrl + K, Ctrl + C
//For å fjerne comments, merk alt i filen og bruk: Ctrl + K, Ctrl + U


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace TechSupport.WARE.Warehouse
//{
//    public class Import : IImport
//    {
//        private List<Package> importPackagesList;

//        public Import()
//        {
//            importPackagesList = new List<Package>();
//        }

//        public void PackageImport(double deliveryTime, List<Package> packages, Contact sender, Contact receiver, string pakketype)
//        {
//            foreach (var package in packages)
//            {
//                package.Sender = sender;
//                package.Receiver = receiver;
//                package.DeliveryTime = DateTime.Today.AddHours(deliveryTime);
//                package.ChangeStatus(StatusList.Delivery);
//                ImportPackagesList.Add(package);

//                // Legger til pakkene i de hyllene vi allerede har med typer
//                switch (pakketype)
//                {
//                    case "cold":
//                        pakkeListNavnHer1.Add(package);
//                        break;
//                    case "dry":
//                        pakkeListNavnHer2.Add(package);
//                        break;
//                    case "wet":
//                        pakkeListNavnHer3.Add(package);
//                        break;
//                    default:
//                        pakkeListNavnHer4.Add(package);
//                        break;
//                }

//                Console.WriteLine($"Vare Mottak registerert for Kl {deliveryTime} av sender {sender.FirstName} {sender.Surname} til {receiver.firstName} {receiver.surname}");
//            }


//            public void DailyPackageImport(double deliveryHour, List<Package> packages, Contact sender, Contact receiver)
//            {
//                foreach (var package in packages)
//                {
//                    package.Sender = sender;
//                    package.Receiver = receiver;
//                    DateTime deliveryTime = DateTime.Today.AddHours(deliveryHour);
//                    package.DeliveryTime = deliveryTime;
//                    package.ChangeStatus(StatusList.Delivery);
//                    ImportPackagesList.Add(package);

//                    // Legger til pakkene i de hyllene vi allerede har med typer
//                    switch (pakketype)
//                    {
//                        case "cold":
//                            pakkeListNavnHer1.Add(package);
//                            break;
//                        case "dry":
//                            pakkeListNavnHer2.Add(package);
//                            break;
//                        case "wet":
//                            pakkeListNavnHer3.Add(package);
//                            break;
//                        default:
//                            pakkeListNavnHer4.Add(package);
//                            break;
//                    }
//                }

//                Console.WriteLine($"Gjentagende Daglig Vare Mottak Registrert for Kl {deliveryHour} fra sender {sender.FirstName} {sender.Surname} til {receiver.firstName} {receiver.surname}");
//            }

//            public void WeeklyPackageImport(DayOfWeek deliveryDay, double deliveryHour, List<Package> packages, Contact sender, Contact receiver)
//            {
//                var nextDeliveryDate = GetNextWeekday(DateTime.Today, deliveryDay).AddHours(deliveryHour);

//                foreach (var package in packages)
//                {
//                    package.Sender = sender;
//                    package.Receiver = receiver;
//                    package.DeliveryTime = nextDeliveryDate;
//                    package.ChangeStatus(StatusList.Delivery);
//                    ImportPackagesList.Add(package);

//                    // Legger til pakkene i de hyllene vi allerede har med typer
//                    switch (pakketype)
//                    {
//                        case "cold":
//                            pakkeListNavnHer1.Add(package);
//                            break;
//                        case "dry":
//                            pakkeListNavnHer2.Add(package);
//                            break;
//                        case "wet":
//                            pakkeListNavnHer3.Add(package);
//                            break;
//                        default:
//                            pakkeListNavnHer4.Add(package);
//                            break;
//                    }
//                }

//                Console.WriteLine($"Gjentagende Ukentlig Vare Mottak Registrert for {deliveryDay} Kl. {deliveryHour} av sender {sender.firstName} {sender.surname} til {receiver.firstName} {receiver.surname}.");
//            }

//            //Denne metoden sikrer at selv om den angitte ukedagen allerede har passert i gjeldende uke så registreres mottal til neste forekomst av den dagen i neste uke.
//            private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
//            {
//                int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
//                return start.AddDays(daysToAdd);
//            }

//        public override string ToString()
//        {
//            StringBuilder importDetails = new StringBuilder();
//            importDetails.AppendLine("Motatte Varer:");

//            foreach (var package in ImportPackagesList)
//            {
//                importDetails.AppendLine(package.ToString());
//                importDetails.AppendLine("Status Change Log:");
//                foreach (var statusChange in package.GetPackageLog().GetEntries())
//                {
//                    importDetails.AppendLine($"  - Previous Status: {statusChange.getPreviousStatus()}, New Status: {statusChange.getNewStatus()}, Time: {statusChange.getDateTime()}");
//                }
//                importDetails.AppendLine();
//            }

//            return importDetails.ToString();
//        }


//        public List<Package> ImportPackagesList => importPackagesList;
//    }
//}