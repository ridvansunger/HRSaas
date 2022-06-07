using AutoMapper;
using FluentValidation.Results;
using HRSaas.Business.Abstract;
using HRSaas.Business.ValidationRules;
using HRSaas.Core.Helpers;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using HRSaas.UI.Helpers;
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

namespace HRSaas.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class PersonalController : Controller
    {
        #region Resim yükleme için
        IHostingEnvironment _env;
        Random rnd = new Random();
        string directory;
        #endregion
        #region DI
        private IPersonalBusiness _personalBusiness;
        private IDepartmentBusiness _departmentBusiness;
        private ITitleBusiness _titleBusiness;
        private ICityBusiness _cityBusiness;
        private ICountyBusiness _countyBusiness;
        private IAddressBusiness _addressBusiness;
        private ICompanyBusiness _companyBusiness;
        private IMapper _mapper;
        private UserManager<Personal> _userManager;

        #endregion


        public PersonalController(IPersonalBusiness personalBusiness, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, IHostingEnvironment env, ICityBusiness cityBusiness, ICountyBusiness countyBusiness, IAddressBusiness addressBusiness, IMapper mapper, UserManager<Personal> userManager, ICompanyBusiness companyBusiness)
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
          
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            List<PersonalDetailVm> displayVMList = new List<PersonalDetailVm>();

            var values = _personalBusiness.GetAll(t0=>t0.CompanyId==loggedInPersonal.CompanyId);

            foreach (var item in values)
            {
                Department dept = await _departmentBusiness.GetById(Convert.ToInt32(item.DepartmentId));
                Title title = await _titleBusiness.GetById(Convert.ToInt32(item.TitleId));

                PersonalDetailVm displayVM = new PersonalDetailVm()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Mail = item.Mail,
                    TitleName = title.TitleName,
                    Phone = item.Phone,
                    EndDate = item.EndDate,
                    DepertmentName = dept.DepartmentName,
                    Photo = item.Photo,
                    PersonalIsActive = (bool)item.PersonalIsActive,
                    StartDate = item.StartDate,
                    Tc = item.Tc,
                    BirthDate = item.BirthDate
                };

                displayVMList.Add(displayVM);
            }

            return View(displayVMList);
        }

        [HttpGet]
        public IActionResult PersonalAdd()
        {
            //AddMailExtension();
            FillPersonalAddAndUpdateView();
            return View();
        }

        //private void AddMailExtension()
        //{
        //    var userEmail = User.FindFirstValue(ClaimTypes.Email);
        //    string[] subs = userEmail.Split("@");
        //    ViewBag.EmailExtension = "@" + subs[1];
        //}

        public void MailSend()
        {
            MailSender.Send("rdvansunger@gmail.com");
            //MailSender.Send("omersalihkanra@gmail.com","123Pass_");
            //MailSender.Send("infohrsaas@gmail.com","123Pass_");
        }
        //Personel ekleme ve güncelleme
        [HttpPost]
        public async Task<IActionResult> PersonalAdd(PersonalDetailVm personalAdd)
        {
            //AddMailExtension();
            #region Diğer Validation result ile yapılan şekli
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

                    //Personal eklenenuser = new Personal
                    //{
                    //    AddressId = 1,
                    //    AnnualLeave = 1,
                    //    Gender = true,
                    //    Password = "1",
                    //    FirstName = "salih",
                    PersonalEmailCreator(userEmail, personalAdd);
                    var address1 = _mapper.Map<Address>(personalAdd);
                    var addressDto = await _addressBusiness.Add(address1);
                    personalAdd.AddressId = addressDto.Id;
                    personalAdd.CompanyId = loggedInPersonal.CompanyId;
                    _ = personalAdd.ProfileImage != null ? personalAdd.Photo = ImageSave(personalAdd.ProfileImage) : null;

                    var personal = _mapper.Map<Personal>(personalAdd);

                    await _personalBusiness.Add(personal);

                    //}
                    //else
                    //{
                    //    ViewBag.msg = "Hatalı TC.No Girişi";
                    //    ModelState.AddModelError(personalAdd.Tc, errorMessage: "Tc NO hatalı giriş yaptınız.");
                    //}
                }
                else
                {
                    PersonalEmailCreator(userEmail,personalAdd);
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

                    FillPersonalAddAndUpdateView();
                    return View(personalAdd);
                }
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                FillPersonalAddAndUpdateView();
                return View();
            }
           
            return RedirectToAction("Index");

            #endregion

            #region digerValid
            //if (ModelState.IsValid)
            //{
            //    if (personalAdd.Id==0)
            //    {
            //        personalAdd.Mail = personalAdd.Mail + "@hrsaas.com";
            //        var address1 = _mapper.Map<Address>(personalAdd);
            //        var addressDto = await _addressBusiness.Add(address1);
            //        personalAdd.AddressId = addressDto.Id;

            //        _ = personalAdd.ProfileImage != null ? personalAdd.Photo = ImageSave(personalAdd.ProfileImage) : null;

            //        var personal = _mapper.Map<Personal>(personalAdd);
            //        await _personalBusiness.Add(personal);
            //    }
            //    else
            //    {
            //        Address address = _mapper.Map<Address>(personalAdd);
            //        Address addressDto = await _addressBusiness.Update(address);
            //        personalAdd.AddressId = addressDto.Id;
            //        personalAdd.Photo = ImageSave(personalAdd.ProfileImage);
            //        Personal pudto = _mapper.Map<Personal>(personalAdd);
            //        await _personalBusiness.Update(pudto);
            //    }

            //    return RedirectToAction("Index");
            //}
            //var messages = ModelState.ToList();

            //return View(personalAdd);
            #endregion
        }

        private void PersonalEmailCreator(string userEmail, PersonalDetailVm personalAdd)
        {
            string[] subs = userEmail.Split("@");
            personalAdd.Mail = personalAdd.FirstName+personalAdd.LastName + "@" + subs[1];
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



        //Personel Listelemede güncelleye tıklandığında tetiklenen action
        [HttpGet("Manager/Personal/PersonalAdd/{id}")]
        public async Task<IActionResult> PersonalAdd(int id)
        {
            FillPersonalAddAndUpdateView();
            Personal personalDto = await _personalBusiness.GetById(id);
            Address addressDto = await _addressBusiness.GetById((int)personalDto.AddressId);

            PersonalDetailVm personalDetailVm = _mapper.Map<PersonalDetailVm>(personalDto);
            personalDetailVm.AddressName = addressDto.AddressName;
            personalDetailVm.AddressDescription = addressDto.Description;
            personalDetailVm.PostalCode = addressDto.PostalCode;
            personalDetailVm.CityId = (int)addressDto.CityId;
            personalDetailVm.CountyId = (int)addressDto.CountyId;

            return View(personalDetailVm);

        }

        //İle Göre İlçe getirme
        [HttpPost]
        public IActionResult GetCountiesByCityId([FromBody] int CityId)
        {
            var counties = _countyBusiness.GetAll(t0 => t0.CityId == CityId);
            return Json(new SelectList(counties, "Id", "CountyName"));
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

        //Personel Silme
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personalValue = await _personalBusiness.GetById(id);

            if (personalValue.PersonalIsActive == true)
            {
                
                personalValue.PersonalIsActive = false;
                personalValue.EndDate = DateTime.Now;
            }
            else if (personalValue.PersonalIsActive == false)
            {
                personalValue.PersonalIsActive = true;
                personalValue.EndDate = null;
            }
               

            var value = _mapper.Map<Personal>(personalValue);
            await _personalBusiness.Update(value);

            return RedirectToAction("Index");
        }

        //Ekrandaki İl, İlçe, Departman, Ünvan tablosunun dolduruldu
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
