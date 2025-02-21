using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WorkExperienceDto
    {
        public Guid EmployeId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Responsibility { get; set; }
        public string Description { get; set; }
    }
}
