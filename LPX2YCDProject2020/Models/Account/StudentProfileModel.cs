using LPX2YCDProject2020.Models.AddressModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class StudentProfileModel
    {
        //public StudentProfileModel()
        //{
        //    this.SubjectDetails = new HashSet<SubjectDetails>();
        //}

        [Key]   
        public string UserId{ get; set; }

        public List<StudentSubjects> Enrolments { get; set; } /*= new HashSet<StudentSubjects>();*/

        [Display(Name ="Suburb")]
        public int SuburbId { get; set; }
        public Suburb suburb { get; set; }

        [Display(Name ="Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name ="Building/Complex")]
        public string AddressLine2 { get; set; }

        [Display(Name ="School name")]
        public string NameOfSchool { get; set; }

        [Display(Name ="Grade")]
        public string Grade { get; set; }
        
        //public object SubjectDetails { get; }

        [Display(Name ="Subject")]
        public int subjectId { get; set; }

        [Display(Name ="City")]
        public int CityId { get; set; }

        [Display(Name ="Province")]
        public int ProvinceId { get; set; }
    }
}
