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

namespace LPX2YCDProject2020.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private IAddressRepository _addressRepository;

        public AccountController(ApplicationDbContext context, IAddressRepository addressRepository, IAccountRepository accountRepository, IUserService userService)
        {
            _accountRepository = accountRepository;
            _userService = userService;
            _context = context;
            _addressRepository = addressRepository;
        }

        //[Route("signup")]
        public IActionResult SignUp() =>  View();
       

        //[Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUp)
        {
                signUp.DateJoined = DateTime.Now.ToString("yyyy/MM/dd");
                if (ModelState.IsValid)
                {
                    var result = await _accountRepository.CreateUserAsync(signUp);
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
        public IActionResult UpdateProfile()
        {
            var userId = _userService.GetUserId();
            IQueryable<StudentProfileModel> result = _context.StudentProfiles.Where(p => p.UserId == userId);

            StudentProfileModel model = new StudentProfileModel();

            foreach (var c in result)
            {
                model.AddressLine1 = c?.AddressLine1;
                model.AddressLine2 = c?.AddressLine2;
                model.ProvinceId = c.ProvinceId;
                model.CityId = c.CityId;
                model.SuburbId = c.SuburbId;
                model.NameOfSchool = c.NameOfSchool;
                model.Grade = c.Grade;
                

            }
            
          
            ViewBag.SubjectList = new SelectList(_addressRepository.GetSubjectListAsync(), "Id", "SubjectName");
            ViewBag.ProvinceList = new SelectList(_addressRepository.GetProvinceListAsync(), "ProvinceId", "ProvinceName");
            return View(model);
        } 

       

        [HttpPost]
        public async Task<JsonResult> AddStudentSubjects(int SubjectId, string studentId)
        {
            StudentSubjects subjects = new StudentSubjects
            {
                UserId = studentId,
                SubjectId = SubjectId
            };

            var exists = _context.Users.FindAsync(studentId);
            if(exists != null)
            {
                await _context.AddAsync(subjects);
                await _context.SaveChangesAsync();
                return Json("Success");
            }
            else
            {
                return Json(NotFound());
            }
        }

        public async Task<IActionResult> UpdateAddress(StudentProfileModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UpdateProfile));
            }
            return RedirectToAction(nameof(UpdateProfile), new { model});
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
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl); 
                    }
                    return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

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
            return RedirectToAction("Index", "Home");
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
                UserId = uid
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")] 
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token.Replace(' ', '+');
                var result =  await _accountRepository.ResetPasswordAsync(model);

                if(result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSucess = true;
                    return View(model);
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
            var userId = _userService.GetUserId();
            var result = _context.StudentSubjects.Where(p => p.UserId == userId);
             

            //var learner = _context.StudentProfiles.

            int[] ids = null ;
            int i = 0;
            List<SubjectDetails> studentSubjects = new List<SubjectDetails>();

            //This is where we left off.



            foreach (var a in result)
            {
               ids[i] = a.SubjectId;
                i++;
            }

            for (int b = 0; b <= ids.Length; b++)
                studentSubjects = (List<SubjectDetails>)_context.Subject.Where(c => c.Id == ids[b]);
            

            return Json(new SelectList(studentSubjects, "SubjectId", "SubjectName"));
        }
    }
}
