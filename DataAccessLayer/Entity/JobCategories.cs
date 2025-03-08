using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobCategories:BaseEntity
    {
        public Guid JobId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SkillId { get; set; }
    }
}
