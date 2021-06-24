using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class SubjectDetails
    {
        //public SubjectDetails()
        //{
        //    this.StudentProfileModel = new HashSet<StudentProfileModel>();
        //}

        public int Id { get; set; }
        public string SubjectName { get; set; }
        //public object StudentProfileModel { get; }

        public List<StudentSubjects> Enrolments { get; set; } /*= new HashSet<StudentSubjects>();*/
    }
}
