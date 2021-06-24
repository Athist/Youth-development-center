using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is requred")]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateJoined { get; set; }


        [Required(ErrorMessage = "Date of birth is requred")]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(13)]
        [RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))", ErrorMessage = "Invalid ID number")]
        [Display(Name = "Identity number")]
        [Required(ErrorMessage = "Identity number is required")]
        public string IDNumber { get; set; }

    }
}
