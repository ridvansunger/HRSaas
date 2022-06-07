using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface IPackageBusiness
    {
        IEnumerable<Package> GetAll(Func<Package, bool> predicate = null);
        Task<Package> GetById(int id);
        Task<Package> Add(Package entity);
        Task<Package> Update(Package entity);
        Task<bool> Delete(int id);
    }
}
