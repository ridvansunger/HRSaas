using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface IAdvanceBusiness
    {
        IEnumerable<Advance> GetAll(Func<Advance, bool> predicate = null);
        Task<Advance> GetById(int id);
        Task<Advance> Add(Advance entity);
        Task<Advance> Update(Advance entity);
        Task<bool> Delete(int id);
        decimal GetTotalAdvanceReceivedMonthly(int personalId);
    }
}
