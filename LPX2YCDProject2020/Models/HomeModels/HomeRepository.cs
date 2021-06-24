using LPX2YCDProject2020.Models.ContactUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.HomeModels
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ContactUsModel GetSystemDetailsAsync()
        {
            IList<SystemDetailsModel> model = _context.SystemDetails.Include(p => p.Suburb).ThenInclude(p => p.City).ThenInclude(p => p.Province).ToList();

            ContactUsModel data = new ContactUsModel();

            foreach (var a in model)
            {
                string surb = a.Suburb?.SuburbName.ToString();
                data.BusinessName = a.BusinessName;
                data.AddressLine1 = a.AddressLine1;
                data.AddressLine2 = a.AddressLine2;
                data.BusinessName = a.BusinessName;
                data.ContactNumber = a.ContactNumber;
                data.EmailAddress = a.EmailAddress;
                data.SuburbName = a.Suburb.SuburbName;
                data.PostalCode = a.Suburb.PostalCode;
                data.CityName = a.Suburb.City.CityName;
                data.ProvinceName = a.Suburb.City.Province.ProvinceName;
                data.Country = a.Suburb.City.Province.Country;
            }
            return data;
        }
    }
}
