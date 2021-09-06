using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AdminModels
{
    public class Programme
    {
        public int Id { get; set; }

        [Display(Name = "Programme description"), Required(ErrorMessage = "Please enter a decsription")]
        public string Decription { get; set; }

        [Display(Name = "Programme name"), Required(ErrorMessage = "Please enter the name of the programme")]
        public string ProgrammeName { get; set; }

        [Required(ErrorMessage = "Please provide a date")]
        public DateTime Date { get; set; }

        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        public string Duration { get; set; }

        public string Venue { get; set; }

        public string Link { get; set; }

        [Display(Name = "Category"), Required(ErrorMessage = "Please select a category")]
        public string EventCategory { get; set; }

        [NotMapped]
        [Display(Name = "Profile Photo")]
        public IFormFile ProfilePhoto { get; set; }

        public string ImageUrl { get; set; }
    }
}
