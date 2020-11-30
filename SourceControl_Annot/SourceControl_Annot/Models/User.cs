using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace SourceControl_Annot.Models
{
    public class User
    {
        //name
        [Required(ErrorMessage = "Your name should contain at most 15 character")]
        [MaxLength(15)]
        public string Name { get; set; }
        
        //credit card
        [Required]
        [DataType(DataType.CreditCard)]
        [DisplayName("Craditcard number")]
        [RegularExpression(@"^4[0-9]{12}(?:[0-9]{3})?$",ErrorMessage ="Enter valid Creditcard Number")]
        public string cred { get; set; }
        /*Email address*/
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage ="Enter valid mail")]
        public string Email { get; set; }



        /*password */
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8,ErrorMessage = "Enter 8 to 15 characters")]
        public string Password { get; set; }
        /*phone number.*/
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Enter valid Phone number")]
        public string Phone { get; set; }
    }
}