﻿using System;
using System.Collections.Generic;

namespace TestTwo.Model
{
    public partial class Addresse
    {
        public Addresse()
        {
            Employees = new HashSet<Employee>();
        }

        public int AddressId { get; set; }
        public string AddressText { get; set; }
        public int? TownId { get; set; }

        public virtual Town Town { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
