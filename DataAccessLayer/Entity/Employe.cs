﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Employe
    {
        public Guid Id { get; set; }
        public DateOnly DOB { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
    }
}
