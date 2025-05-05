using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobApplicationDto
    {
        public Guid JobId { get; set; }
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }
        public IFormFile Resume { get; set; }

        public DateTime AppliedOn { get; set; }
       
    }

    public class JobApplicationResponseDto : JobApplicationDto
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public string? Title { get; set; }

        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }

        public string Resume { get; set; }

        public DateTime AppliedOn { get; set; }

        public string Status { get; set; }

        

    }
}
