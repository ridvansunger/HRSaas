using AutoMapper;
using FluentValidation.Results;
using HRSaas.Business.Abstract;
using HRSaas.Business.ValidationRules;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.SiteManager.Controllers
{
    [Area("SiteManager")]
    [Authorize(Roles = "SiteManager")]
    public class ManagerController : Controller
    {
        IHostingEnvironment _env;
        Random rnd = new Random();
        string directory;
        private IPersonalBusiness _personalBusiness;
        private IDepartmentBusiness _departmentBusiness;
        private ITitleBusiness _titleBusiness;
        private ICityBusiness _cityBusiness;
        private ICountyBusiness _countyBusiness;
        private IAddressBusiness _addressBusiness;
        private ICompanyBusiness _companyBusiness;
        private IMapper _mapper;

        private UserManager<Personal> _userManager;
        public ManagerController(IPersonalBusiness personalBusiness, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, IHostingEnvironment env, ICityBusiness cityBusiness, ICountyBusiness countyBusiness, IAddressBusiness addressBusiness, IMapper mapper, UserManager<Personal> userManager, ICompanyBusiness companyBusiness)
        {
            _companyBusiness = companyBusiness;
            _userManager = userManager;
            _personalBusiness = personalBusiness;
            _departmentBusiness = departmentBusiness;
            _titleBusiness = titleBusiness;
            _cityBusiness = cityBusiness;
            _countyBusiness = countyBusiness;
            _env = env;
            directory = _env.ContentRootPath + "/wwwroot/Data/Images/";
            _addressBusiness = addressBusiness;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {
            FillPersonalAddAndUpdateView();
            return View();
        }
        //İle Göre İlçe getirme
        [HttpPost]
        public IActionResult GetCountiesByCityId([FromBody] int CityId)
        {
            var counties = _countyBusiness.GetAll(t0 => t0.CityId == CityId);
            return Json(new SelectList(counties, "Id", "CountyName"));
        }
        [HttpPost]
        public async Task<IActionResult> PersonalAdd(PersonalDetailVm personalAdd)
        {
            PersonalValidator _personalValidator = new PersonalValidator();
            ValidationResult results = _personalValidator.Validate(personalAdd);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
            if (results.IsValid)
            {
                if (personalAdd.Id == 0)
                {
                    //foreach (var item in _personalBusiness.GetAll())
                    //{
                    //    if (item.Tc == personalAdd.Tc && item.PersonalIsActive == true)
                    //    {
                    //        ViewBag.tcmsg = "Aynı Kimlik Numarası ile kayıtlı çalışan bulunmaktadır";
                    //        FillPersonalAddAndUpdateView();
                    //        return View(personalAdd);
                    //    }
                    //}
                    //if (IdentificationNumberValidator.Verify(personalAdd.Tc) == true)
                    //{
                    var address1 = _mapper.Map<Address>(personalAdd);
                    var addressDto = await _addressBusiness.Add(address1);
                    personalAdd.AddressId = addressDto.Id;
                    personalAdd.CompanyId = loggedInPersonal.CompanyId;
                    _ = personalAdd.ProfileImage != null ? personalAdd.Photo = ImageSave(personalAdd.ProfileImage) : null;

                    var personal = _mapper.Map<Personal>(personalAdd);

                    await _personalBusiness.AddManager(personal);

                    //}
                    //else
                    //{
                    //    ViewBag.msg = "Hatalı TC.No Girişi";
                    //    ModelState.AddModelError(personalAdd.Tc, errorMessage: "Tc NO hatalı giriş yaptınız.");
                    //}
                }
                else
                {
                    
                    Address address = _mapper.Map<Address>(personalAdd);
                    Address addressDto = await _addressBusiness.Update(address);
                    personalAdd.AddressId = addressDto.Id;
                    personalAdd.Photo = ImageSave(personalAdd.ProfileImage);

                    Personal pudto = await ManualMapper(personalAdd.Id, personalAdd);
                    pudto.Mail = personalAdd.Mail;
                    pudto.AddressId = personalAdd.AddressId;
                    pudto.BirthDate = personalAdd.BirthDate;
                    pudto.FirstName = personalAdd.FirstName;
                    pudto.LastName = personalAdd.LastName;
                    await _personalBusiness.Update(pudto);

                    return View(personalAdd);

                }
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            FillPersonalAddAndUpdateView();

            return RedirectToAction("Index", "Home");
        }

        //Resim Kaydetme
        private string ImageSave(IFormFile profileImage)
        {
            string imageName = null;
            if (profileImage != null)
                using (var fileStream = new FileStream(Path.Combine(directory, imageName), FileMode.Create, FileAccess.Write))
                {
                    profileImage.CopyTo(fileStream);
                    imageName = rnd.Next(0, 99999).ToString() + ".png";
                }
            return imageName;
        }
        public async Task<Personal> ManualMapper(int id, PersonalDetailVm personalVM)
        {
            Personal personal = await _personalBusiness.GetById(personalVM.Id);
            personal.AddressId = personalVM.AddressId;
            personal.BirthDate = personalVM.BirthDate;
            personal.DepartmentId = personalVM.DepartmentId;
            personal.EndDate = personalVM.EndDate;
            personal.FirstName = personalVM.FirstName;
            personal.Gender = personalVM.Gender;
            personal.LastName = personalVM.LastName;
            personal.Password = personalVM.Password;
            personal.PersonalIsActive = personalVM.PersonalIsActive;
            personal.Phone = personalVM.Phone;
            personal.Salary = personalVM.Salary;
            personal.StartDate = personalVM.StartDate;
            personal.TitleId = personalVM.TitleId;
            personal.CompanyId = personalVM.CompanyId;

            return personal;
        }
        private void FillPersonalAddAndUpdateView()
        {
            
            var departments = _departmentBusiness.GetAll().ToList();
            List<SelectListItem> departmentSelectListItems = new List<SelectListItem>();
            foreach (var item in departments)
            {
                departmentSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DepartmentName
                });
            }
            ViewBag.DepartmentList = new SelectList(departmentSelectListItems, "Value", "Text"); ;

            //-----------------------------------------------------

            var titles = _titleBusiness.GetAll().ToList();
            List<SelectListItem> titleSelectListItems = new List<SelectListItem>();
            foreach (var item in titles)
            {
                titleSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.TitleName
                });
            }
            ViewBag.TitleList = new SelectList(titleSelectListItems, "Value", "Text");
            
            //-----------------------------------------------------

            var Companies = _companyBusiness.GetAll().ToList();
            List<SelectListItem> companySelectListItems = new List<SelectListItem>();
            foreach (var item in Companies)
            {
                companySelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.CompanyName
                });
            }
            ViewBag.CompanyList = new SelectList(companySelectListItems, "Value", "Text");

            //-----------------------------------------------------

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

            //-----------------------------------------------------

            var counties = _countyBusiness.GetAll().ToList();
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
    }
}
