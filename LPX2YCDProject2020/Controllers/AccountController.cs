using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.EmailModels;
using LPX2YCDProject2020.Models.PasswordResetModel;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LPX2YCDProject2020.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, IAddressRepository addressRepository, IAccountRepository accountRepository, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _userService = userService;
            _context = context;
            _addressRepository = addressRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        //[Route("signup")]
        public IActionResult SignUp() =>  View();
       
        //[Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUp)
        {
                signUp.DateJoined = DateTime.Now.ToShortDateString();
                if (ModelState.IsValid)
                {
                    var result = await _accountRepository.CreateLearnerAccountAsync(signUp);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);

                        return View(signUp);
                    }
                    ModelState.Clear();
                    return RedirectToAction("ConfirmEmail", new { email = signUp.Email });
                }
            
            return View(signUp);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            try { 
            var userId = _userService.GetUserId();

            var ViewModel = new StudentProfileViewModel(); 
            ViewModel.LearnerProfiles = await _context.StudentProfiles
                .Include(c => c.suburb)
                .ThenInclude(c => c.City)
                .ThenInclude(c => c.Province)
                .Where(i => i.UserId == userId)
                .ToListAsync();

            ViewModel.EnrolledSubjects = await _context.StudentSubjects
                .Include(c => c.Subjects)
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");
            return View(ViewModel);
                    }
            catch(Exception c)
            {
                return RedirectToAction("ErrorPage", "Account", new { message = c }) ;
            }
        }

        public async Task<IActionResult> SchoolReport(string Id)
        {
            var ViewModel = new StudentProfileViewModel();
            if (Id != null)
            {
                 var EnrolledSubjects = await _context.StudentSubjects
                    .Include(c => c.Subjects)
                    .Where(c => c.UserId == Id)
                    .AsNoTracking()
                    .ToListAsync();
                ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");

                return View(EnrolledSubjects);
            }
            else
            {
                var userId = _userService.GetUserId();

                 ViewModel.EnrolledSubjects = await _context.StudentSubjects
                  .Include(c => c.Subjects)
                  .Where(c => c.UserId == userId)
                  .AsNoTracking()
                  .ToListAsync();
                ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");

                return View(ViewModel);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddStudentSubjects(StudentProfileViewModel model)
        {
            var newStudentSubject = new StudentSubjects
            {
                Comments = model.Subjects.Comments,
                Year = DateTime.Now.Year.ToString(),
                FirstTermMark = model.Subjects.FirstTermMark,
                SecondTermMark = model.Subjects.SecondTermMark,
                ThirdTermMark = model.Subjects.ThirdTermMark,
                Target = model.Subjects.Target,
                SubjectId = model.Subjects.SubjectId,
                UserId = model.Subjects.UserId
            };

            if (ModelState.IsValid)
            {
                var found = await _context.StudentSubjects.SingleAsync(v => v.SubjectId == newStudentSubject.SubjectId && v.UserId == newStudentSubject.UserId);

                if (found == null)
                {
                    _context.Add(newStudentSubject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SchoolReport));
                }
                else
                {
                    found.Comments = model.Subjects.Comments;
                    found.Year = DateTime.Now.Year.ToString();
                    found.FirstTermMark = model.Subjects.FirstTermMark;
                    found.SecondTermMark = model.Subjects.SecondTermMark;
                    found.ThirdTermMark = model.Subjects.ThirdTermMark;
                    found.Target = model.Subjects.Target;
                    found.SubjectId = model.Subjects.SubjectId;
                    found.UserId = model.Subjects.UserId;

                    _context.Update(found);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SchoolReport));
                }
            }
            return RedirectToAction(nameof(SchoolReport), new { ModelState });
        }

        [HttpGet]
        public ActionResult EditStudentSubjects(int id)
        {
            //We here working on loading the edit view/modal
            var enrolment = _context.StudentSubjects.Where(c => c.Id == id).FirstOrDefault();

            ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");
            return View("EditSubjectModal", enrolment);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditStudentSubjects(StudentSubjects model)
        {
          
            model.Year = DateTime.Now.Year.ToString();
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SchoolReport));
        }

        [Authorize]
        [HttpGet]
        public IActionResult UpdateProfileDetails(bool IsSuccess = false)
        {
            ViewBag.IsSuccess = IsSuccess;
            var user = _userService.GetUserId();

            var details = _context.StudentProfiles.Include(a => a.suburb)
                .ThenInclude(c => c.City)
                .ThenInclude(m => m.Province).SingleOrDefault(p=>p.UserId == user);

            ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");
            ViewBag.ProvinceList = new SelectList(_addressRepository.GetProvinceListAsync(), "ProvinceId", "ProvinceName");
            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileDetails(StudentProfileModel model)
        {
            model.UserId = _userService.GetUserId();
            if (ModelState.IsValid)
            {
                if(model.ProfilePhoto != null)
                {
                    string folder = "Images/ProfilePhotos/";
                    folder += Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                    model.ImageUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                   await  model.ProfilePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                var exists = _context.StudentProfiles.Where(f=>f.UserId == model.UserId).SingleOrDefault();

                if(exists != null)
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UpdateProfileDetails), new { IsSuccess = true });
                }
                else
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UpdateProfileDetails), new { IsSuccess = true });
                }

            }
            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signIn, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signIn);
                if (result.Succeeded)
                {
                    //if (!string.IsNullOrEmpty(returnUrl))
                    //{
                    //    return LocalRedirect(returnUrl);
                    //}
                    return RedirectToAction("ViewProfile", "Account");
                }
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Please verify your accout before attempting to login");
                else if (result.IsLockedOut == true)
                    ModelState.AddModelError("", "Your account has been locked after 5 failed login attempts. Come back in 5..");
                else
                    ModelState.AddModelError("", "Invalid login credentials");
            } 
            return View(signIn);
        }
     
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOut();
            return RedirectToAction("Home", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if(result.Succeeded)
                {
                    ViewBag.IsSuccessful = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmationModel model = new EmailConfirmationModel
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(uid))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmail(uid, token);
                if (result.Succeeded)
                    model.EmailVerified = true;              
            }
            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmationModel emails)
        {
            var user = await _accountRepository.GetUserByEmailAsync(emails.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    emails.EmailVerified = true;
                    return View(emails);
                }
                await _accountRepository.GenerateNewEmailTokenAsync(user);
                emails.EmailSent = true;
                ModelState.Clear();
            }
            else
                ModelState.AddModelError("", "There appears to be an error, please try again later..");

            return View(emails);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if(ModelState.IsValid) 
            {
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);

                if(user !=null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }


        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                Token = token,
                UserId = uid,
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")] 
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result =  await _accountRepository.ResetPasswordAsync(model);

                if(result.Succeeded)
                {
                    ModelState.Clear();
                    ViewBag.Successful = "Password updated successfully";
                    model.IsSucess = true;
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        public JsonResult GetCityList(int ProvinceId)
        {
            var cityList = _context.Cities.Where(p => p.provinceId == ProvinceId).ToList();
            return Json(new SelectList(cityList, "CityId", "CityName"));
        }

        public JsonResult GetSuburbList(int CityId)
        {
            var suburbList = _context.Suburbs.Where(p => p.CityId == CityId).ToList();
      
            return Json(new SelectList(suburbList, "SuburbId", "SuburbName"));
        }

        public JsonResult GetSuburbPCode(int SuburbId)
        {
            var code = _context.Suburbs.Where(p => p.SuburbId == SuburbId).ToList();
            return Json(new SelectList(code, "SuburbId", "PostalCode"));
        }

        public JsonResult GetSubjectDetails()
        {
          
            //This is where we left off.
            var UserId = _userService.GetUserId();

            var results = _context.StudentSubjects
                .Include(p => p.Subjects)
                .FirstOrDefault(p => p.UserId == UserId);


            //for (int b = 0; b <= ids.Length; b++)
            //    studentSubjects = (List<SubjectDetails>)_context.Subject.Where(c => c.Id == ids[b]);
            
            //This is where I left OFF
            return Json(results);
        }

        public  IActionResult deleteEnrolment(int id)
        {
            StudentSubjects c = _context.StudentSubjects.Where(c => c.Id == id).FirstOrDefault<StudentSubjects>();
            _context.StudentSubjects.Remove(c);
            _context.SaveChanges();

            return RedirectToAction(nameof(SchoolReport));
        }

        public IActionResult Find(int id)
        {
            var enrolment = _context.StudentSubjects.Find(id);
            return new JsonResult(enrolment);
        }
    }
}
