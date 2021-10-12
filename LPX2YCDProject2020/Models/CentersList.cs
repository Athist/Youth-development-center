using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class CentersList
    {
        [Key]
        public string CenterId { get; set; }


        [Display(Name = "Center Name")]
        public string CenterName { get; set; }

        [Display(Name = "Center Manager")]
        public string CenterManager { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Number of youth")]
        public int YouthNumber { get; set; }

        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Contact number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Office number")]
        public string OfficeNumber { get; set; }

        [Display(Name = "Regional Coordinator Name")]
        public string Name { get; set; }

        [Display(Name = "Regional Coordinator Surname")]
        public string Surname { get; set; }
    }
}
