using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobCategories:BaseEntity
    {
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }
        public Guid CategoryId { get; set; }
        public  virtual Category Category { get; set; }
        
       
        public Guid SkillId { get; set; }
        public virtual Skills Skills { get; set; }


    }
}
