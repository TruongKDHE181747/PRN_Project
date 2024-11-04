using Microsoft.Win32;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using MaterialDesignThemes.Wpf;
using Repositories.Models;
using Services;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    

    public partial class EmployeeAddWindow : Window
    {
        EmployeeServices employeeServices = new EmployeeServices();
        DepartmentServices departmentServices = new DepartmentServices();
        JobpositionServices jobpositionServices = new JobpositionServices();
        RoleServices roleServices = new RoleServices();
        StatusServices statusServices = new StatusServices();
        ActivityHistoryServices activityHistoryServices = new ActivityHistoryServices();

        public Employee selected_employee { get; set; } = null;
        string uri_after_upload_file = "user1.jpg";
        public EmployeeAddWindow()
        {
            InitializeComponent();
           
        }

        private void txtTotalLeaveDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
            
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            uri_after_upload_file = UploadImages();
            //txtFirstName.Text = image_uri;  
        }


        public String UploadImages()
        {
            String saveInSQlPath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmg;*.jpg;*.png";

            String fullPath = Path.GetFullPath("Images");  //Lấy ra absolute path của folder Image trong EmployeeWPF project

            //Tách chuỗi để lấy đúng tên nơi lưu trữ: bỏ đoạn "bin\Debug\net8.0-windows\" - 24 kí tự, trong fullpath
            int lastIndex = 0;
            for (int i = 0; i < fullPath.Length; i++)
            {
                if (fullPath[i] == '\\')
                {
                    lastIndex = i;
                }
            }
            int startSubStringIndex = lastIndex - 24;
            String filePath = fullPath.Substring(0, startSubStringIndex) + "Images";

            if (openFileDialog.ShowDialog() == true)
            {
                string images_uri = openFileDialog.FileName;
                ibImage.ImageSource = new BitmapImage(new Uri(images_uri));


                string source = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(source);
                String destination = filePath + "\\" + Path.GetFileName(source);
                try
                {
                    fileInfo.CopyTo(destination);  //Copy file vào nơi lưu trữ

                }
                catch
                {
                    //Phòng trường hợp trùng tên file
                    saveInSQlPath = Path.GetFileName(source);
                }
                //C:\Users\Dell\source\repos\PRN_Project\EmployeeWPF\EImages\

                //Tách chuỗi Images để lưu trong database
                saveInSQlPath = Path.GetFileName(source);

            }
            return saveInSQlPath;
        }



        public void Load_Image(String uri)
        {
            String fullPath = Path.GetFullPath("Images");
            int lastIndex = 0;
            for (int i = 0; i < fullPath.Length; i++)
            {
                if (fullPath[i] == '\\')
                {
                    lastIndex = i;
                }
            }
            int startSubStringIndex = lastIndex - 24;
            String filePath = fullPath.Substring(0, startSubStringIndex) + "Images";
            String fileName = filePath + "\\" + uri; //Lấy absolute path của Image để ko phải thay đổi Build Action => Resource
            ibImage.ImageSource = new BitmapImage(new Uri(fileName));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txtTotalLeaveDays.Text = "20";
            txtAvailableDays.Text = "20";
            Load_Image("user1.jpg");
            LoadStatus();
            LoadRole();
            LoadJobPosition();
            LoadDepartment();
            

        }


        public void LoadRole()
        {
            cboRole.ItemsSource = roleServices.GetRoles();
            cboRole.DisplayMemberPath = "RoleName";
            cboRole.SelectedValuePath = "RoleId";
            cboRole.SelectedIndex = 0;  
        }

        public void LoadStatus()
        {
            cboStatus.Items.Clear();
            cboStatus.ItemsSource = statusServices.GetStatuses();
            cboStatus.DisplayMemberPath = "StatusDescription";
            cboStatus.SelectedValuePath = "StatusId";
            cboStatus.SelectedIndex = 0;
        }

        public void LoadJobPosition()
        {
            cboJobPosition.Items.Clear();
            cboJobPosition.ItemsSource = jobpositionServices.GetJobPositions();
            cboJobPosition.DisplayMemberPath = "JobPositionName";
            cboJobPosition.SelectedValuePath = "JobPositionId";
            cboJobPosition.SelectedIndex = 0;
        }


        public void LoadDepartment()
        {
            cboDepartment.Items.Clear();
            cboDepartment.ItemsSource = departmentServices.GetDepartments().Where(d => d.DepartmentId != 6);
            cboDepartment.DisplayMemberPath = "DepartmentName";
            cboDepartment.SelectedValuePath = "DepartmentId";
            cboDepartment.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            selected_employee = null;
            this.Close();
        }

        public bool CheckDuplicate(Employee employee)
        {
            employee = null;
            employee = employeeServices.checkDuplicateUsername(txtUsername.Text);
            if (employee != null)
            {
                MessageBox.Show("This username has already existed!", "Existing username", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            employee = null;
            employee = employeeServices.getEmployeeByEmail(txtEmail.Text);
            if (employee != null)
            {
                MessageBox.Show("This email has already exist!", "Existing email", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            employee = null;
            employee = employeeServices.getEmployeeByPhoneNumber(txtPhoneNumber.Text);
            if (employee != null)
            {
                MessageBox.Show("This phone number has already exist!", "Existing phone number", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        public bool CheckEmailFormat()
        {
            bool result = true;
            string email = txtEmail.Text;
            if (email.Contains("@")==false)
            {
                MessageBox.Show("Email is wrong format!", "Wrong format email", MessageBoxButton.OK, MessageBoxImage.Error);
                result = false; 
            }
            return result;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            Employee employee = new Employee();   

            employee.Photo = uri_after_upload_file;
             

            if (txtUsername.Text.Length==0|| txtPassword.Password.Length==0|| txtFirstName.Text.Length == 0 || txtLastName.Text.Length==0 || txtAddress.Text.Length==0 || txtEmail.Text.Length==0
                || txtPhoneNumber.Text.Length==0 || txtAvailableDays.Text.Length==0 || txtTotalLeaveDays.Text.Length == 0 ||dpDob.Text.Length==0 || dpStartDate.Text.Length==0)
            {
                MessageBox.Show("Please fill all the Input!", "Fill all input", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                if(CheckEmailFormat()==false)
                {
                    return;
                }

                if (CheckDuplicate(employee) == true)
                {
                    return;
                }
                employee.UserName = txtUsername.Text;   
                employee.Password = txtPassword.Password;   
                employee.FirstName = txtFirstName.Text;
                employee.LastName = txtLastName.Text;
                employee.Email = txtEmail.Text;
                employee.PhoneNumber = txtPhoneNumber.Text;
                employee.Address = txtAddress.Text;
                employee.Dob = DateOnly.Parse(dpDob.Text);
                employee.StartDate = DateOnly.Parse(dpStartDate.Text);
                employee.TotalLeaveDays = int.Parse(txtTotalLeaveDays.Text);
                employee.AvailableLeaveDays = int.Parse(txtAvailableDays.Text);
                employee.JobPositionId = int.Parse(cboJobPosition.SelectedValue + "");
                employee.DepartmentId = int.Parse(cboDepartment.SelectedValue + "");
                employee.RoleId = int.Parse(cboRole.SelectedValue + "");
                employee.StatusId = int.Parse(cboStatus.SelectedValue + "");
                employee.IsActive = true;
                if (rbMale.IsChecked == true)
                {
                    employee.Gender = true;
                }
                else
                {
                    employee.Gender = false;
                }

                employeeServices.AddEmployee(employee);

                SaveInHistory();

                MessageBox.Show("Add successful!", "Add successful", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                
            }
        }

        public void SaveInHistory()
        {
            //Luu history
            int maxid = employeeServices.getEmployees().Select(e => e.EmployeeId).Max();
            Employee admin = Application.Current.Properties["admin"] as Employee;
            ActivityHistory activityHistory = new ActivityHistory();
            activityHistory.EmployeeId = admin.EmployeeId;
            activityHistory.Action = "Add";
            activityHistory.Target = "New Employee with id = " + maxid;
            activityHistory.Date = DateOnly.FromDateTime(DateTime.Now);
            activityHistory.Time = TimeOnly.FromDateTime(DateTime.Now);
            activityHistoryServices.addActivityHistory(activityHistory);
        }

        private void txtPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
        }
    }
}