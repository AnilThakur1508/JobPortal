using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QualificationDto
    {
        public Guid EmployeId { get; set; }
        public string InstitutionName { get; set; }
        public String FieldOfStudy { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Score { get; set; }
    }
}
