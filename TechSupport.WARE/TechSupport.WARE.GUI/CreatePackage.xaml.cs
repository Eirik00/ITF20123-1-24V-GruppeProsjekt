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
    /// Interaction logic for CreatePackage.xaml
    /// </summary>
    public partial class CreatePackage : Window
    {
        private MainWindow _mainWindow;
        public CreatePackage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void ForceTextToNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regx = new Regex("[^0-9]+");
            e.Handled = regx.IsMatch(e.Text);
        }

        private void createPackageFunction(object sender, RoutedEventArgs e)
        {
            try
            {
                int PackageID = _mainWindow.GetNextAvailablePackageID();
                int packageLengthInCm = Int32.Parse(packageLengthInCmField.Text);
                int packageHeightInCm = Int32.Parse(packageHeightInCmField.Text);
                int packageDepthInCm = Int32.Parse(packageDepthInCmField.Text);
                int packageWeightInGrams = Int32.Parse(packageWeightInGramsField.Text);
                bool packageIsFragile = fragileCheckBox.IsChecked ?? false;
                StorageSpecification specification = (StorageSpecification)((ComboBoxItem)comboBox.SelectedItem).Content;

                Package newPackage = new(PackageID, packageLengthInCm, packageHeightInCm, packageDepthInCm, packageWeightInGrams, packageIsFragile, specification);
                _mainWindow.AddPackagetoList(PackageID.ToString(), newPackage);
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendEventToMain(object sender, EventArgs e)
        {
            _mainWindow.RefreshPackageList();
        }
    }
}
