using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CategoryCountDto
    {
        public Guid CategoryId { get; set; }  
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
        public string CategoryIcon { get; set; }
    }
}
