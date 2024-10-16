using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentServices
    {
        Prn212Context context = new Prn212Context();    
        public List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
    }
}
