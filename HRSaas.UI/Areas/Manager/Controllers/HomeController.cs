using HRSaas.Business.Abstract;
using HRSaas.Business.Concrete;
using HRSaas.DAL.Concrete;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

namespace HRSaas.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private IPersonalBusiness _personalBusiness;
        private IDepartmentBusiness _departmentBusiness;
        private ITitleBusiness _titleBusiness;
        private IAdvanceBusiness _advanceBusiness;
        private ILeaveBusiness _leaveBusiness;
        private IExpenditureBusiness _expenditureBusiness;
        private IPackageBusiness _packageBusiness;
        private ICompanyBusiness _companyBusiness;
        private readonly SignInManager<Personal> _signInManager;

        private UserManager<Personal> _userManager;
        public HomeController(IPersonalBusiness personalBusiness, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, IAdvanceBusiness advanceBusiness, ILeaveBusiness leaveBusiness, IExpenditureBusiness expenditureBusiness, UserManager<Personal> userManager, SignInManager<Personal> signInManager, IPackageBusiness packageBusiness, IMapper mapper,ICompanyBusiness companyBusiness)
        {
            _companyBusiness = companyBusiness;
            _mapper = mapper;
            _userManager = userManager;
            _packageBusiness = packageBusiness;
            _personalBusiness = personalBusiness;
            _departmentBusiness = departmentBusiness;
            _titleBusiness = titleBusiness;
            _advanceBusiness = advanceBusiness;
            _leaveBusiness = leaveBusiness;
            _signInManager = signInManager;
            _expenditureBusiness = expenditureBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
            int? companyId = loggedInPersonal.CompanyId;
            Company company = await _companyBusiness.GetById((int)companyId);
            int? packageId = company.PackageId;
            Package package=null;
            PackageVM packageVM=null;
            if (packageId!=null)
            {
                 package =await _packageBusiness.GetById((int)packageId);
                 packageVM = _mapper.Map<PackageVM>(package);
            }
             

            var listOfPersonal = _personalBusiness.GetAll(t0 => t0.CompanyId == loggedInPersonal.CompanyId);
            List<PersonalDetailVm> personals = new List<PersonalDetailVm>();

            foreach (var item in listOfPersonal)
            {
                Department dept = await _departmentBusiness.GetById(Convert.ToInt32(item.DepartmentId));
                Title title = await _titleBusiness.GetById(Convert.ToInt32(item.TitleId));
                PersonalDetailVm displayVM = new PersonalDetailVm()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EndDate = item.EndDate,
                    DepertmentName = dept.DepartmentName,
                    TitleName = title.TitleName,
                    PersonalIsActive = (bool)item.PersonalIsActive,
                    Mail = item.Mail,
                    Phone = item.Phone,
                    Photo = item.Photo,
                    StartDate = (DateTime)item.StartDate,
                    Tc = item.Tc,

                };

                personals.Add(displayVM);
            }

            TimeSpan ts = DateTime.Now-loggedInPersonal.Company.PackageStartDate;
            ViewBag.RemaningTime = packageVM.DurationTime- Convert.ToInt32(ts.Days.ToString());

            //avansları çek
            var advanceList = _advanceBusiness.GetAll(t0 => t0.ApprovalStatus == false && t0.SeenByManager == false).OrderByDescending(t0 => t0.Id).Take(10);

            List<AdvanceVM> advances = new List<AdvanceVM>();
            foreach (var item in advanceList)
            {
                Personal per = await _personalBusiness.GetById(Convert.ToInt32(item.PersonalId));
                //Department dept = await _departmentBusiness.GetById(Convert.ToInt32(item.Id));
                AdvanceVM advanceVM = new AdvanceVM()
                {
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    NameAndSurname = per.FirstName + " " + per.LastName,
                    DepartmentName = per.Department.DepartmentName,
                    AdvanceAmount = item.AdvanceAmount,
                    ApprovalDate = item.ApprovalDate,
                    ApprovalStatus = item.ApprovalStatus,
                    Comment = item.Comment,
                    RequestDate = item.RequestDate

                };
                advances.Add(advanceVM);
            }

            //izinleri çek
            var leaveList = _leaveBusiness.GetAll(t0 => t0.ApprovalStatus == false && t0.SeenByManager == false).OrderByDescending(t0 => t0.Id).Take(10);
            List<LeaveVM> leaves = new List<LeaveVM>();
            foreach (var item in leaveList)
            {
                Personal per = await _personalBusiness.GetById(Convert.ToInt32(item.PersonalId));
                LeaveVM leaveVM = new LeaveVM()
                {
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    NameAndSurname = per.FirstName + " " + per.LastName,
                    DepartmentName = per.Department.DepartmentName,
                    ApprovalDate = item.ApprovalDate,
                    ApprovalStatus = item.ApprovalStatus,
                    Comment = item.Comment,
                    LeaveEndDate = item.LeaveEndDate,
                    LeaveStartDate = item.LeaveStartDate,
                    LeaveTypeId = item.LeaveTypeId


                };

                leaves.Add(leaveVM);
            }

            //MAsrafları çek
            var expendList = _expenditureBusiness.GetAll(t0 => t0.Status == false && t0.SeenByManager == false).OrderByDescending(t0 => t0.Id).Take(10);
            List<PersonalExpenditureVM> expenditures = new List<PersonalExpenditureVM>();
            foreach (var item in expendList)
            {

                Personal per = await _personalBusiness.GetById(Convert.ToInt32(item.PersonalId));
                PersonalExpenditureVM perExVM = new PersonalExpenditureVM()
                {
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    ApprovalDate = item.ApprovalDate,
                    Attachment = item.Attachment,
                    DepartmentName = item.Personal.Department.DepartmentName,
                    ExDescription = item.ExDescription,
                    ExpenditureAmount = item.ExpenditureAmount,
                    ExpenditureDate = item.ExpenditureDate,
                    NameAndSurname = per.FirstName + " " + per.LastName,
                    RequestDate = item.RequestDate,
                    SeenByManager = item.SeenByManager,
                    Status = item.Status,
                    TitleName = per.Title.TitleName
                };
                expenditures.Add(perExVM);
            }


            return View(Tuple.Create<IEnumerable<PersonalDetailVm>, IEnumerable<AdvanceVM>, IEnumerable<LeaveVM>, IEnumerable<PersonalExpenditureVM>,PackageVM>(personals, advances, leaves, expenditures, packageVM));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/Login/Login");
        }
    }
}
