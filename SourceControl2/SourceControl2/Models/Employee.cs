using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sample.Models
{
    public class Employee
    {
        public int  Id { get; set; }
        [Required(ErrorMessage ="required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(10,MinimumLength =8)]
        public string Password { get; set; }

    }
}