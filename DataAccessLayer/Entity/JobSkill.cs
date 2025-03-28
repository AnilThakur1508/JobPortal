using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobSkill:BaseEntity
    {
        public Guid JobId { get; set; }
        public Guid SkillId { get; set; }
    }
}
