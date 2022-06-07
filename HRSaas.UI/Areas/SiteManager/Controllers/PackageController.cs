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
    public class PackageController : Controller
    {
        IMapper _mapper;
        IPackageBusiness _packageBusiness;
        public PackageController(IPackageBusiness packageBusiness, IMapper mapper)
        {
            _mapper = mapper;
            _packageBusiness = packageBusiness;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(PackageVM packageAdd)
        {
            if (ModelState.IsValid)
            {
                Package company = _mapper.Map<Package>(packageAdd);
                try
                {
                    _packageBusiness.Add(company);
                    TempData["packageMessage"] = "Kayit Basariyla Gerceklestirildi";
                }
                catch (Exception)
                {
                    TempData["packageMessage"] = "Kayit Basarisiz";

                    throw;
                } 
            }

            return View(packageAdd);
        }
    }
}
