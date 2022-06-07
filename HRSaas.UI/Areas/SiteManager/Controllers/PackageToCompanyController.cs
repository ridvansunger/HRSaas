using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.SiteManager.Controllers
{
    [Area("SiteManager")]
    [Authorize(Roles = "SiteManager")]
    public class PackageToCompanyController : Controller
    {
        IMapper _mapper;
        ICompanyBusiness _companyBusiness;
        IPackageBusiness _packageBusiness;
        public PackageToCompanyController(ICompanyBusiness companyBusiness, IMapper mapper, IPackageBusiness packageBusiness)
        {
            _packageBusiness = packageBusiness;
            _mapper = mapper;
            _companyBusiness = companyBusiness;
        }
        [HttpGet]
        public IActionResult Index()
        {
            FillPersonalAddAndUpdateView();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CompanyVM companyVM)
        {

            Company company = await _companyBusiness.GetById(companyVM.Id);
            company.PackageId = companyVM.PackageId;
            company.PackageStartDate = DateTime.Now;
            try
            {
                
                await _companyBusiness.Update(company);
                TempData["packagetoCompanyMessage"] = "Paket Atama Basariyla Gerceklestirildi";
            }
            catch (Exception)
            {
                TempData["packagetoCompanyMessage"] = "Paket Atama Basarisiz";

                throw;
            }
            
            return RedirectToAction("Index");
        }
        private void FillPersonalAddAndUpdateView()
        {

            var packages = _packageBusiness.GetAll().ToList();
            List<SelectListItem> packageSelectListItems = new List<SelectListItem>();
            foreach (var item in packages)
            {
                packageSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.PackageName
                });
            }
            ViewBag.PackageList = new SelectList(packageSelectListItems, "Value", "Text"); ;

      
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

      
        }
    }
}
