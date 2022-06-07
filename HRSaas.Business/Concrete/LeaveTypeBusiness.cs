using HRSaas.Business.Abstract;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class LeaveTypeBusiness : ILeaveTypeBusiness
    {
        ILeaveTypeRepository _repo;
        public LeaveTypeBusiness(ILeaveTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<LeaveType> Add(LeaveType entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<LeaveType> GetAll(Func<LeaveType, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }

        public async Task<LeaveType> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<LeaveType> Update(LeaveType entity)
        {
            return await _repo.Update(entity);
        }
    }
}
