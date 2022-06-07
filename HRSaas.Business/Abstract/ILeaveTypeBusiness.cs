using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface ILeaveTypeBusiness
    {
        IEnumerable<LeaveType> GetAll(Func<LeaveType, bool> predicate = null);
        Task<LeaveType> GetById(int id);
        Task<LeaveType> Add(LeaveType entity);
        Task<LeaveType> Update(LeaveType entity);
        Task<bool> Delete(int id);
    }
}
