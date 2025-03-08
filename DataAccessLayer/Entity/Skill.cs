using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Skill :BaseEntity
    {
        public  string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
