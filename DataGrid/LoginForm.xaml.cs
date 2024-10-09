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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        Prn212Context context = new Prn212Context();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            EmployeeServices es = new EmployeeServices();   
            Employee employee = es.getEmployeeByUserName(txtUser.Text);

            if (employee != null && employee.Password.Equals(txtPass.Password))
            {
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.Show();

            }
            else
            {
                MessageBox.Show("Incorrect Username or Password", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
