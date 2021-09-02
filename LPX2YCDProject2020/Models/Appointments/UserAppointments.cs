﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Appointments
{
    public class UserAppointments
    {
        public int Id { get; set; }

        [Display(Name = "Assigned to")]
        public string assignedEmployee { get; set; }

        [Display(Name = "Name"), Required(ErrorMessage = "Please enter your name")]
        public string userId { get; set; }

        [Display(Name = "Time"), DataType(DataType.Time), Required(ErrorMessage = "Please select time")]
        public DateTime Time { get; set; }

        [Display(Name = "Date"), DataType(DataType.Date), Required(ErrorMessage = "Please select date")]
        public DateTime Date { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Note ")]
        public string CenterNotes { get; set; }

        public string Status { get; set; }

        [Display(Name = "Reson for visit"), Required]
        public int AppointmentTypeId { get; set; }
        public AppointmentType appointmentTypes { get; set; }

        public bool Saved { get; set; }
    }
}
