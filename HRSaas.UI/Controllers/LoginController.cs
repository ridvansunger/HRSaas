using AutoMapper;
using HRSaas.Business.Concrete;
using HRSaas.Core.UI.Authorize;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;

using HRSaas.UI.Areas.Manager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace HRSaas.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<Personal> _userManager;
        private readonly SignInManager<Personal> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public LoginController(UserManager<Personal> userManager, SignInManager<Personal> signInManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Personal eklenenuser = new Personal
            //{
            //    AddressId = 1,
            //    AnnualLeave = 1,
            //    Gender = true,
            //    Password = "1",
            //    FirstName = "deneme",
            //    LastName = "deneme",
            //    Tc = "111",
            //    Mail = "deneme@gmail",
            //    Phone = "4554",
            //    Photo = "DefaultMaleImage.png",
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    TitleId = 1,
            //    PersonalIsActive = true,
            //    DepartmentId = 1,
            //    BirthDate = DateTime.Now,
            //    Salary = 100,
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 3,
            //    UserName = "manager",
            //    Email = "manager@manager",
            //    IsActivated = false
            //};
            //IdentityResult result = await _userManager.CreateAsync(eklenenuser, "123Pass_");
            //await _roleManager.CreateAsync(new AppRole { Name = "SiteManager" });
            //var gelenuser = await _userManager.FindByEmailAsync("salih@gmail");
            //var result1 = await _userManager.AddToRoleAsync(gelenuser, "Admin1");

            //Personal eklenenUser1 = new Personal
            //{
            //    AddressId = 1,
            //    AnnualLeave = 1,
            //    Gender = true,
            //    Password = "1",
            //    FirstName = "deneme1",
            //    LastName = "deneme",
            //    Tc = "111",
            //    Mail = "sitemanager@gmail.com",
            //    Phone = "4554",
            //    Photo = "DefaultMaleImage.png",
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    TitleId = 1,
            //    PersonalIsActive = true,
            //    DepartmentId = 1,
            //    BirthDate = DateTime.Now,
            //    Salary = 100,
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 3,
            //    UserName = "sitemanager@gmail.com",
            //    Email = "sitemanager@gmail.com",
            //    IsActivated = false
            //};
            //IdentityResult result2 = await _userManager.CreateAsync(eklenenUser1, "1");

            ////await _roleManager.CreateAsync(new AppRole { Name = "Admin1" });

            //var gelenuser2 = await _userManager.FindByEmailAsync("sitemanager@gmail.com");
            //var result3 = await _userManager.AddToRoleAsync(gelenuser2, "SiteManager");

            //Personal eklenenUser2 = new Personal
            //{
            //    AddressId = 1,
            //    AnnualLeave = 1,
            //    Gender = true,
            //    Password = "1",
            //    FirstName = "deneme1",
            //    LastName = "deneme",
            //    Tc = "111",
            //    Mail = "manager@gmail.com",
            //    Phone = "4554",
            //    Photo = "DefaultMaleImage.png",
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    TitleId = 1,
            //    PersonalIsActive = true,
            //    DepartmentId = 1,
            //    BirthDate = DateTime.Now,
            //    Salary = 100,
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 3,
            //    UserName = "manager@gmail.com",
            //    Email = "manager@gmail.com",
            //    IsActivated = false
            //};
            //IdentityResult result5 = await _userManager.CreateAsync(eklenenUser2, "1");

            ////await _roleManager.CreateAsync(new AppRole { Name = "Admin1" });

            //var gelenuser3 = await _userManager.FindByEmailAsync("manager@gmail.com");
            ////var result4 = await _userManager.AddToRoleAsync(gelenuser3, "Manager");
            //Personal eklenenUser4 = new Personal
            //{
            //    AddressId = 1,
            //    AnnualLeave = 1,
            //    Gender = true,
            //    Password = "1",
            //    FirstName = "deneme1",
            //    LastName = "deneme",
            //    Tc = "111",
            //    Mail = "employee@gmail.com",
            //    Phone = "4554",
            //    Photo = "DefaultMaleImage.png",
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    TitleId = 1,
            //    PersonalIsActive = true,
            //    DepartmentId = 1,
            //    BirthDate = DateTime.Now,
            //    Salary = 100,
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 3,
            //    UserName = "employee@gmail.com",
            //    Email = "employee@gmail.com",
            //    IsActivated = false
            //};
            //IdentityResult result6 = await _userManager.CreateAsync(eklenenUser4, "1");

            ////await _roleManager.CreateAsync(new AppRole { Name = "Admin1" });

            //var gelenuser4 = await _userManager.FindByEmailAsync("employee@gmail.com");
            //var result7 = await _userManager.AddToRoleAsync(gelenuser4, "Employee");
            //if (result2.Succeeded)
            //{

            //}


            //if (result3.Succeeded)
            //{

            //}
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginUser)
        {
            if (ModelState.IsValid)
            {
                Personal personal = await _userManager.FindByEmailAsync(loginUser.Email);
                //await _userManager.AddToRoleAsync(personal, "Admin");
                if (personal != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(personal, loginUser.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        if (personal.IsActivated)
                        {
                            var userRole = await _userManager.GetRolesAsync(personal);
                            if (userRole.Contains("Manager"))
                                return RedirectToAction("Index", "Home", new { area = "Manager" });
                            else if (userRole.Contains("Employee"))
                                return RedirectToAction("Index", "Home", new { area = "Employee" });
                            else if (userRole.Contains("SiteManager"))
                                return RedirectToAction("Index", "Home", new { area = "SiteManager" });
                            //else
                            //    return RedirectToAction("Login");
                        }
                        else
                        {
                            loginUser.FirstSingin = true;
                            ViewBag.IsPassive = "true";
                            return View(loginUser);
                        }
                    }
                    else
                    {
                        //buraya şifre hatası
                        ViewBag.PasswordAlert = "Girdiginiz sifre kullanici verileriyle uyusmuyor.Email ve sifreyi kontrol ederek tekrar giriniz.";
                        return View(loginUser);
                    }
                }
                else
                {
                    ViewBag.UserAlert = "Kullanici bulunamadi. Giris parametrelerini dikkat ediniz.";
                    return View(loginUser);
                }
            }
            else
            {
                if (loginUser.FirstSingin)
                    return View(loginUser);
            }
            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(LoginVM loginUser)
        {
            if (ModelState.IsValid)
            {
                var findedUser = await _userManager.FindByEmailAsync(loginUser.Email);
                if (findedUser != null)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(findedUser);
                    findedUser.IsActivated = true;
                    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(findedUser, resetToken, loginUser.Password);

                    if (passwordChangeResult.Succeeded)
                    {
                        ViewBag.IsPassive = "false";
                        return RedirectToAction("Login");
                    }
                }
                else
                    return View();
            }
            else
            {
                loginUser.FirstSingin = true;
                ViewBag.IsPassive = "true";
            }
            return View(loginUser);
        }
    }
}
