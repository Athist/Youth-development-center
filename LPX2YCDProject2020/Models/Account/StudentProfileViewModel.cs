using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class StudentProfileViewModel
    {
        public IEnumerable<StudentSubjects> EnrolledSubjects { get; set; }
        public IEnumerable<StudentProfileModel> LearnerProfiles { get; set; } 
        public IEnumerable<SubjectDetails> SubjectDetails { get; set; }   
        
        public ButtonPartialModel buttonPartial { get; set; }
        public SubjectDetails Details { get; set; }
        public StudentSubjects Subjects { get; set; }
        public StudentProfileModel learner { get; set; }
            
    }
}
