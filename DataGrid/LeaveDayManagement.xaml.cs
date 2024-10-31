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
    /// Interaction logic for LeaveDayManagement.xaml
    /// </summary>
    public partial class LeaveDayManagement : Window
    {
        EmployeeServices employeeServices = new EmployeeServices();
        public LeaveDayManagement()
        {
            InitializeComponent();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnLeaveDayList1_Click(object sender, RoutedEventArgs e)
        {
             LeaveDayListDataGrid.Visibility = Visibility.Visible;
            LeaveDayRequestDataGrid.Visibility = Visibility.Collapsed;
            txtTitle.Text = "Leave Day List";
            btnAccept.Visibility = Visibility.Collapsed;
            btnReject.Visibility = Visibility.Collapsed;
        }

        private void btnLeaveDayRequest_Click(object sender, RoutedEventArgs e)
        {
            LeaveDayListDataGrid.Visibility = Visibility.Collapsed;
            LeaveDayRequestDataGrid.Visibility = Visibility.Visible;
            txtTitle.Text = "Leave Day Request";
            btnAccept.Visibility = Visibility.Visible;
            btnReject.Visibility = Visibility.Visible;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LeaveDayListDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var leaveDayList = employeeServices.GetAllEmployeeLeaveDay();
            LeaveDayListDataGrid.ItemsSource = leaveDayList;
        }

        private void LeaveDayRequestDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnLeaveDay_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
