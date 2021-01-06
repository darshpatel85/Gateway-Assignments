using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.Models
{
    //Model class of User
    public class UserModel
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(15,MinimumLength =2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
