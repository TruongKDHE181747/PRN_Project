using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeServices
    {
        Prn212Context context = new Prn212Context();    
        public Employee? getEmployeeByUserName(string username)
        {
            Employee employee = context.Employees.FirstOrDefault(x => x.UserName.ToLower().Equals(username.ToLower()));  

            return employee;    
           
        }
    }
}
