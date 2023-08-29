using System.ComponentModel.DataAnnotations;

namespace FirstWebAPI.Validator
{
    public class DateCheckValidator:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,ValidationContext validationContext)
        {
          var date= (DateTime?)value;
            if(date<DateTime.Now)
            {
                return new ValidationResult("Date Must be Greater than or eqal to Current Date");
            }
            return ValidationResult.Success ;
        }
    }
}
