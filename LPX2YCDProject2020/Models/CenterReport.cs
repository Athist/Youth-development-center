using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class CenterReport
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

        [Display(Name = "Funding Status")]
        public string FundsStatus { get; set; }

        [Display(Name = "Status (Active/Inactive)")]
        public string Status { get; set; }

       
    }
}
