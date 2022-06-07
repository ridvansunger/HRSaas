using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Core.Helpers;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class PersonalBusiness : IPersonalBusiness
    {
        private UserManager<Personal> _userManager { get;  }
        private SignInManager<Personal> _roleManager { get;  }
        private IPersonalRepository _personalRepo;
        private IMapper _mapper;
        public PersonalBusiness(IPersonalRepository repo, IMapper mapper, UserManager<Personal> userManager,SignInManager<Personal> signInManager)
        {
            _userManager = userManager;
            _roleManager = signInManager;
            _personalRepo = repo;
            _mapper = mapper;
        }
        public async Task<Personal> Add(Personal entity)
        {
            entity.UserName = entity.Mail;
            //entity.EmailConfirmed = true;
            //entity.PhoneNumberConfirmed = true;
            //entity.TwoFactorEnabled = true;
            //entity.LockoutEnabled = true;
            //entity.AccessFailedCount = 3;
            entity.Email = entity.Mail;
            IdentityResult result = await _userManager.CreateAsync(entity);
            var result1 = await _userManager.AddToRoleAsync(entity, "Employee");
            if (result1.Succeeded && result.Succeeded)
            {
                MailSender.Send(entity.Mail);
            }
            return entity;
        }
        public async Task<Personal> Update(Personal entity)
        {
            IdentityResult result = await _userManager.UpdateAsync(entity);
            return entity;
        }
        public async Task<bool> Delete(int id)
        {
            return await _personalRepo.Delete(id);
        }
        public IEnumerable<Personal> GetAll(Func<Personal, bool> predicate=null)
        {
            return _personalRepo.GetAll(predicate) ;
        }

        public async Task<Personal> GetById(int id)
        {
            return await _personalRepo.GetById(id);
        }

        public async Task<Personal> AddManager(Personal entity)
        {

            entity.UserName = entity.Mail;
            entity.Email = entity.Mail;
            IdentityResult result = await _userManager.CreateAsync(entity);
            var result1 = await _userManager.AddToRoleAsync(entity, "Admin");
            if (result1.Succeeded && result.Succeeded)
            {
                MailSender.Send(entity.Mail);
            }
            return entity;
        }
    }
}
