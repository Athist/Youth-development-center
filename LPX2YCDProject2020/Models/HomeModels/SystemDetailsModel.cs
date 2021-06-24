using LPX2YCDProject2020.Models.AddressModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class SystemDetailsModel
    {
        public int SystemDetailsModelId { get; set; }
        public int  BusinessId { get; set; }
        public string BusinessName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Web address")]
        public string Url { get; set; }

        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }

        public Suburb Suburb { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }
        
        [Display(Name = "Unit/Complex")]
        public string AddressLine2 { get; set; }
    }
}
