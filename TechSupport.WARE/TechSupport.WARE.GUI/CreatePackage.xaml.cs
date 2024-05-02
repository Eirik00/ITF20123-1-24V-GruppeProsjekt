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

        private void createEmployeeFunction(object sender, RoutedEventArgs e)
        {
            try
            {
                int PackageID = _mainWindow.GetNextAvailableEmployeeID();
                int accessLevel = Int32.Parse(accessLevelField.Text);
                string firstName = firstNameField.Text;
                string surname = surnameField.Text;
                string email = emailField.Text;
                string address = addressField.Text;
                string country = countryField.Text;
                int phoneNumber = Int32.Parse(phoneNumberField.Text);
                int postalCode = Int32.Parse(postalCodeField.Text);

                Employee newEmployee = new(employeeID, accessLevel, firstName, surname, email, address, "test", 9, 9);
                _mainWindow.AddEmployeetoList(firstNameField.Text, newEmployee);
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendEventToMain(object sender, EventArgs e)
        {
            _mainWindow.RefreshEmployeeList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            // Display the content of the selected item in the TextBlock
            if (selectedItem != null)
            {
                selectedItemTextBlock.Text = "Selected Item: " + selectedItem.Content.ToString();
            }
        }
    }
}
