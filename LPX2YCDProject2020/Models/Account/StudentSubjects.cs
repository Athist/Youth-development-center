using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
   
    public class StudentSubjects
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        StudentProfileModel Student { get; set; }

        [Required]
        public int SubjectId { get; set; }
        StudentSubjects Subjects { get; set; }

        [Display(Name = "Tell us more")]
        public string Comments { get; set; }

        [Display(Name = "Term 1 Mark")]
        public string FirstTermMark { get; set; }

        [Display(Name = "Term 2 Mark")]
        public string SecondTermMark { get; set; }

        [Display(Name = "Term 3 Mark")]
        public string ThirdTermMark { get; set; }

        [Display(Name = "Target final mark")]
        public string Target { get; set; }

    }
}
