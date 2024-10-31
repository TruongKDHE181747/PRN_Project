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
    /// Interaction logic for EmployeeAttendanceInMonth.xaml
    /// </summary>
    public partial class EmployeeAttendanceInMonth : Window
    {
        AttendanceServices attendanceServices = new AttendanceServices();

        public EmployeeAttendanceInMonth()
        {
            InitializeComponent();
        }

        private void AttendanceDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Attendance? selected_item = Application.Current.Properties["SelectedEmployee"] as Attendance;

            // Check if selected_item is not null
            if (selected_item != null)
            {
                // Check if AttendanceDate is not null
                if (selected_item.AttendanceDate.HasValue)
                {
                    AttendanceDataGrid.ItemsSource = attendanceServices.GetAttendanceInMonth(
                        selected_item.EmployeeId ?? 0, // Use the null-coalescing operator to provide a default value if EmployeeId is null
                        selected_item.AttendanceDate.Value.Month,
                        selected_item.AttendanceDate.Value.Year);
                }

            }
            
        }
    }
}
