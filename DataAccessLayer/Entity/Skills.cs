using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Skills :BaseEntity
    {
        public  string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        
    }
}
