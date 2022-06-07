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
    public class ExpenditureBusiness : IExpenditureBusiness
    {
        IExpenditureRepository _repo;

        public ExpenditureBusiness(IExpenditureRepository repo)
        {
            _repo = repo;
        }

        public async Task<Expenditure> Add(Expenditure entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<Expenditure> GetAll(Func<Expenditure, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }

        public async Task<Expenditure> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public string GetForFileNameAsync(string userMail)
        {
            return _repo.GetForFileName(userMail);
        }

        public async Task<Expenditure> Update(Expenditure entity)
        {
            return await _repo.Update(entity);
        }
    }
}
