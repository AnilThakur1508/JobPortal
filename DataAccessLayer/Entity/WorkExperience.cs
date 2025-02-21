using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class WorkExperience
    {
        public Guid Id { get; set; }
        public Guid EmployeId { get; set; }
        public string CompanyName {  get; set; }
        public string JobTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public String Responsibility { get; set; }
        public string Description { get; set; }

    }
}
