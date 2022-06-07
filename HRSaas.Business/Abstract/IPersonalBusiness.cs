using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface IPersonalBusiness
    {
        IEnumerable<Personal> GetAll(Func<Personal, bool> predicate = null);
        Task<Personal> GetById(int id);
        Task<Personal> Add(Personal entity);
        Task<Personal> AddManager(Personal entity);
        Task<Personal> Update(Personal entity); 
        Task<bool> Delete(int id);
    }
}
