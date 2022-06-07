using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface ILeaveBusiness
    {
        IEnumerable<Leave> GetAll(Func<Leave, bool> predicate = null);
        Task<Leave> GetById(int id);
        Task<Leave> Add(Leave entity);
        Task<Leave> Update(Leave entity);
        Task<bool> Delete(int id);
        int DaysLeftFromAnnualLeave(int personalId);
    }
}
