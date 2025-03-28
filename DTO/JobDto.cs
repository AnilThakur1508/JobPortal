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
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        
        public decimal Salary { get; set; }
        
        public Guid EmployerId { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JobType { get; set; }
       
       
       
    }
}

