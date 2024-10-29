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
    /// Interaction logic for SalaryManagement.xaml
    /// </summary>
    public partial class SalaryManagement : Window
    {
        public Employee selected_employee { get; set; } = null;
        public SalaryManagement()
        {
            InitializeComponent();
        }
        EmployeeServices employeeService = new EmployeeServices();
        public void LoadAllEmployeeSalary()
        {
            SalaryDataGrid.ItemsSource = employeeService.GetAllEmployeeSalary().Where(e => e.EmployeeId == selected_employee.EmployeeId);
        }
        
        
    }
}
