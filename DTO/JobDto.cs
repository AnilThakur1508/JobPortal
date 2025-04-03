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
        public string? CategoryIds { get; set; }


        // Store course IDs as a comma-separated string
        public string CourseIds { get; set; }


        // Convert comma-separated string into an array
        
        [NotMapped]
        public List<Guid> CourseIdList
        {
            get => !string.IsNullOrEmpty(CourseIds)
                   ? CourseIds.Split(',').Where(id => Guid.TryParse(id, out _)).Select(Guid.Parse).ToList()
                   : new List<Guid>();
            set => CourseIds = value != null && value.Any() ? string.Join(",", value) : string.Empty;
        }
        public string? SkillIds { get; set; }

        // Convert comma-separated string into an array

        [NotMapped]
        public List<Guid> SkillIdList
        {
            get => !string.IsNullOrEmpty(SkillIds)
                   ? SkillIds.Split(',').Where(id => Guid.TryParse(id, out _)).Select(Guid.Parse).ToList()
                   : new List<Guid>();
            set => SkillIds = value != null && value.Any() ? string.Join(",", value) : string.Empty;
        }
    }
}

