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
    public class LeaveBusiness : ILeaveBusiness
    {
        ILeaveRepository _repo;
        public LeaveBusiness(ILeaveRepository repo)
        {
            _repo = repo;
        }
        public async Task<Leave> Add(Leave entity)
        {
            return await _repo.Add(entity);
        }

        public int DaysLeftFromAnnualLeave(int personalId)
        {
            return _repo.DaysLeftFromAnnualLeave(personalId);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<Leave> GetAll(Func<Leave, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }

        public async Task<Leave> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Leave> Update(Leave entity)
        {
            return await _repo.Update(entity);
        }
    }
}
