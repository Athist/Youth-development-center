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

        public HomeController(ApplicationDbContext context, IUserService userService, IEmailService emailService, IAddressRepository addressRepository)
        {
            _userService = userService;
            _emailService = emailService;
            _addressRepository = addressRepository;
            _context = context;
        }

        public IActionResult Home()
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.learners = _context.StudentProfiles.ToList();
            viewModel.programmes = _context.Programmes.ToList();
            return View(viewModel); 
        }

        public  IActionResult Index()
        {
            var results = _context.CenterDetails.FirstOrDefault();
    
            return View(results);
        }


        public IActionResult AboutUs()
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.learners = _context.StudentProfiles.ToList();
            viewModel.programmes = _context.Programmes.ToList();
            return View(viewModel);
        }

        [HttpGet]
        //Get Method for contact us form
        public async Task<IActionResult> ContactUs() 
         {
            ContactUsModel model = new ContactUsModel();
            //string centerName; 
            model.systemDetails = await _context.CenterDetails.Include(c => c.Suburb)
                .ThenInclude(v => v.City)
                .ThenInclude(z => z.Province)
                .ToListAsync();

            return View(model);
        }
        //Post Method for contact us form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEnquiry(ContactUsModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Enquiries.Add(model.ContactUsFormModel);
                await _context.SaveChangesAsync();
                ViewBag.EnquirySent = "Thank you. We will be in touch soon.";
                return RedirectToAction(nameof(ContactUs));
            }
            return View(model);
        }

        //public IActionResult LearningMaterial() => View();

        //public IActionResult MakeAppointment() => View();

        public IActionResult Programmes() => 
            View(_context.Programmes.ToList());

        public IActionResult EmailUs() =>   View();


      
    }
}
