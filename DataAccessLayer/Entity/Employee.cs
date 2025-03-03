using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Employee:BaseEntity
    {
        
        public DateOnly DOB { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string ProfilePicture { get; set; }
        

    }
}
