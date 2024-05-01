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

namespace TechSupport.WARE.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _employeeIDCounter = 0;
        private Dictionary<String, Aisle> _aisleList = new Dictionary<String, Aisle>();
        private Dictionary<String, Employee> _employeeList = new Dictionary<String, Employee>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void RefreshAisleList()
        {
            lstAisle.Items.Clear();
            foreach (KeyValuePair<String, Aisle> aisle in _aisleList)
                lstAisle.Items.Add(aisle.Key);
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
        public void AddAisletoList(String key, Aisle aisle)
        {
            _aisleList.Add(key, aisle);
        }
        public void AddEmployeetoList(String key, Employee employee)
        {
            _employeeList.Add(key, employee);
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
        private void AisleListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = lstAisle.SelectedItem;
            if (item != null)
            {
                Aisle selectedAisle = _aisleList[item.ToString()];

                curAisleName.Content = "Name: " + item.ToString();
                curAisleDim.Content = $"Dim: {selectedAisle.GetHeight}x{selectedAisle.GetLength}x{selectedAisle.GetDepth}mm";
                curAisleWeight.Content = "Max Weight: " + (selectedAisle.GetWeight/1000) + "kg";
            }
        }
    }
}