using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.BL.Models
{
    public partial class AUser
    {
        public int Id { get; set; }
        public string? Loginname { get; set; }
        public byte[]? Pwdsha { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public bool? Isactive { get; set; }
    }
}
