
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Entity
{
    public class Qualification:BaseEntity
    {
        
        public Guid EmployeeId { get; set; }
        public string InstitutionName { get; set; }
        public string StudyField { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Score { get; set; }
    }
}
