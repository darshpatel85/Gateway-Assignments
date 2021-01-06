using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAss.Models
{
    public class Employee
    {
        public int  Id { get; set; }


        [CustomEmailValidator]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",ErrorMessage ="Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "required")]
        [StringLength(10,MinimumLength =5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.Date)]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "must in dd/mm/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")] 
        public string DOB { get; set; }


        [Required(ErrorMessage = "required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{10}",ErrorMessage ="Mobile Number is not valid")]
        public string MNO { get; set; }

        [Required(ErrorMessage = "required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.ImageUrl)]
        [FileExtensions(Extensions = "jpg,jpeg,png",ErrorMessage ="only jpg/jpeg/png images are allowed")]
        public string Image { get; set; }

    }
}