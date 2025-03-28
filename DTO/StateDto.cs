using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace DTO
{
    public class StateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public Guid CountryId { get; set; }
    }
}
