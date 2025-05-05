using DataAccessLayer.Entity;
using DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobApplication :BaseEntity
    {
        
        public Guid JobId { get; set; }
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
        public string Resume { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
       public AppStatus Status { get; set; }



    }
}


