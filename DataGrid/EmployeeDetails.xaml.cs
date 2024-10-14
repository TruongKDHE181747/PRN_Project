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
using static System.Net.Mime.MediaTypeNames;
using MaterialDesignThemes.Wpf;
using Repositories.Models;
using Services;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    

    public partial class EmployeeDetails : Window
    {
        EmployeeServices employeeServices = new EmployeeServices();
        public Employee selected_employee { get; set; } = null;
        string uri_after_upload_file = "";
        public EmployeeDetails()
        {
            InitializeComponent();
            Load_Image("phuong.jpg");
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
                    MessageBox.Show("Duplicate file name!", "Can not upload", MessageBoxButton.OK, MessageBoxImage.Error);
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
            LoadRole(); 
            LoadJobPosition();  
            LoadDepartment(); 
            LoadEmployeeInfo();
        }

        public void LoadEmployeeInfo()
        {
            if (selected_employee != null)
            {
                txtFirstName.Text = selected_employee.FirstName;    
                txtLastName.Text = selected_employee.LastName;  
                txtAddress.Text = selected_employee.Address;    
                txtEmail.Text = selected_employee.Email;    
                txtPhoneNumber.Text = selected_employee.PhoneNumber;    
                txtAvailableDays.Text = selected_employee.AvailableLeaveDays+"";
                txtTotalLeaveDays.Text = selected_employee.TotalLeaveDays + "";


                dpDob.Text = selected_employee.Dob + "";
                dpStartDate.Text = selected_employee.StartDate + "";
                

                cboRole.SelectedValue = selected_employee.RoleId;
                cboDepartment.SelectedValue = selected_employee.DepartmentId;
                cboJobPosition.SelectedValue = selected_employee.JobPositionId;

                Load_Image(selected_employee.Photo);

                if (selected_employee.Gender==true)
                {
                    rbMale.IsChecked = true;
                }
                else
                {
                    rbFemale.IsChecked = true;
                }
                
            }
        }


        public void LoadRole()
        {
            cboRole.ItemsSource = employeeServices.getEmployees().Select(e => e.Role).Distinct();
            cboRole.DisplayMemberPath = "RoleName";
            cboRole.SelectedValuePath = "RoleId";
        }

        public void LoadJobPosition()
        {
            cboJobPosition.Items.Clear();   
            cboJobPosition.ItemsSource = employeeServices.getEmployees().Select(e => e.JobPosition).Distinct();
            cboJobPosition.DisplayMemberPath = "JobPositionName";
            cboJobPosition.SelectedValuePath = "JobPositionId";
        }


        public void LoadDepartment()
        {
            cboDepartment.Items.Clear();
            cboDepartment.ItemsSource = employeeServices.getEmployees().Select(e => e.Department).Distinct();
            cboDepartment.DisplayMemberPath = "DepartmentName";
            cboDepartment.SelectedValuePath = "DepartmentId";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            selected_employee = null;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = selected_employee;   
            if(uri_after_upload_file.Length > 0)
            {
                employee.Photo = uri_after_upload_file;
            }

            if(txtFirstName.Text.Length == 0 || txtLastName.Text.Length==0 || txtAddress.Text.Length==0 || txtEmail.Text.Length==0
                || txtPhoneNumber.Text.Length==0 || txtAvailableDays.Text.Length==0 || txtTotalLeaveDays.Text.Length == 0)
            {
                MessageBox.Show("Please fill all the Input!", "Fill all input", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                employee.FirstName = txtFirstName.Text;
                employee.LastName = txtLastName.Text;
                employee.Email = txtEmail.Text;
                employee.PhoneNumber = txtPhoneNumber.Text;
                employee.Address = txtAddress.Text;
                employee.TotalLeaveDays = int.Parse(txtTotalLeaveDays.Text);
                employee.AvailableLeaveDays = int.Parse(txtAvailableDays.Text);
                employee.JobPositionId = int.Parse(cboJobPosition.SelectedValue + "");
                employee.DepartmentId = int.Parse(cboDepartment.SelectedValue + "");
                employee.RoleId = int.Parse(cboRole.SelectedValue + "");
                if (rbMale.IsChecked == true)
                {
                    employee.Gender = true;
                }
                else
                {
                    employee.Gender = false;
                }

                employeeServices.UpdateEmployee(employee);
                MessageBox.Show("Update successful!", "Update successful", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEmployeeInfo();
            }

            

        }
    }
}