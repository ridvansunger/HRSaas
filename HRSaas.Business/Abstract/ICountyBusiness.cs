using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface ICountyBusiness
    {
        IEnumerable<County> GetAll(Func<County, bool> predicate = null);
        Task<County> GetById(int id);
        Task<County> Add(County entity);
        Task<County> Update(County entity);
        Task<bool> Delete(int id);
    }
}
