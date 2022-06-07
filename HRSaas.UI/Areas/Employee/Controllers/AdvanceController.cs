using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class AdvanceController : Controller
    {
        private IMapper _mapper;
        private IAdvanceBusiness _advanceBusiness;
        private IPersonalBusiness _personalBusiness;
        private UserManager<Personal> _userManager;
        public AdvanceController(IAdvanceBusiness advanceBusiness, IMapper mapper,IPersonalBusiness personalBusiness, UserManager<Personal> userManager)
        {
            _userManager = userManager;
            _personalBusiness = personalBusiness;
            _advanceBusiness = advanceBusiness;
            _mapper = mapper;
            
        }
        [HttpGet]
        public async Task<IActionResult> AdvanceAdd()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            ViewBag.UserPhoto = loggedInPersonal.Photo;
            ViewBag.UserName = loggedInPersonal.FirstName + " " + loggedInPersonal.LastName;

            AdvanceVM advanceVM = new AdvanceVM();
            advanceVM.PersonalId = loggedInPersonal.Id;
            return View(advanceVM);
        }
        [HttpPost]
        public async Task<IActionResult> AdvanceAdd(AdvanceVM advanceData)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            if (ModelState.IsValid)
            {
                var advance = _mapper.Map<Advance>(advanceData);
                if (advanceData.Id == 0)
                {
                    advance.PersonalId = loggedInPersonal.Id;
                    advance.RequestDate = DateTime.Now;
                    advance.SeenByManager = false;
                    advance.ApprovalStatus = false;
                    if (await _advanceBusiness.Add(advance) == null)
                    {
                        ViewBag.Message = "Ekleme başarısız oldu!";
                    }
                    return RedirectToAction("Index","Home");
                }
            }
            return View(advanceData);
        }
    }
}
