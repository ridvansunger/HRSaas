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
    public class AdvanceBusiness : IAdvanceBusiness
    {
        IAdvanceRepository _repo;
        public AdvanceBusiness(IAdvanceRepository repo)
        {
            _repo = repo;
        }
        public async Task<Advance> Add(Advance entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<Advance> GetAll(Func<Advance, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }

        public Task<Advance> GetById(int id)
        {
            return _repo.GetById(id);
        }

        public decimal GetTotalAdvanceReceivedMonthly(int personalId)
        {
            return _repo.GetTotalAdvanceReceivedMonthly(personalId);
        }

        public Task<Advance> Update(Advance entity)
        {
            return _repo.Update(entity);
        }
    }
}
