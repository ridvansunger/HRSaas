using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface IDepartmentBusiness
    {
        IEnumerable<Department> GetAll(Func<Department, bool> predicate = null);
        Task<Department> GetById(int id);
        Task<Department> Add(Department entity);
        Task<Department> Update(Department entity);
        Task<bool> Delete(int id);
    }
}
