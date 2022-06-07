using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
   public interface ICityBusiness
    {
        IEnumerable<City> GetAll(Func<City, bool> predicate = null);
        Task<City> GetById(int id);
        Task<City> Add(City entity);
        Task<City> Update(City entity);
        Task<bool> Delete(int id);
    }
}
