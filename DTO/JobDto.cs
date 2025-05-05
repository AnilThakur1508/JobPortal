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
        public Guid ExperienceLevelId { get; set; }
        public decimal Salary { get; set; }
        public Guid EmployerId { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid JobTypeId { get; set; }
        public Guid CategoryId { get; set; }
        public string CourseIds { get; set; }

        [NotMapped]
        public List<Guid> CourseIdList
        {
            get => !string.IsNullOrEmpty(CourseIds)
                   ? CourseIds.Split(',').Where(id => Guid.TryParse(id, out _)).Select(Guid.Parse).ToList()
                   : new List<Guid>();
            set => CourseIds = value != null && value.Any() ? string.Join(",", value) : string.Empty;
        }
        public string? SkillIds { get; set; }

        [NotMapped]
        public List<Guid> SkillIdList
        {
            get => !string.IsNullOrEmpty(SkillIds)
                   ? SkillIds.Split(',').Where(id => Guid.TryParse(id, out _)).Select(Guid.Parse).ToList()
                   : new List<Guid>();
            set => SkillIds = value != null && value.Any() ? string.Join(",", value) : string.Empty;
        }
        public string? CompanyName { get; set; }
        public AddressDto? Address { get; set; }
       
    }
    public class JobResponseDto: JobDto
    {
        public string ExperienceLevelName { get; set; }
        public string JobTypeName { get; set; }
    }
}

