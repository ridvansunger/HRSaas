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
    public class AdvanceController : Controller
    {
        IAdvanceBusiness _advanceBusiness;
        IPersonalBusiness _personalBusiness;
        IDepartmentBusiness _departmentBusiness;
        ITitleBusiness _titleBusiness;
        IMapper _mapper;
        public AdvanceController(IAdvanceBusiness advanceBusiness, IPersonalBusiness personalBusiness, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, IMapper mapper)
        {
            _advanceBusiness = advanceBusiness;
            _personalBusiness = personalBusiness;
            _departmentBusiness = departmentBusiness;
            _titleBusiness = titleBusiness;
            _mapper = mapper;
        }


        //Onay ekranı için yazıldı
        public async Task<IActionResult> Index(int id)
        {
            //Personal personalDto = await _personalBusiness.GetById(id);
            //PersonalDetailVm personalDetailVm = _mapper.Map<PersonalDetailVm>(p);

            Advance advance = await _advanceBusiness.GetById(id);
            Personal personalVm = await _personalBusiness.GetById(advance.PersonalId);
            Department department = await _departmentBusiness.GetById((int)personalVm.DepartmentId);
            Title title = await _titleBusiness.GetById((int)personalVm.TitleId);
            AdvanceVM advanceVM = _mapper.Map<AdvanceVM>(advance);

            advanceVM.NameAndSurname = personalVm.FirstName + " " + personalVm.LastName;
            advanceVM.DepartmentName = department.DepartmentName;
            advanceVM.Salary = (decimal)personalVm.Salary;
            advanceVM.TitleName = title.TitleName;

            return View(advanceVM);
        }


        //Onay ekranı için yazıldı
        //[HttpGet("Manager/Personal/PersonalAdd/{id}")]
        [HttpGet("Manager/Advance/AdvanceAccept/{id}")]
        public async Task<IActionResult> AdvanceAccept(int id)
        {
            var advanceValue = await _advanceBusiness.GetById(id);
            advanceValue.ApprovalStatus = true;
            advanceValue.SeenByManager = true;
            advanceValue.ApprovalDate = DateTime.Now;
            await _advanceBusiness.Update(advanceValue);
            return RedirectToAction("Index", "Home");
        }

        //Onay ekranı için yazıldı
        [HttpGet("Manager/Advance/AdvanceReject/{id}")]
        public async Task<IActionResult> AdvanceReject(int id)
        {
            var advanceValue = await _advanceBusiness.GetById(id);
            advanceValue.ApprovalStatus = false;
            advanceValue.SeenByManager = true;
            advanceValue.ApprovalDate = DateTime.Now;
            await _advanceBusiness.Update(advanceValue);
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> AllAdvanceList()
        {
            var valuesAdvances = _advanceBusiness.GetAll();
            List<AdvanceVM> advanceVMList = new List<AdvanceVM>();
            // Advance advance = _mapper.Map<AdvanceVM>();
            foreach (var item in valuesAdvances)
            {
                Advance advance = await _advanceBusiness.GetById(Convert.ToInt32(item.Id));
                Personal personal = await _personalBusiness.GetById(Convert.ToInt32(item.PersonalId));
                Department dept = await _departmentBusiness.GetById(Convert.ToInt32(personal.DepartmentId));
                Title title = await _titleBusiness.GetById(Convert.ToInt32(personal.TitleId));

                AdvanceVM advanceVM = new AdvanceVM()
                {
                    NameAndSurname = personal.FirstName + " " + personal.LastName,
                    TitleName = title.TitleName,
                    DepartmentName = dept.DepartmentName,
                    ApprovalStatus = item.ApprovalStatus,
                    AdvanceAmount = item.AdvanceAmount,
                    Comment = item.Comment,
                    ApprovalDate = item.ApprovalDate,
                    RequestDate = item.RequestDate,
                    Salary = (decimal)personal.Salary,
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    SeenByManager = item.SeenByManager
                };
                advanceVMList.Add(advanceVM);
            }
            return View(advanceVMList);
        }

    }
}
