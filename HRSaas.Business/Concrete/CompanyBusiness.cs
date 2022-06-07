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
    public class CompanyBusiness:ICompanyBusiness
    {
        ICompanyRepository _repo;

        public CompanyBusiness(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Company> Add(Company entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<Company> GetAll(Func<Company, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }

        public async Task<Company> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Company> Update(Company entity)
        {
            return await _repo.Update(entity);
        }
    }
}
