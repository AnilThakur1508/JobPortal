using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobFilterDto
    {
        public Guid? CategoryId { get; set; }
        public Guid? JobTypeId { get; set; }
        public Guid? ExperienceLevelId { get; set; }
       
    } 
}
