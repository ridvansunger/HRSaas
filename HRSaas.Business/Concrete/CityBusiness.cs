using HRSaas.Business.Abstract;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class CityBusiness : ICityBusiness
    {
        private ICityRepository _cityRepo;
        public CityBusiness(ICityRepository repo)
        {
            _cityRepo = repo;
        }

        public async Task<City> Add(City entity)
        {
            return await _cityRepo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _cityRepo.Delete(id);
        }

        public IEnumerable<City> GetAll(Func<City, bool> predicate = null)
        {
            return _cityRepo.GetAll(predicate);
        }

        public async Task<City> GetById(int id)
        {
            return await _cityRepo.GetById(id);
        }

        public async Task<City> Update(City entity)
        {
            return await _cityRepo.Update(entity);
        }
    }
}
