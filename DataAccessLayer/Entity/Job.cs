
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

        public Guid EmployerId { get; set; }
        [ForeignKey("EmployerId")]
        public  Employer Employer { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid JobTypeId { get; set; }
        public JobType JobType { get; set; }
        public Guid ExperienceLevelId { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
       

    }
}
