using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class RegionalCoordinatorsReports
    {
        [Key]
        public string RegCoordinatorId { get; set; }


        [Display(Name = "Regional Coordinator Name")]
        public string Name { get; set; }

        [Display(Name = "Regional Coordinator Surname")]
        public string Surname { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Assigned number of centers")]
        public int NumberOfCenters { get; set; }
               
    }
}
