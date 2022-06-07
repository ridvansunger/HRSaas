using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Business.ValidationRules;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRSaas.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private ILeaveTypeBusiness _leaveTypeBusiness;
        private ILeaveBusiness _leaveBusiness;
        private IAdvanceBusiness _advanceBusiness;
        private IPersonalBusiness _personalBusiness;
        private IAddressBusiness _addressBusiness;
        private UserManager<Personal> _userManager;
        private SignInManager<Personal> _signInManager;
        private IExpenditureBusiness _expenditureBusiness;


        public HomeController(ILeaveBusiness leaveBusiness, ILeaveTypeBusiness leaveTypeBusiness, IMapper mapper, IAdvanceBusiness advanceBusiness, IPersonalBusiness personalBusiness, IAddressBusiness addressBusiness, UserManager<Personal> userManager, SignInManager<Personal> signInManager, IExpenditureBusiness expenditureBusiness)
        {
            _userManager = userManager;
            _leaveTypeBusiness = leaveTypeBusiness;
            _leaveBusiness = leaveBusiness;
            _mapper = mapper;
            _advanceBusiness = advanceBusiness;
            _personalBusiness = personalBusiness;
            _addressBusiness = addressBusiness;
            _signInManager = signInManager;
            _expenditureBusiness = expenditureBusiness;
        }
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
            
            ViewBag.UserPhoto = loggedInPersonal.Photo;
            ViewBag.UserName = loggedInPersonal.FirstName + " " + loggedInPersonal.LastName;
            var listOfLeave = _leaveBusiness.GetAll(t0=>t0.PersonalId==loggedInPersonal.Id).Take(5);
            List<LeaveVM> leaves = new List<LeaveVM>();

            PersonalDetailVm perVM = _mapper.Map<PersonalDetailVm>(loggedInPersonal);

            var adress = await _addressBusiness.GetById(perVM.AddressId);
            perVM.AddressName = adress.AddressName;                

            foreach (var item in listOfLeave)
            {
                LeaveType lev = await _leaveTypeBusiness.GetById(Convert.ToInt32(item.LeaveTypeId));
                LeaveVM leaveVM = new LeaveVM()
                {
                    LeaveTypeName = lev.LeaveTypeName,
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    LeaveTypeId = item.LeaveTypeId,
                    ApprovalStatus = item.ApprovalStatus,
                    LeaveStartDate = item.LeaveStartDate,
                    LeaveEndDate = item.LeaveEndDate,
                    ApprovalDate = item.ApprovalDate,
                    RequestDate = item.RequestDate,
                    Comment = item.Comment
                };
                leaves.Add(leaveVM);
            }

            var listOfAdvance = _advanceBusiness.GetAll(t0 => t0.PersonalId == loggedInPersonal.Id).Take(5);
            List<AdvanceVM> advances = new List<AdvanceVM>();
            foreach (var item in listOfAdvance)
            {

                AdvanceVM advanceVM = new AdvanceVM()
                {
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    ApprovalStatus = item.ApprovalStatus,
                    AdvanceAmount = item.AdvanceAmount,
                    RequestDate = item.RequestDate,
                    ApprovalDate = item.ApprovalDate,
                    Comment = item.Comment,

                };
                advances.Add(advanceVM);

            }

            var listofExpenditure = _expenditureBusiness.GetAll(t0=>t0.PersonalId== loggedInPersonal.Id);
            List<PersonalExpenditureVM> listOfRetExp = new List<PersonalExpenditureVM>();
            if (listofExpenditure.Any())
                foreach (var item in listofExpenditure)
                    listOfRetExp.Add(_mapper.Map<PersonalExpenditureVM>(item)); 


            //return View(leaves);
            return View(Tuple.Create<IEnumerable<LeaveVM>, IEnumerable<AdvanceVM>, PersonalDetailVm, IEnumerable<PersonalExpenditureVM>>(leaves, advances, perVM,listOfRetExp));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/Login/Login");
        }
    }
}
