
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DataAccessLayer.Entity
{
    public class Qualification
    {
        public Guid Id { get; set; }
        public Guid EmployeId { get; set; }
        public string InstitutionName { get; set; }
        public String FieldOfStudy { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Score { get; set; }
    }
}
