using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.SiteManager.Controllers
{
    [Area("SiteManager")]
    [Authorize(Roles = "SiteManager")]
    public class HomeController : Controller
    {
        private readonly IPackageBusiness _packageBusiness;
        private readonly ICompanyBusiness _companyBusiness;
        private readonly IMapper _mapper;
        private UserManager<Personal> _userManager;

        public HomeController(IPackageBusiness packageBusiness, ICompanyBusiness companyBusiness, IMapper mapper, UserManager<Personal> userManager)
        {

            _userManager = userManager;
            
            _packageBusiness = packageBusiness;
            _companyBusiness = companyBusiness;
            _mapper = mapper;
            //@TempData["UserPhoto"]=
        }

        private async Task<Personal> findLogedInPersonal()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
            return loggedInPersonal;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CompanyVM> companyVMs= GetCompanyListAsync();
            List<PackageVM> packageVms =  GetPackageListAsync();
            
            return View(Tuple.Create< List<CompanyVM>, List<PackageVM>>(companyVMs, packageVms));
        }

        private List<PackageVM> GetPackageListAsync()
        {
            List<PackageVM> packageVMs = new List<PackageVM>();
            _packageBusiness.GetAll().ToList().ForEach(t0=> {
                packageVMs.Add(_mapper.Map<PackageVM>(t0));
            });

            return packageVMs;
        }

        private List<CompanyVM> GetCompanyListAsync()
        {
            List<CompanyVM> companyVMs = new List<CompanyVM>();
            _companyBusiness.GetAll().ToList().ForEach(t0=> {
                companyVMs.Add(_mapper.Map<CompanyVM>(t0));
            });

            companyVMs.ForEach(t0=> {
                t0.PackageName = _packageBusiness.GetById((int)t0.PackageId).Result.PackageName;
            });

            return companyVMs;
        }
    }
}
