using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using HRSaas.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace HRSaas.UI.Controllers
{
    public class HomeController : Controller
    {
        IPackageBusiness _packageBusiness;
        IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPackageBusiness packageBusiness, IMapper mapper)
        {
            _mapper = mapper;
            _packageBusiness = packageBusiness;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Package> packages = _packageBusiness.GetAll().Take(4).ToList();
            List<PackageVM> packageVMs = new List<PackageVM>();
            foreach (var package in packages)
            {
                PackageVM packageVM = _mapper.Map<PackageVM>(package);
                packageVMs.Add(packageVM);
            }
            return View(packageVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
