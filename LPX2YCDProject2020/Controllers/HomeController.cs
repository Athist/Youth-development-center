using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Models.AddressModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using LPX2YCDProject2020.Models;
using Microsoft.EntityFrameworkCore;
using LPX2YCDProject2020.Models.ContactUs;
using LPX2YCDProject2020.Models.HomeModels;

namespace LPX2YCDProject2020.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IAddressRepository _addressRepository;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ApplicationDbContext context, IHomeRepository homeRepository, IUserService userService, IEmailService emailService, IAddressRepository addressRepository)
        {
            _userService = userService;
            _emailService = emailService;
            _addressRepository = addressRepository;
            _context = context;
            _homeRepository = homeRepository;
        }

        public  IActionResult Index() => View();

        public IActionResult _EnquiryForm() => View();

        public IActionResult AboutUs()
        {
           
            return View();
            
        }

        [HttpGet]
        public IActionResult Gallery()
        {

            return View(_context.Cities.ToList());
        }

        [HttpPost]







        //Get Method for contact us form
        public IActionResult ContactUs() => View(_homeRepository.GetSystemDetailsAsync());

        //Post Method for contact us form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsModel model)
        {

            if (ModelState.IsValid)
            {
                _context.Add(model);
         
                var result = await _context.SaveChangesAsync();
                ViewBag.EnquirySent = "Thank you. We will be in touch soon.";

                return RedirectToAction(nameof(ContactUs), model = _homeRepository.GetSystemDetailsAsync());
            }

            return View(model);
        }

        //public IActionResult LearningMaterial() => View();

        //public IActionResult MakeAppointment() => View();

        public IActionResult Donate() => View();

        public IActionResult EmailUs() =>   View();


      
    }
}
