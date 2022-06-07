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
    public class PackageBusiness : IPackageBusiness
    {
        IPackageRepository _repo;
        public PackageBusiness(IPackageRepository repo)
        {
            _repo = repo;
        }

        public async Task<Package> Add(Package entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public IEnumerable<Package> GetAll(Func<Package, bool> predicate = null)
        {
            return _repo.GetAll(predicate);
        }


        public Task<Package> GetById(int id)
        {
            return _repo.GetById(id);
        }

        public Task<Package> Update(Package entity)
        {
            return _repo.Update(entity);
        }
    }
}
