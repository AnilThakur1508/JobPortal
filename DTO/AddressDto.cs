using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddressDto
    {
        
        public string AddressLine1 { get; set; }  
        public string AddressLine2 { get; set; }  
        public string StateName { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public Guid UserId { get; set; }
        
        

    }
}
