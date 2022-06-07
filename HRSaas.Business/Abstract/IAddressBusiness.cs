using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface IAddressBusiness
    {
        IEnumerable<Address> GetAll(Func<Address, bool> predicate = null);
        Task<Address> GetById(int id);
        Task<Address> Add(Address entity);
        Task<Address> Update(Address entity);
        Task<bool> Delete(int id);
    }
}
