using Repositories.Models;
using Services;
using System.Collections;
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
        DepartmentServices departmentServices = new DepartmentServices();
        JobpositionServices jobpositionServices = new JobpositionServices();
        RoleServices roleServices = new RoleServices();

        public void LoadAllEmployee()
        {
           //employeeDataGrid.Items.Clear();
           employeeDataGrid.ItemsSource = employeeServices.getEmployees();
        }

        public void LoadJobPosition()
        {

            cboJobPosition.ItemsSource = jobpositionServices.GetJobPositions();
            cboJobPosition.DisplayMemberPath = "JobPositionName";
            cboJobPosition.SelectedValuePath = "JobPositionId";
        }


        public void LoadDepartment()
        {

            cboDepartment.ItemsSource = departmentServices.GetDepartments();
            cboDepartment.DisplayMemberPath = "DepartmentName";
            cboDepartment.SelectedValuePath = "DepartmentId";
        }

        public void LoadGender()
        {
            List<string> sList = new List<string>();
            sList.Add("Female");
            sList.Add("Male"); 
            cboGender.ItemsSource = sList;
        }

        public void LoadData()
        {
            txtCountEmployee.Text = employeeServices.getTotalEmployee() + " Employees";
            LoadAllEmployee();
            LoadDepartment();
            LoadJobPosition();
            LoadGender();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //btnAddEmployee.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
            //btnAddEmployee.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("black"));
            LoadData();
        }

        private void btnEmployeeDetail_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeeDataGrid.SelectedItem as Employee;

            EmployeeDetails employeeDetail = new EmployeeDetails();
            employeeDetail.selected_employee = employee;
            employeeDetail.ShowDialog();
            LoadAllEmployee();

        }

        public void FilterEmployee()
        {
            List<Employee> eList = employeeServices.getEmployees();
            string name = txtEmployeeName.Text;
            if (name.Length > 0)
            {

                eList = eList.Where(e => (e.FirstName + " " + e.LastName).ToLower().Contains(name.ToLower())).ToList();
            }

            int departmentId = -1;
            int jobpositionId = -1;
            int gender = -1;

            if (cboDepartment.SelectedIndex > -1)
            {
                departmentId = int.Parse(cboDepartment.SelectedValue + "");
                eList = eList.Where(e => e.DepartmentId == departmentId).ToList();
            }
            if (cboJobPosition.SelectedIndex > -1)
            {
                jobpositionId = int.Parse(cboJobPosition.SelectedValue + "");
                eList = eList.Where(e => e.JobPositionId == jobpositionId).ToList();
            }
            if (cboGender.SelectedIndex > -1)
            {
                gender = cboGender.SelectedIndex;   // index = 1 => Male, Index = 0 => Female
                if (gender == 1)
                {
                    eList = eList.Where(e => e.Gender == true).ToList();
                }
                if (gender == 0)
                {
                    eList = eList.Where(e => e.Gender == false).ToList();
                }
            }
            // employeeDataGrid.Items.Clear(); 
            employeeDataGrid.ItemsSource = eList;
        }

        private void txtEmployeeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployee();
        }

        private void cboDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployee();
        }

        private void cboJobPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployee();
        }

        private void cboGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployee();
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cboDepartment.SelectedIndex = -1;
            cboJobPosition.SelectedIndex = -1;  
            cboGender.SelectedIndex = -1;
            txtEmployeeName.Text = "";
            LoadAllEmployee();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddWindow employeeAddWindow = new EmployeeAddWindow();
            employeeAddWindow.ShowDialog();
            LoadData(); 
            
        }

        private void btnHideEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to change Status of this Employee!", "Change Status", MessageBoxButton.YesNo, MessageBoxImage.Question); 
            if(result == MessageBoxResult.Yes)
            {
                
                Employee employee = employeeDataGrid.SelectedItem as Employee;
                employee.StatusId = 2;
                employeeServices.UpdateEmployee(employee);
                MessageBox.Show("Change successful!", "Change Status", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(); 
            }

        }
    }

 

}

