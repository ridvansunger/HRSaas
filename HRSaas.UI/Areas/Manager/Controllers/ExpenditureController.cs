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
    public class ExpenditureController : Controller
    {
        IExpenditureBusiness _expenditureBusiness;
        IPersonalBusiness _personalBusiness;
        IDepartmentBusiness _departmentBusiness;
        ITitleBusiness _titleBusiness;
        IMapper _mapper;

        public ExpenditureController(IExpenditureBusiness expenditureBusiness, IPersonalBusiness personalBusiness, IDepartmentBusiness departmentBusiness, ITitleBusiness titleBusiness, IMapper mapper)
        {
            _expenditureBusiness = expenditureBusiness;
            _personalBusiness = personalBusiness;
            _departmentBusiness = departmentBusiness;
            _titleBusiness = titleBusiness;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index(int id)
        {
            Expenditure expenditure = await _expenditureBusiness.GetById(id);
            Personal personal = await _personalBusiness.GetById((int)expenditure.PersonalId);
            Department department = await _departmentBusiness.GetById((int)personal.DepartmentId);
            Title title = await _titleBusiness.GetById((int)personal.TitleId);

            PersonalExpenditureVM perExpendVM = _mapper.Map<PersonalExpenditureVM>(expenditure);
            perExpendVM.NameAndSurname = personal.FirstName + " " + personal.LastName;
            perExpendVM.DepartmentName = department.DepartmentName;
            perExpendVM.TitleName = title.TitleName;
            perExpendVM.PersonalId = personal.Id;

            return View(perExpendVM);
        }


        //Onay ekranı için yazıldı
        [HttpGet("Manager/Expenditure/ExpenditureAccept/{id}")]
        public async Task<IActionResult> ExpenditureAccept(int id)
        {
            var expenditureValue = await _expenditureBusiness.GetById(id);
            expenditureValue.Status = true;
            expenditureValue.SeenByManager = true;
            expenditureValue.ApprovalDate = DateTime.Now;
            await _expenditureBusiness.Update(expenditureValue);
            return RedirectToAction("Index", "Home");
        }

        //Onay ekranı için yazıldı
        [HttpGet("Manager/Expenditure/ExpenditureReject/{id}")]
        public async Task<IActionResult> ExpenditureReject(int id)
        {
            var expenditureValue = await _expenditureBusiness.GetById(id);
            expenditureValue.Status = false;
            expenditureValue.SeenByManager = true;
            expenditureValue.ApprovalDate = DateTime.Now;
            await _expenditureBusiness.Update(expenditureValue);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> AllExpenditureList()
        {
            var valuesExpenditure = _expenditureBusiness.GetAll();
            List<PersonalExpenditureVM> perExVMList = new List<PersonalExpenditureVM>();
            foreach (var item in valuesExpenditure)
            {
                Expenditure expenditure = await _expenditureBusiness.GetById(item.Id);
                Personal personal = await _personalBusiness.GetById(item.PersonalId);
                Department dept = await _departmentBusiness.GetById(Convert.ToInt32(personal.DepartmentId));
                Title title = await _titleBusiness.GetById(Convert.ToInt32(personal.TitleId));


                PersonalExpenditureVM perExVM = new PersonalExpenditureVM()
                {
                    NameAndSurname = personal.FirstName + " " + personal.LastName,
                    TitleName = title.TitleName,
                    DepartmentName = dept.DepartmentName,
                    Status = item.Status,
                    ApprovalDate = item.ApprovalDate,
                    RequestDate = item.RequestDate,
                    Id = item.Id,
                    PersonalId = item.PersonalId,
                    SeenByManager = item.SeenByManager,
                    Attachment = item.Attachment,
                    ExDescription = item.ExDescription,
                    ExpenditureAmount = item.ExpenditureAmount,
                    ExpenditureDate = item.ExpenditureDate
                };

                perExVMList.Add(perExVM);
            }
            return View(perExVMList);
        }

    }
}
