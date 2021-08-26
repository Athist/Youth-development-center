using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.AddressModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        //<-----Start of Province action methods ------>

        public IActionResult ViewProvinces() => View(_context.Provinces.ToList());
  
        [HttpGet]
        public IActionResult AddProvinces() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProvinces(Province model)
        {
            Province result = _context.Provinces.FirstOrDefault(a => a.ProvinceName == model.ProvinceName);

            if (result != null)
            {
                ModelState.AddModelError("", "Province already exists");
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.Country = "South Africa";
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction(nameof(ViewProvinces));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditProvince(int? provinceId)
        {
            if (provinceId == 0)
            {
                return NotFound();
            }

            Province province = await _context.Provinces.SingleOrDefaultAsync(c => c.ProvinceId == provinceId);

            if (province == null)
                return NotFound();

            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProvince(Province model)
        {
            if (ModelState.IsValid)
            {
                var province = await _context.Provinces.SingleOrDefaultAsync(c=> c.ProvinceId == model.ProvinceId);

                if (province == null)
                    return NotFound();
                else
                {
                    province.ProvinceName = model.ProvinceName;
                    province.Country = "South Africa";
                    _context.Update(province);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ViewProvinces));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProvince(int? provinceId)
        {
            if (provinceId == 0)
                return NotFound();
            var province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == provinceId);

             _context.Remove(province);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewProvinces));
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesByProvince(int? provinceId)
        {

            if (provinceId == 0)
            {
                return NotFound();
            }

            var viewModel = new CascadingAddressClass();

            viewModel.Province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == provinceId);
            //viewModel.Province = (Province)_context.Provinces.Where(c => c.ProvinceId == provinceId);
            viewModel.cities = _context.Cities.Where(x => x.provinceId == provinceId).ToList();

            return View(viewModel);
        }

        //<-----End of Province action methods ------>

        //<-----start of City action methods ------>
        public async Task<IActionResult> AddCities(int? id)
        {
            if (id == 0)
                return NotFound();

            var ViewModel = new CascadingAddressClass();

            ViewModel.Province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == id);

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCities(CascadingAddressClass model)
        {
            City result = _context.Cities.FirstOrDefault(a => a.CityName == model.City.CityName);
            int id = result.provinceId;
            if (result != null)
            {
                ModelState.AddModelError("", "City already exists");
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Cities.Add(model.City);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction(nameof(GetCitiesByProvince),new { provinceId = id });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditCity(int? cityId)
        {
            if (cityId == 0)
            {
                return NotFound();
            }

            City city = await _context.Cities.SingleOrDefaultAsync(c => c.CityId == cityId);

            if (city == null)
                return NotFound();

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(City model)
        {
            if (ModelState.IsValid)
            {
                var city = await _context.Cities.SingleOrDefaultAsync(c => c.CityId == model.CityId);

                if (city == null)
                    return NotFound();
                else
                {
                    city.CityName = model.CityName;
                    city.provinceId = model.provinceId;
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetCitiesByProvince), new { provinceId = model.provinceId });
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCity(int? cityId)
        {
            if (cityId == 0)
                return NotFound();
            var city = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == cityId);
            var provinceId = city.provinceId;
            _context.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetCitiesByProvince), new { provinceId = provinceId });
        }

        [HttpGet]
        public async Task<IActionResult> GetSuburbsByCity(int? cityId)
        {
            if (cityId == 0)
            {
                return NotFound();
            }

            var viewModel = new CascadingAddressClass();

            viewModel.City = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == cityId);
            //viewModel.Province = (Province)_context.Provinces.Where(c => c.ProvinceId == provinceId);
            viewModel.suburbs = _context.Suburbs.Where(x => x.CityId == cityId).ToList();

            return View(viewModel);
        }
        //<-----End of City action methods ------>


        //<-----start of Suburb action methods ------>
        public async Task<IActionResult> AddSuburb(int? id)
        {
            if (id == 0)
                return NotFound();

            var ViewModel = new CascadingAddressClass();

            ViewModel.City = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == id);

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSuburb(CascadingAddressClass model)
        {
            Suburb result = _context.Suburbs.FirstOrDefault(a => a.SuburbName == model.Suburb.SuburbName);

            if (result != null)
            {
                ModelState.AddModelError("", "Suburb already exists");
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.Suburb.CityId = model.City.CityId;
                    _context.Suburbs.Add(model.Suburb);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = model.Suburb.CityId });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditSuburb(int? suburbId)
        {
            if (suburbId == 0)
            {
                return NotFound();
            }

            Suburb suburb = await _context.Suburbs.SingleOrDefaultAsync(c => c.SuburbId == suburbId);

            if (suburb == null)
                return NotFound();

            return View(suburb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSuburb(Suburb model)
        {
            if (ModelState.IsValid)
            {
                var suburb = await _context.Suburbs.SingleOrDefaultAsync(c => c.SuburbId == model.SuburbId);

                if (suburb == null)
                    return NotFound();

                else
                {
                    suburb.SuburbName = model.SuburbName;
                    suburb.PostalCode = model.PostalCode;
                    suburb.CityId = model.CityId;
                    _context.Update(suburb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = model.CityId });
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSuburb(int? suburbId)
        {
            if (suburbId == 0)
                return NotFound();
            var suburb = await _context.Suburbs.FirstOrDefaultAsync(i => i.SuburbId == suburbId);
            var cityId = suburb.CityId;
            _context.Remove(suburb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = cityId });
        }
        //<-----End of suburb action methods ------>

        //<-----About us page action methods------>
        public IActionResult AddAboutInfo()
        {
            var details = _context.SystemDetails.ToList();
            return View(details);
        }
    }
}
