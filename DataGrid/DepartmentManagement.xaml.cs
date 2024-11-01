using Repositories.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for DepartmentManagement.xaml
    /// </summary>
    public partial class DepartmentManagement : Window
    {
        private DepartmentServices _departmentServices = new DepartmentServices();
        private EmployeeServices _employeeServices = new EmployeeServices();
        public DepartmentManagement()
        {
            InitializeComponent();
            LoadDepartments();
            LoadDepartmentComboBox();
        }
        private void LoadDepartments()
        {
            employeeDataGrid.ItemsSource = _employeeServices.getEmployees();
        }
        private void LoadDepartmentComboBox()
        {
            _departmentServices.GetDepartments();
        }
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDepartmentWindow();
            addWindow.ShowDialog();
            LoadDepartments();
        }
        private void btnChangeDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var changeWindow = new ChangeDepartmentWindow(selectedEmployee);
                changeWindow.ShowDialog();
                LoadDepartments();
            }
            else
            {
                MessageBox.Show("Please select an employee to change department.");
            }
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDepartments();
        }
        private void employeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                txtTitle.Text = $"Selected Employee: {selectedEmployee.FirstName} {selectedEmployee.LastName}";
            }
        }
        private void btnHideEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                selectedEmployee.IsActive = false;
                _employeeServices.UpdateEmployee(selectedEmployee);
                LoadDepartments();
            }
            else
            {
                MessageBox.Show("Please select an employee to hide.", "Hide Employee", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();   
        }
    }
}
