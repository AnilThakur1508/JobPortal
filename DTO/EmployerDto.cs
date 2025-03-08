using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EmployerDto
    {
        public EmployerDto()
        {
            Address = new AddressDto();
        }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       
        public Guid UserId { get; set; }
        public AddressDto Address { get; set; }




    }
}
