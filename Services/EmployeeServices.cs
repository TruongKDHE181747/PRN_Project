using Microsoft.EntityFrameworkCore;
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


        public List<Employee> getEmployees()
        {
           return context.Employees
                .Include(e => e.JobPosition)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToList();
        }

        public int getTotalEmployee()
        {
            return context.Employees.Count();
        }

        public void UpdateEmployee(Employee employee)
        {
            var e = context.Employees.Find(employee.EmployeeId);
            e.FirstName = employee.FirstName;
            e.LastName = employee.LastName;
            e.JobPositionId = employee.JobPositionId;   
            e.Email = employee.Email;   
            e.DepartmentId = employee.DepartmentId; 
            e.RoleId = employee.RoleId;
            e.PhoneNumber = employee.PhoneNumber;   
            e.Address = employee.Address;   
            e.AvailableLeaveDays = employee.AvailableLeaveDays;
            e.TotalLeaveDays = employee.TotalLeaveDays; 
            e.Status = employee.Status; 
            e.Dob = employee.Dob;   
            e.Gender = employee.Gender; 
            e.Photo = employee.Photo;   
            //context.Employees.Update(e);
            context.SaveChanges();  

        }
    }
}
