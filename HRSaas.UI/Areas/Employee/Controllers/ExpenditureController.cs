using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRSaas.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ExpenditureController : Controller
    {
        private UserManager<Personal> _userManager;
        private IExpenditureBusiness _expenditure;
        private IMapper _mapper;
        private IHostingEnvironment _env;
        string directory;
        public ExpenditureController(UserManager<Personal> userManager, IExpenditureBusiness expenditure, IMapper mapper, IHostingEnvironment env)
        {
            _userManager = userManager;
            _expenditure = expenditure;
            _mapper = mapper;
            _env = env;
            directory = _env.ContentRootPath + "/wwwroot/Data/CompanyFiles/";
        }

        [HttpGet]
        public async Task<IActionResult> ExpenditureAdd()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            Personal loggedInPersonal = await _userManager.FindByEmailAsync(userEmail);

            ViewBag.UserPhoto = loggedInPersonal.Photo;
            ViewBag.UserName = loggedInPersonal.FirstName + " " + loggedInPersonal.LastName;
            PersonalExpenditureVM expenditureVM = new PersonalExpenditureVM() {
                PersonalId = loggedInPersonal.Id
            };
            return View(expenditureVM);
        }

        [HttpPost]
        public async Task<IActionResult> ExpenditureAdd(PersonalExpenditureVM pExp)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                pExp.ExpenditureAmount = (decimal)pExp.ExpenditureAmount;
                var expenditure = _mapper.Map<Expenditure>(pExp);
                string fileName = _expenditure.GetForFileNameAsync(userEmail) + "-" + pExp.ExDescription + "-" + ((DateTime)pExp.ExpenditureDate).ToString("d") + "." + pExp.File.FileName.Split(".")[1];
                fileName = FileSave(pExp.File, fileName);
                expenditure.Attachment = fileName;
                expenditure.PersonalId = _userManager.FindByEmailAsync(userEmail).Result.Id;

                if (await _expenditure.Add(expenditure) != null)
                    return RedirectToAction("Index", "Home");
            }
            return View(pExp);
        }

        private string FileSave(IFormFile file, string fileName)
        {
            if (file != null)
                using (var fileStream = new FileStream(Path.Combine(directory, fileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            return fileName;
        }
    }
}
