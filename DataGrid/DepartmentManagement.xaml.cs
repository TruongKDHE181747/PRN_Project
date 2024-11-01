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
            employeeDataGrid.ItemsSource = _employeeServices.GetAllEmployeeLeaveDay();
        }
        private void LoadDepartmentComboBox()
        {
            var departments = _departmentServices.GetDepartments();
            departmentComboBox.ItemsSource = departments;
            departmentComboBox.DisplayMemberPath = "DepartmentName";
            departmentComboBox.SelectedValuePath = "DepartmentId";
        }
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var newDepartment = new Department
            {
                DepartmentName = "New Department" 
            };

            _departmentServices.AddDepartment(newDepartment);
            LoadDepartments();
        }
        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Department selectedDepartment)
            {
                selectedDepartment.DepartmentName = "Updated Name";
                _departmentServices.UpdateDepartment(selectedDepartment);
                LoadDepartments();
            }
            else
            {
                MessageBox.Show("Please select a department to edit.");
            }
        }
        private void btnChangeDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                if (departmentComboBox.SelectedValue is int newDepartmentId)
                {
                    _departmentServices.AssignEmployeeToDepartment(selectedEmployee.EmployeeId, newDepartmentId);
                    LoadDepartments();
                }
                else
                {
                    MessageBox.Show("Please select a department to assign.", "Change Department", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to change department.", "Change Department", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataGrid.SelectedItem is Department selectedDepartment)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete the department: {selectedDepartment.DepartmentName}?",
                    "Delete Department", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _departmentServices.DeleteDepartment(selectedDepartment.DepartmentId);
                    LoadDepartments();
                }
            }
            else
            {
                MessageBox.Show("Please select a department to delete.", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void btnEmployeeDetail_Click(object sender, RoutedEventArgs e)
        {
            //if (employeeDataGrid.SelectedItem is Employee selectedEmployee)
            //{
            //    EmployeeDetailsWindow detailsWindow = new EmployeeDetailsWindow(selectedEmployee);
            //    detailsWindow.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Please select an employee to view details.", "Employee Details", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
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
