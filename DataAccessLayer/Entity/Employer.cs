using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Employer:BaseEntity
    {
        
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       
        public Guid UserId { get; set; }
        
        
        



    }
}
