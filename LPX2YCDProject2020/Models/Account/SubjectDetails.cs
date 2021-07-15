using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class SubjectDetails
    {
     
        public int Id { get; set; }
        public string SubjectName { get; set; }
        //public object StudentProfileModel { get; }

        public virtual ICollection<StudentSubjects> Enrolments { get; set; } = new HashSet<StudentSubjects>();
    }
}
