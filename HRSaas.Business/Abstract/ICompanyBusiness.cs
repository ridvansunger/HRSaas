using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface ICompanyBusiness
    {
        IEnumerable<Company> GetAll(Func<Company, bool> predicate = null);
        Task<Company> GetById(int id);
        Task<Company> Add(Company entity);
        Task<Company> Update(Company entity);
        Task<bool> Delete(int id);
    }
}
