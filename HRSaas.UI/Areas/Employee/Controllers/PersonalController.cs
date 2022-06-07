using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class PersonalController : Controller
    {
        //Resim yükleme için
        IHostingEnvironment _env;
        Random rnd = new Random();
        string directory;

        private readonly IAddressBusiness _addressBusiness;
        private readonly IPersonalBusiness _personalBusiness;
        private readonly ICityBusiness _cityBusiness;
        private readonly ICountyBusiness _countyBusiness;
        private UserManager<Personal> _userManager;


        public PersonalController(IAddressBusiness addressBusiness, IPersonalBusiness personalBusiness, ICityBusiness cityBusiness, ICountyBusiness countyBusiness, UserManager<Personal> userManager)
        {
            _addressBusiness = addressBusiness;
            _personalBusiness = personalBusiness;
            _cityBusiness = cityBusiness;
            _countyBusiness = countyBusiness;
            _userManager = userManager;            
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            await FillProfileSummary();
            var user = await _personalBusiness.GetById(id);
            if (user != null)
            {
                var address = await _addressBusiness.GetById(user.AddressId);
                EmployeeUpdateVM employeeUpdateVM = new EmployeeUpdateVM
                {
                    Id = user.Id,
                    Phone = user.Phone,
                    Mail = user.Email,
                    AddressId = user.AddressId,
                    AddressName = address.AddressName,
                    AddressDescription = address.Description,
                    PostalCode = address.PostalCode,
                    CityId = address.CityId,
                    CountyId = address.CountyId
                };
                FillSelectCity();
                FillSelectCounty((int)address.CityId);
                return View(employeeUpdateVM);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeeUpdateVM employe)
        {
            await FillProfileSummary();
            var user = await _personalBusiness.GetById(employe.Id);
            var address = await _addressBusiness.GetById(user.AddressId);
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    address.AddressName = employe.AddressName;
                    address.CityId = employe.CityId;
                    address.CountyId = employe.CountyId;
                    address.Description = employe.AddressDescription;
                    address.PostalCode = employe.PostalCode;

                    await _addressBusiness.Update(address);
                    user.Phone = employe.Phone;
                    user.Email = User.FindFirstValue(ClaimTypes.Email);
                    if (await _personalBusiness.Update(user) != null)
                        return RedirectToAction("Index", "Home");
                }
            }
            FillSelectCity();
            FillSelectCounty((int)address.CityId);
            employe.Mail = User.FindFirstValue(ClaimTypes.Email);
            return View(employe);
        }

        [HttpPost]
        public IActionResult GetCountiesByCityId([FromBody] int CityId)
        {
            if (CityId == 0)
                return BadRequest("Lütfen şehir seçimi yapınız");
            var counties = _countyBusiness.GetAll(t0 => t0.CityId == CityId);
            return Json(new SelectList(counties, "Id", "CountyName"));
        }

        private void FillSelectCity()
        {
            var cities = _cityBusiness.GetAll().ToList();
            List<SelectListItem> citiesSelectListItems = new List<SelectListItem>();
            foreach (var item in cities)
            {
                citiesSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.CityName
                });
            }
            ViewBag.CityList = new SelectList(citiesSelectListItems, "Value", "Text");
        }

        private void FillSelectCounty(int cityId)
        {
            List<County> counties = _countyBusiness.GetAll(t0 => t0.CityId == cityId).ToList();
            List<SelectListItem> countiesSelectListItems = new List<SelectListItem>();
            foreach (var item in counties)
            {
                countiesSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.CountyName
                });
            }
            ViewBag.CountyList = new SelectList(countiesSelectListItems, "Value", "Text");
        }

        private async Task FillProfileSummary()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            ViewBag.UserPhoto = loggedInPersonal.Photo;
            ViewBag.UserName = loggedInPersonal.FirstName + " " + loggedInPersonal.LastName;
        }
    }
}
