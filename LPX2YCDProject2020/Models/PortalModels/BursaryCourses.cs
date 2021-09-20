using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class BursaryCourses
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public Bursary Bursary { get; set; }
        public int BursaryId { get; set; }
    }
}
