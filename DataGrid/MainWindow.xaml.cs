using Repositories.Models;
using Services;
using System.Collections.ObjectModel;
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

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        //Close Button Click Event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
        }


        //Load data
        EmployeeServices employeeServices = new EmployeeServices();
        public void LoadAllEmployee()
        {
            
           employeeDataGrid.ItemsSource = employeeServices.getEmployees();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txtCountEmployee.Text = employeeServices.getTotalEmployee()+" Employees";
            LoadAllEmployee();
        }

        private void btnEmployeeDetail_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeeDataGrid.SelectedItem as Employee;

            EmployeeDetails employeeDetail = new EmployeeDetails();
            employeeDetail.selected_employee = employee;
            employeeDetail.ShowDialog();
            LoadAllEmployee();

        }
    }

 

}

