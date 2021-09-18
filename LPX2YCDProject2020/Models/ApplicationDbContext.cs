﻿using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.ContactUs;
using LPX2YCDProject2020.Models.HomeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Appointments;
using LPX2YCDProject2020.Models.AdminModels;

namespace LPX2YCDProject2020.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<SignUpModel> SignUp { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CenterDetails> CenterDetails { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<SubjectDetails> Subject { get; set; }
        public DbSet<StudentProfileModel> StudentProfiles { get; set; }
        public DbSet<StudentSubjects> StudentSubjects { get; set; }
        public DbSet<ContactUsFormModel> Enquiries { get; set; }
        public DbSet<UserAppointments> Appointment { get; set; }
        public DbSet<AppointmentType> AppointmentType{ get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<EventReservations> EventReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentSubjects>()
                .HasOne<SubjectDetails>(sc => sc.Subjects)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(sc => sc.SubjectId);


            builder.Entity<StudentSubjects>()
                .HasOne<StudentProfileModel>(sc => sc.Student)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(sc => sc.UserId);

            //builder.Entity<EventReservations>()
            //     .HasKey(c => new { c.ProgramId, c.UserId });

            //builder.Entity<StudentSubjects>()
            //     .HasKey(c => new { c.UserId, c.SubjectId });

            //builder.Entity<IdentityUserLogin>()
            //    .HasNoKey();

            //builder.Entity<StudentProfileModel>()
            //    .HasMany<StudentSubjects>(v=>v.)
        }

    }
}
 