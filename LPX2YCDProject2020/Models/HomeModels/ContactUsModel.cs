using LPX2YCDProject2020.Models.AddressModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.ContactUs
{
    public class ContactUsModel
    {
        public ContactUsFormModel ContactUsFormModel { get; set; }

        public  SystemDetailsModel SystemDetailsModel { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name"), Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name"), Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email"), Display(Name = "Email address"), DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please select"), Display(Name = "Select a description that describes you best")]
        public string PatronType { get; set; }

        [Required(ErrorMessage = "Reuired, please type your message")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        public string CityName { get; set; }
        public string Country { get; set; }
        public string ProvinceName { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string PostalCode { get; set; }

        [Display(Name = "Phone number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Web address")]
        public string Url { get; set; }

        [Display(Name = "Suburb")]
        public string SuburbName { get; set; }

        public Suburb Suburb { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Unit/Complex")]
        public string AddressLine2 { get; set; }
    }
}
