using System;
using System.Collections.Generic;

namespace WebAPI.BL.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string? Employeecode { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Isactive { get; set; }
    }
}
