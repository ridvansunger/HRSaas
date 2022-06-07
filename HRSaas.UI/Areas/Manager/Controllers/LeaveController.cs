using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class LeaveController : Controller
    {

        ILeaveBusiness _leaveBusiness;
        IPersonalBusiness _personalBusiness;
        IDepartmentBusiness _departmentBusiness;
        ITitleBusiness _titleBusiness;
        IMapper _mapper;
        ILeaveTypeBusiness _leaveTypeBusiness;

        public LeaveController(ILeaveBusiness leaveBusiness, IPersonalBusiness personalBusiness, IMapper mapper, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, ILeaveTypeBusiness leaveTypeBusiness)
        {
            _leaveBusiness = leaveBusiness;
            _personalBusiness = personalBusiness;
            _mapper = mapper;
            _departmentBusiness = departmentBusiness;
            _titleBusiness = titleBusiness;
            _leaveTypeBusiness = leaveTypeBusiness;

        }

        //Onay ekranı için yazıldı
        public async Task<IActionResult> Index(int id)
        {
            Leave leave = await _leaveBusiness.GetById(id);
            Personal personal = await _personalBusiness.GetById(leave.PersonalId);
            Department department = await _departmentBusiness.GetById((int)personal.DepartmentId);
            Title title = await _titleBusiness.GetById((int)personal.TitleId);
            LeaveType leaveType = await _leaveTypeBusiness.GetById(leave.LeaveTypeId);

            LeaveVM leaveVM = _mapper.Map<LeaveVM>(leave);
            leaveVM.NameAndSurname = personal.FirstName + " " + personal.LastName;
            leaveVM.DepartmentName = department.DepartmentName;
            leaveVM.TitleName = title.TitleName;
            leaveVM.LeaveTypeName = leaveType.LeaveTypeName;


            //çalıştığı gün sayısı
            TimeSpan ts = (DateTime.Now) - ((DateTime)personal.StartDate);
            leaveVM.TotalWorkDayNumber = Convert.ToInt32(ts.Days.ToString());

            //kullandığı izin gün sayısı
            float usedDayOff = leaveVM.AnnualLeave - leaveVM.UsedDayOff;
            leaveVM.UsedDayOff = usedDayOff;

            return View(leaveVM);
        }


        //Onay ekranı için yazıldı
        [HttpGet("Manager/Leave/LeaveAccept/{id}")]
        public async Task<IActionResult> LeaveAccept(int id)
        {
            var leaveValue = await _leaveBusiness.GetById(id);
            leaveValue.SeenByManager = true;
            leaveValue.ApprovalStatus = true;
            leaveValue.ApprovalDate = DateTime.Now;
            await _leaveBusiness.Update(leaveValue);
            return RedirectToAction("Index", "Home");
        }

        //Onay ekranı için yazıldı
        [HttpGet("Manager/Leave/LeaveReject/{id}")]
        public async Task<IActionResult> LeaveReject(int id)
        {
            var leaveValue = await _leaveBusiness.GetById(id);
            leaveValue.ApprovalStatus = false;
            leaveValue.SeenByManager = true;
            leaveValue.ApprovalDate = DateTime.Now;
            await _leaveBusiness.Update(leaveValue);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> AllLeaveList()
        {
            var valuesLeaves = _leaveBusiness.GetAll();

            List<LeaveVM> leaveVMList = new List<LeaveVM>();

            foreach (var item in valuesLeaves)
            {
                Leave leave = await _leaveBusiness.GetById(Convert.ToInt32(item.Id));
                Personal personal = await _personalBusiness.GetById(Convert.ToInt32(item.PersonalId));
                Department dept = await _departmentBusiness.GetById(Convert.ToInt32(personal.DepartmentId));
                Title title = await _titleBusiness.GetById(Convert.ToInt32(personal.TitleId));
                LeaveType leaveType = await _leaveTypeBusiness.GetById(Convert.ToInt32(item.LeaveTypeId));

                LeaveVM leaveVM = new LeaveVM()
                {
                    NameAndSurname = personal.FirstName + " " + personal.LastName,
                    TitleName = title.TitleName,
                    DepartmentName = dept.DepartmentName,
                    ApprovalStatus = item.ApprovalStatus,
                    Comment = item.Comment,
                    ApprovalDate = item.ApprovalDate,
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    SeenByManager = item.SeenByManager,
                    LeaveStartDate = item.LeaveStartDate,
                    LeaveEndDate = item.LeaveEndDate,
                    LeaveTypeName=leaveType.LeaveTypeName
                    
                };

                leaveVMList.Add(leaveVM);

            }
            return View(leaveVMList);

        }
    }
}
