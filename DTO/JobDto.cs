using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
       
        public decimal Salary { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime PostedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JobType { get; set; }
       
       
       
    }
}

