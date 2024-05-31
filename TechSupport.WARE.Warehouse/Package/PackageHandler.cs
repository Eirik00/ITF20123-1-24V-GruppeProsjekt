using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Initializes a new instance of the PackageHandler class and subscribes event handlers to the specified aisle.
    /// 
    /// It has overloading for Simulation.
    /// </summary>
    public class PackageHandler
    {
        private Simulation _simulation;

        /// <summary>
        /// Initializes a new instance of the PackageHandler class and subscribes event handlers to the specified aisle.
        /// 
        /// It has overloading for Simulation.
        /// </summary>
        /// <param name="package">The package to manage.</param>
        public PackageHandler(Package package)
        {
            package.PackageStatusChangedEvent += HandlePackageStatusChanged;
            package.PackageSenderChangedEvent += HandlePackageSenderChanged;
            package.PackageReceiverChangedEvent += HandlePackageReceiverChanged;
            HandlePackage(this, new PackageStatusChangedEventArgs(package));
        }

        /// <summary>
        /// Initializes a new instance of the PackageHandler class and subscribes event handlers to the specified aisle.
        /// 
        /// It has overloading for Simulation.
        /// </summary>
        /// <param name="package">The package to manage.</param>
        /// <param name="sim">Simulation object to simulate package to.</param>
        public PackageHandler(Package package, Simulation sim)
        {
            _simulation = sim;
            package.PackageStatusChangedEvent += HandlePackageStatusChanged;
            package.PackageSenderChangedEvent += HandlePackageSenderChanged;
            package.PackageReceiverChangedEvent += HandlePackageReceiverChanged;
            HandlePackage(this, new PackageStatusChangedEventArgs(package));
        }
        internal void HandlePackage(object sender, PackageStatusChangedEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} added to the handler.");
        }
        internal void HandlePackageStatusChanged(object sender, PackageStatusChangedEventArgs e)
        {
            Console.WriteLine($"Package with ID {e.PackageId} had its status changed to: {e.Status}");
        }
        internal void HandlePackageSenderChanged(object sender, PackageStatusChangedEventArgs e)
        {
            if(e.OldContact != null) 
            {
                Console.WriteLine($"Package with ID {e.PackageId} had its Sender changed from: {e.OldContact.FirstName} {e.OldContact.Surname}, to be: {e.NewSender.FirstName} {e.NewSender.Surname}");
            }
            else
            {
                Console.WriteLine($"Package with ID {e.PackageId} got assigned Sender: {e.NewSender.FirstName} {e.NewSender.Surname}");
            }
        }
        internal void HandlePackageReceiverChanged(object sender, PackageStatusChangedEventArgs e)
        {
            if (e.OldContact != null)
            {
                Console.WriteLine($"Package with ID {e.PackageId} had its Reciever changed from: {e.OldContact.FirstName} {e.OldContact.Surname}, to be: {e.NewReceiver.FirstName} {e.NewReceiver.Surname}");
            }
            else
            {
                Console.WriteLine($"Package with ID {e.PackageId} got assigned Reciever: {e.NewReceiver.FirstName} {e.NewReceiver.Surname}");
            }
        }
    }

    internal class PackageStatusChangedEventArgs : EventArgs
    {
        internal int PackageId { get; }
        internal StatusList Status {  get; }
        internal Contact? NewSender { get; }
        internal Contact? NewReceiver { get; }
        internal Contact OldContact { get; }
        internal StorageSpecification StorageSpecification { get; }
        internal Aisle Aisle { get; }
        internal PackageStatusChangedEventArgs(Package package)
        {
            PackageId = package.PackageId;
            Status = package.Status;
            OldContact = package.Sender;
            OldContact = package.Receiver;
        }
        internal PackageStatusChangedEventArgs(Package package, Contact oldContact)
        {
            PackageId = package.PackageId;
            Status = package.Status;
            OldContact = oldContact;
            NewSender = package.Sender;
            NewReceiver = package.Receiver;
            StorageSpecification = package.PackageAisle.CurrentStorageZone.StorageSpecification;
            Aisle = package.PackageAisle;
        }
    }
}
