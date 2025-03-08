
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity 
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime PostedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JobType { get; set; }


    }
}
