using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.PortalModels;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Controllers
{
    public class PortalController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountRepository _accRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAddressRepository _addressRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICompositeViewEngine _viewEngine;


        public PortalController(ICompositeViewEngine viewEngine, IUserService userService, IAccountRepository accRepository, ApplicationDbContext context, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _viewEngine = viewEngine;
            _userService = userService;
            _userManager = userManager;
            _accRepository = accRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _addressRepository = addressRepository;
            _context = context;
        }

        //Bursaries methods
        public IActionResult AddBursaries() => View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveBursary(Bursary model)
        {
            var result = _context.Bursaries.SingleOrDefault(c => c.Description == model.Description);
            if(result != null)
            {
                ModelState.AddModelError("", "The bursary already exists");
                return RedirectToAction(nameof(AddBursaries));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Bursaries.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListBursaries));
                }
            }
            catch(Exception c)
            {
                return RedirectToAction("ErrorPage", "Account", new { message = c });
            }
            return RedirectToAction(nameof(AddBursaries)); 
        }

        public IActionResult ListBursaries() =>
            View(_context.Bursaries
            .Include(c => c.BursaryCourses)
            .AsNoTracking()
            .ToList());

        public IActionResult AddBursarySubject()
        {
            ViewBag.Bursary = new SelectList(GetBursariesAsync(), "Id", "Description");
            ViewBag.Course = new SelectList(GetCoursesAsync(), "Id", "CourseName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBursarySubject(BursaryCourses model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.BursaryCourses.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListBursaries));
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("ErrorPage", "Account", new { message = e});
            }
            return View();           
        }

        public IActionResult ViewBursary() => View();
        public IActionResult UpdateBursary() => View();
        public IActionResult DeleteBursary() => View();

        //End Bursaries methods

        //Study materials methods
        public IActionResult AddMaterial() => View();
        public IActionResult ViewMaterial() => View();
        public IActionResult UpdateMaterial() => View();
        public IActionResult DeleteMaterial() => View();







        public List<Course> GetCoursesAsync()
        {
            List<Course> courses = _context.Courses.ToList();
            return courses;
        }
        public List<Bursary> GetBursariesAsync()
        {
            List<Bursary> bursaries = _context.Bursaries.ToList();
            return bursaries;
        }
    }
}
