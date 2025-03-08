using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class State :BaseEntity
    {
       
        
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public virtual Country Country { get; set; }

    }
}
