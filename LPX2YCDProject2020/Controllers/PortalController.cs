using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.PortalModels;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (result != null)
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
            catch (Exception c)
            {
                return RedirectToAction("ErrorPage", "Account", new { message = c });
            }
            return RedirectToAction(nameof(AddBursaries));
        }

        public async Task<IActionResult> ListBursaries() =>
         View(await _context.Bursaries
        .Include(p => p.RequiredSubjects)
        .ThenInclude(p => p.SubjectDetails)
        .Include(q => q.SponsoredFields)
        .ThenInclude(w => w.Course)
        .OrderBy(w => w.openingDate)
        .ToListAsync());

        public async Task<IActionResult> ShowAllBursaries() =>
         View(await _context.Bursaries
             .Include(p => p.RequiredSubjects)
             .ThenInclude(p => p.SubjectDetails)
             .Include(q => q.SponsoredFields)
             .ThenInclude(w => w.Course)
             .OrderBy(w => w.openingDate)
             .ToListAsync());


        public async Task<IActionResult> BursaryDetails(int Id)
        {
            if (Id == 0)
                return RedirectToAction(nameof(BursaryDetails));

            var results = await _context.Bursaries
                .Include(p => p.RequiredSubjects)
             .ThenInclude(p => p.SubjectDetails)
             .Include(q => q.SponsoredFields)
             .ThenInclude(w => w.Course)
             .OrderBy(w => w.openingDate)
             .FirstOrDefaultAsync(c => c.Id == Id);

            return View(results);
        }

        //
        public IActionResult AddBursarySubject(int id)
        {
            if (id == 0)
                return RedirectToAction("ErrorPage", "Admin");

            var result = _context.Bursaries.FirstOrDefault(c => c.Id == id);

            if (result == null)
            {
                ModelState.AddModelError("", "Something went wrong. Try again later");
                return View();
            }

            BursaryCourses model = new BursaryCourses();
            model.BursaryId = result.Id;
            ViewBag.Course = new SelectList(GetCoursesAsync(), "Id", "CourseName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBursarySubject(BursaryCourses model)
        {
            BursaryCourses results = (from c in _context.BursaryCourses
                                      where c.BursaryId == model.BursaryId
                                      && c.CourseId == model.CourseId
                                      select c).FirstOrDefault();
            if (results != null)
            {
                ModelState.AddModelError("", "The record already exists");
                ViewBag.Course = new SelectList(GetCoursesAsync(), "Id", "CourseName");
                return View(model);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.BursaryCourses.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListBursaries));
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("ErrorPage", "Admin", new { message = e });
            }
            return View();
        }
        //
        public IActionResult AddRequiredSubject(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("", "Not available, please try again later");
                return View();
            }

            var result = _context.Bursaries.FirstOrDefault(c => c.Id == id);

            if (result == null)
            {
                ModelState.AddModelError("", "Not available, please try again later");
                return View();
            }

            RequiredSubjects model = new RequiredSubjects();
            model.BursaryId = result.Id;
            ViewBag.Subject = new SelectList(GetSubjectAsync(), "Id", "SubjectName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequiredSubject(RequiredSubjects model)
        {
            RequiredSubjects results = (from c in _context.SubjectRequirement
                                        where c.BursaryId == model.BursaryId
                                      && c.SubjectId == model.SubjectId
                                        select c).FirstOrDefault();
            if (results != null)
            {
                ModelState.AddModelError("", "The record already exists");
                ViewBag.Subject = new SelectList(GetSubjectAsync(), "Id", "SubjectName");
                return View(model);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.SubjectRequirement.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListBursaries));
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("ErrorPage", "Admin", new { message = e });
            }
            return View();
        }


        public IActionResult ViewBursary() => View();
        public IActionResult UpdateBursary() => View();
        public IActionResult DeleteBursary() => View();

        //End Bursaries methods

        //Study materials methods
        public IActionResult AddMaterial()
        {
            ViewBag.Subject = new SelectList(GetSubjectAsync(), "Id", "SubjectName");
            return View();
        }
         

        [HttpPost]
        public async Task<IActionResult> AddMaterial(SubjectResources model)
        {
            if (ModelState.IsValid)
            {
                if(model.Pdf != null)
                {
                    string folder = "Resources/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Pdf.FileName;
                    model.PdfUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.Pdf.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                _context.StudyResources.Add(model);
                await _context.SaveChangesAsync();
                ViewBag.Subject = new SelectList(GetSubjectAsync(), "Id", "SubjectName");
                return RedirectToAction(nameof(AddMaterial), new { isSuccess = true});
            }
            ViewBag.Subject = new SelectList(GetSubjectAsync(), "Id", "SubjectName");
            return View(model);
        }

        public IActionResult ListAllMaterial() 
            => View(_context.StudyResources
                .Include(v=>v.Subject));

        public IActionResult ViewMaterialDetails(int id)
        {
            if (id == 0)
                return RedirectToAction("ErrorPage", "Admin");
            var result = _context.StudyResources
                .Include(q => q.Subject)
                .FirstOrDefault(o => o.Id == id);

            return View(result);
        }

        public IActionResult UpdateMaterial(int Id)
        {
            if (Id == 0)
                return RedirectToAction("ErrorPage", "Admin");

            var result = _context.SubjectRequirement
                .Include(c=>c.SubjectDetails)
                .FirstOrDefault(x => x.Id == Id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMaterial(SubjectResources model)
        {
            if (ModelState.IsValid)
            {
                if (model.Pdf != null)
                {
                    string folder = "Resources/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Pdf.FileName;
                    model.PdfUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.Pdf.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                _context.StudyResources.Update(model);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteMaterial(int id)
        {
            if (id == 0)
                return RedirectToAction("ErrorPage", "Admin");
            var results = _context.StudyResources.FirstOrDefault(c => c.Id == id);
            _context.Remove(results);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListAllMaterial));
        }




        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        public List<SubjectDetails> GetSubjectAsync()
        {
            List<SubjectDetails> subjects = _context.Subject.ToList();
            return subjects;
        }
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
