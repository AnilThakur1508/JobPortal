using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class JobQualification : BaseEntity
    {
      
        
           
            public string Name { get; set; }  // Qualification Name (e.g., "Bachelor’s Degree in Computer Science")
           
            public string Level { get; set; }  // Example: "Diploma", "Bachelor's", "Master's", "PhD"
            public string FieldOfStudy { get; set; }  // Example: "Computer Science", "Business Administration"
            

    }
}
