using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobApplicationDto
    {
        public Guid JobId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime AppliedOn { get; set; }
        public string Status { get; set; }
    }
}
