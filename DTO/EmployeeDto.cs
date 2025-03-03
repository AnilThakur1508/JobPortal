using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            Address = new AddressDto();
        }

        public Guid UserId { get; set; }
        public DateOnly DOB { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        
        public IFormFile ProfilePicture { get; set; }
        


    }
}
