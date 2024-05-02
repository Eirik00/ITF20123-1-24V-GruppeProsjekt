using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechSupport.WARE.Warehouse;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TechSupport.WARE.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string saveFolderPath = "";
        public bool notSaved = false;

        private int _employeeIDCounter = 0;
        private int _packageIDCounter = 0;
        private Dictionary<String, Aisle> _aisleList = new Dictionary<String, Aisle>();
        private Dictionary<String, Employee> _employeeList = new Dictionary<String, Employee>();
        private Dictionary<String, Package> _packageList = new Dictionary<String, Package>();
        public MainWindow()
        {
            saveFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "techsupport\\warehouse");
            try
            {
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath + "\\old");
                }
                else
                {
                    //string[] saveFiles = Directory.GetFiles(saveFolderPath); Trenger en løsning på denne
                    //foreach(string file in saveFiles)
                    //{
                    //    switch (System.IO.Path.GetFileName(file.Split(".")[0]))
                    //    {
                    //        case "wsAisle":
                    //            var jsonAisle = File.ReadAllText(file);
                    //            _aisleList = JsonSerializer.Deserialize<Dictionary<String, Aisle>>(jsonAisle, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //            break;
                    //        case "wsEmployee":
                    //            var jsonEmployee = File.ReadAllText(file);
                    //            _employeeList = JsonSerializer.Deserialize<Dictionary<String, Employee>>(jsonEmployee, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //            break;
                    //        case "wsPackage":
                    //            var jsonPackage = File.ReadAllText(file);
                    //            _packageList = JsonSerializer.Deserialize<Dictionary<String, Package>>(jsonPackage, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //            break;
                    //    }
                    //}
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            InitializeComponent();
            comboBoxAisle.SelectionChanged += ComboBoxShelfButton;
        }

        public void WarehouseChanged()
        {
            notSaved = true;
            unsavedChanges.Content = "*There are unsaved changes";
        }

        public void RefreshAisleList()
        {
            lstAisle.Items.Clear();
            foreach (KeyValuePair<String, Aisle> aisle in _aisleList)
                lstAisle.Items.Add(aisle.Key);
        }

        public void RefreshEmployeeList()
        {
            lstEmployees.Items.Clear();
            foreach (KeyValuePair<String, Employee> employee in _employeeList)
                lstEmployees.Items.Add(employee.Key);
        }

        public void RefreshPackageList()
        {
            lstPackages.Items.Clear();
            foreach (KeyValuePair<String, Package> employee in _packageList)
                lstPackages.Items.Add(employee.Key);
        }

        //public void RefreshPackageListInAisle(int AisleId)
        //{
        //    lstPackagesInAisle.Items.Clear();
        //    foreach (KeyValuePair<String, Aisle> aisle in _aisleList)
        //        if (aisle.Value.GetAisleId == AisleId)
        //        {
        //            foreach (Package package in aisle.Value.PackagesInAisle)
        //            {
        //                lstPackagesInAisle.Items.Add(package);
        //            }
        //        }
        //}

        private void AddPackageToAisleButton(object sender, EventArgs e)
        {
            var aisleItem = comboBoxAisle.SelectedItem;
            var packageItem = lstPackages.SelectedItem;
            var employeeItem = comboBoxEmployee.SelectedItem;
            var shelf = (ValueTuple<int, int>)comboBoxShelf.SelectedItem;
            Aisle selectedAisle = _aisleList[aisleItem.ToString()];
            Package package = _packageList[packageItem.ToString()];
            Employee employee = _employeeList[employeeItem.ToString()];
            selectedAisle.AddPackage(package, shelf.Item1, shelf.Item2, employee);
        }

        private void OpenCreateAisleButton(object sender, RoutedEventArgs e)
        {
            CreateAisle createAisleWindow = new CreateAisle(this);
            createAisleWindow.ShowDialog();
        }
        private void OpenCreateEmployeeButton(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployeeWindow = new CreateEmployee(this);
            createEmployeeWindow.ShowDialog();
        }
        private void OpenCreatePackageButton(object sender, RoutedEventArgs e)
        {
            CreatePackage createPackageWindow = new CreatePackage(this);
            createPackageWindow.ShowDialog();
        }
        private void ComboBoxShelfButton(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxAisle.SelectedItem != null)
            {
                var selectedAisle = comboBoxAisle.SelectedItem;
                comboBoxShelf.ItemsSource = _aisleList[selectedAisle.ToString()].GetAvailableSpaces();
            }
        }
        public void AddAisletoList(String key, Aisle aisle)
        {
            _aisleList.Add(key, aisle);
        }
        public void AddEmployeetoList(String key, Employee employee)
        {
            _employeeList.Add(key, employee);
        }
        public void AddPackagetoList(String key, Package package)
        {
            _packageList.Add(key, package);
        }
        public int GetNextAvailableAisleId()
        {
            return _aisleList.Count;
        }
        public int GetNextAvailableEmployeeID()
        {
            _employeeIDCounter += 1;
            return _employeeIDCounter;
        }
        public int GetNextAvailablePackageID()
        {
            _packageIDCounter += 1;
            return _packageIDCounter;
        }
        private void AisleSelected()
        {
            lstPackagesInAisle.Visibility = Visibility.Visible;
            addPackageButton.Visibility = Visibility.Collapsed;
            packageAisleSelection.Visibility = Visibility.Collapsed;
            packageEmployeeSelection.Visibility = Visibility.Collapsed;
            packageShelfSelection.Visibility = Visibility.Collapsed;
            comboBoxAisle.Visibility = Visibility.Collapsed;
            comboBoxEmployee.Visibility = Visibility.Collapsed;
            comboBoxShelf.Visibility = Visibility.Collapsed;
        }
        private void PackageSelected()
        {
            lstPackagesInAisle.Visibility = Visibility.Collapsed;
            addPackageButton.Visibility = Visibility.Visible;
            packageAisleSelection.Visibility = Visibility.Visible;
            packageEmployeeSelection.Visibility = Visibility.Visible;
            packageShelfSelection.Visibility = Visibility.Visible;
            comboBoxAisle.Visibility = Visibility.Visible;
            comboBoxEmployee.Visibility = Visibility.Visible;
            comboBoxShelf.Visibility = Visibility.Visible;
            comboBoxAisle.ItemsSource = null;
            comboBoxEmployee.ItemsSource = null;
            comboBoxAisle.ItemsSource = _aisleList.Keys;
            comboBoxEmployee.ItemsSource = _employeeList.Keys;
        }
        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender == lstAisle)
            {
                var item = lstAisle.SelectedItem;
                if (item != null)
                {
                    Aisle selectedAisle = _aisleList[item.ToString()];
                    infoBoxTitle.Content = "Current Aisle:";
                    curInfoName.Content = "Name: " + item.ToString();
                    curInfoDim.Content = $"Dim: {selectedAisle.HeightOfAisleInCm}x{selectedAisle.LengthOfAisleInCm}x{selectedAisle.DepthOfAisleInCm}cm";
                    curInfoWeight.Content = "Max Weight: " + selectedAisle.TotalWeightLimitInKg + "kg";
                    infoBox2Title.Content = "Packages in Aisle:";
                    AisleSelected();
                }
                lstEmployees.SelectedItem = null;
                lstPackages.SelectedItem = null;
            }
            else if(sender == lstEmployees)
            {
                var item = lstEmployees.SelectedItem;
                if(item != null)
                {
                    Employee employee = _employeeList[item.ToString()];
                    infoBoxTitle.Content = "Current Employee:";
                    curInfoName.Content = $"Name: {employee.FirstName} {employee.Surname}";
                    curInfoDim.Content = "Phone Number: " + employee.PhoneNumber;
                    curInfoPackages.Content = "Email: " + employee.Email;
                    curInfoWeight.Content = "Access Level: " + employee.AccessLevel;
                }
            }else if (sender == lstPackages)
            {
                var item = lstPackages.SelectedItem;
                if (item != null)
                {
                    Package package = _packageList[item.ToString()];
                    infoBoxTitle.Content = "Current Package:";
                    curInfoName.Content = $"ID: {package.PackageId}";
                    curInfoDim.Content = $"Dim: {package.PackageLengthInCm}x{package.PackageHeightInCm}x{package.PackageDepthInCm}";
                    curInfoWeight.Content = "Weight: " + package.PackageWeightInGrams;
                    infoBox2Title.Content = "Packages Options:";
                    PackageSelected();
                }
                lstAisle.SelectedItem = null;
                lstEmployees.SelectedItem = null;
            }
        }
        private void SaveWarehouse(object sender, RoutedEventArgs e)
        {
            string[] fileNames = Directory.GetFiles(saveFolderPath);
            foreach (string fileName in fileNames)
            {
                File.Move(fileName, saveFolderPath + "\\old\\" + System.IO.Path.GetFileName(fileName));
            }
            //Aisle
            using (StreamWriter writer = new StreamWriter(saveFolderPath + "\\wsAisle." + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt"))
            {
                writer.WriteLine(JsonSerializer.Serialize(_aisleList));
            }
            using (StreamWriter writer = new StreamWriter(saveFolderPath + "\\wsPackage." + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt"))
            {
                writer.WriteLine(JsonSerializer.Serialize(_packageList));
            }
            using (StreamWriter writer = new StreamWriter(saveFolderPath + "\\wsEmployee." + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt"))
            {
                writer.WriteLine(JsonSerializer.Serialize(_employeeList));
            }
            unsavedChanges.Content = "";
        }
    }
}