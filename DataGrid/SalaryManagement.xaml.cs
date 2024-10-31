using Repositories.Models;
using Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DataGrid
{
    public partial class SalaryManagement : Window
    {
        public Employee SelectedEmployee { get; set; } = null;
        private readonly SalaryService _salaryService;

        public SalaryManagement()
        {
            InitializeComponent();
            _salaryService = new SalaryService(new Prn212Context());
            LoadAllEmployeeSalaries();
        }

        private void LoadAllEmployeeSalaries()
        {
            SalaryDataGrid.ItemsSource = SelectedEmployee != null
                ? _salaryService.GetAllEmployeeSalaries().Where(s => s.EmployeeId == SelectedEmployee.EmployeeId).ToList()
                : _salaryService.GetAllEmployeeSalaries().ToList();
        }
        private void SalaryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
            {
                SelectedEmployee = selectedSalary.Employee;
                LoadAllEmployeeSalaries();
            }
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAllEmployeeSalaries();
        }
        private void btnLeaveDay_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                this.DragMove();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }
        }
    }
}