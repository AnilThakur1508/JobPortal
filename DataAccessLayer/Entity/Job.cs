using DataAccessLayer.Entity;
using System.ComponentModel.DataAnnotations.Schema;

public class Job : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

    public Guid EmployeeId { get; set; } // Ensure this property exists

    public DateTime PostedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string JobType { get; set; }
    public virtual Employee Employee { get; set; }
    public Guid SkillId { get; set; }
    public virtual Skills Skills { get; set; }
}