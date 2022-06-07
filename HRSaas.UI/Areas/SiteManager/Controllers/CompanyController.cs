using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace HRSaas.UI.Areas.SiteManager.Controllers
{
    [Area("SiteManager")]
    [Authorize(Roles = "SiteManager")]
    public class CompanyController : Controller
    {
        IMapper _mapper;
        ICompanyBusiness _companyBusiness;
        public CompanyController(ICompanyBusiness companyBusiness, IMapper mapper)
        {
            _mapper = mapper;
            _companyBusiness = companyBusiness;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CompanyVM companyAdd)
        {
            if (ModelState.IsValid)
            {
                Company company = _mapper.Map<Company>(companyAdd);
                try
                {
                    _companyBusiness.Add(company);
                    TempData["companyMessage"] = "Kayit Basariyla Gerceklestirildi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["companyMessage"] = "Kayit Basarisiz";

                    throw;
                } 
            }
            
            return View(companyAdd);
        }
    }
}
