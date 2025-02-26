
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DataAccessLayer.Entity
{
    public class Qualification:BaseEntity
    {
        
        public Guid EmployeeId { get; set; }
        public string InstitutionName { get; set; }
        public string StudyField { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Score { get; set; }
    }
}
