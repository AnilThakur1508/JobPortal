using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
   public class Address: BaseEntity
    {

        public Guid UserId { get; set; }
        public string AddressLine1 { get; set; }  // Street Address
        public string AddressLine2 { get; set; }  // Apartment, Suite, Unit (optional)
        public Guid StateId { get; set; }
        public virtual State State { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
   }
}
