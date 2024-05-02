using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.GUI
{
    /// <summary>
    /// Interaction logic for CreateAisle.xaml
    /// </summary>
    public partial class CreateAisle : Window
    {
        private MainWindow _mainWindow;
        public CreateAisle(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void ForceTextToNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regx = new Regex("[^0-9]+");
            e.Handled = regx.IsMatch(e.Text);
        }

        private void createAisleFunction(object sender, RoutedEventArgs e)
        {
            try
            {
                int shelves = Int32.Parse(aisleShevles.Text);
                int places = Int32.Parse(aislePlacesPerShelf.Text);
                int weight = Int32.Parse(aisleWeight.Text);
                int length = Int32.Parse(aisleLegnth.Text);
                int height = Int32.Parse(aisleHeight.Text);
                int depth = Int32.Parse(aisleDepth.Text);
                int aisleId = _mainWindow.GetNextAvailableAisleId();

                Aisle newAisle = new(shelves, places, length, height, depth, weight, aisleId);
                _mainWindow.AddAisletoList(aisleName.Text, newAisle);
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendEventToMain(object sender, EventArgs e)
        {
            _mainWindow.WarehouseChanged();
            _mainWindow.RefreshAisleList();
        }
    }
}
