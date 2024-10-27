using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LeaveRequestRepository
    {
        Prn212Context context=new Prn212Context();

        public List<LeaveRequest> GetAll()
        {
            using (var context = new Prn212Context())
            {
                return context.LeaveRequests.Include(e=>e.Employee).Include(d=>d.Employee.Department).Include(s=>s.RequestStatus).Include(t=>t.LeaveTypeId).ToList();
            }
        }


    }
}
