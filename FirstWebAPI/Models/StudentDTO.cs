using FirstWebAPI.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FirstWebAPI.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required (ErrorMessage ="Student Name is Required")]
        [MaxLength (50)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Your email format is invalid")]
        public string Email { get; set; }

        //[Range(10,20)]
        //public int Age { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [DateCheckValidator]
        public DateTime RegisterDate { get; set; }  
        //public string Password { get; set; }

        //[Compare(nameof(Password))]
        //public string ComfirmPassword { get; set; }
    }
}
