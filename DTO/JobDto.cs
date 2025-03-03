using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public Guid UserId { get; set; }
    }
}

