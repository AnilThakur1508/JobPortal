using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobQualification : BaseEntity
    {
        
        public Guid JobId {  get; set; }
        public Guid CourseId { get; set; }
      
        
           
           

    }
}
