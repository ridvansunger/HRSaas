using HRSaas.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRSaas.Business.ValidationRules;
using HRSaas.Entities.Dtos;
using FluentValidation.Results;
using HRSaas.Entities.Concrete;
//using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HRSaas.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class LeaveController : Controller
    {
        private IMapper _mapper;
        private ILeaveTypeBusiness _leaveTypeBusiness;
        private ILeaveBusiness _leaveBusiness;
        private UserManager<Personal> _userManager;

        public LeaveController(ILeaveBusiness leaveBusiness, ILeaveTypeBusiness leaveTypeBusiness, IMapper mapper, UserManager<Personal> userManager)
        {
            _userManager = userManager;
            _leaveTypeBusiness = leaveTypeBusiness;
            _leaveBusiness = leaveBusiness;
            _mapper = mapper;
        }

        [HttpGet]
        public async  Task<IActionResult> LeaveAdd()
        {
            await FillProfileSummary();
            FillLeaveIndexView();
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
            LeaveVM leaveVM = new LeaveVM()
            {PersonalId=loggedInPersonal.Id,LeaveStartDate=DateTime.Now,LeaveEndDate=DateTime.Now};
            return View(leaveVM);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveAdd(LeaveVM leaveData)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);
                var leave = _mapper.Map<Leave>(leaveData);
                if (leaveData.Id == 0)
                {
                    leave.ApprovalStatus = false;
                    leave.SeenByManager = false;
                    leave.PersonalId = loggedInPersonal.Id;
                    leave.RequestDate = DateTime.Now;
                    if (await _leaveBusiness.Add(leave) != null)
                        return RedirectToAction("Index","Home");
                }
                //else
                //{
                //    leave.PersonalId = loggedInPersonal.Id;
                //    leave.RequestDate = DateTime.Now;
                //    if (await _leaveBusiness.Update(leave)!=null)
                //        return RedirectToAction("Index", "Home");
                //}
            }
            else
            { 
                FillLeaveIndexView();
                await FillProfileSummary();
            }
            return View(leaveData);
        }

        private void FillLeaveIndexView()
        {
            var LeaveTypes = _leaveTypeBusiness.GetAll().ToList();
            List<SelectListItem> leaveTypeSelectListItems = new List<SelectListItem>();
            foreach (var item in LeaveTypes)
            {
                leaveTypeSelectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.LeaveTypeName
                });
            }
            ViewBag.LeaveTypes = new SelectList(leaveTypeSelectListItems, "Value", "Text");
        }

        private async Task FillProfileSummary()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            ViewBag.UserPhoto = loggedInPersonal.Photo;
            ViewBag.UserName = loggedInPersonal.FirstName + " " + loggedInPersonal.LastName;
        }
    }
}