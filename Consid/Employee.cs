using System;
using System.Collections.Generic;

#nullable disable

namespace Consid
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public bool IsCeo { get; set; }
        public bool IsManager { get; set; }
        public int? ManagerId { get; set; }
    }
}
