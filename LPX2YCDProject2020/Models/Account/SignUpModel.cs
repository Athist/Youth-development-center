using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    [Table("Signup")]
    public class SignUpModel
    {
        
        public int Id  { get; set; }

        [StringLength(13)]
        [RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))", ErrorMessage = "Invalid ID number")]
        [Display(Name = "Identity number")]
        [Required(ErrorMessage = "Identity number is required")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is requred")]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at lest 6 characters long")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-type password")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Cellphone number is requred")]
        [Display(Name = "Cellphone number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateJoined { get; set; }

        [Required(ErrorMessage = "Date of birth is requred")]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

      
    }
}
