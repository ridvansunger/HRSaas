using HRSaas.Business.Abstract;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class CountyBusiness:ICountyBusiness
    {
        ICountyRepository _countyRepo;
        public CountyBusiness(ICountyRepository repo) 
        {
            _countyRepo = repo;
        }

        public async Task<County> Add(County entity)
        {
            return await _countyRepo.Add(entity);
        }
        public async Task<bool> Delete(int id)
        {
           return await _countyRepo.Delete(id);
        }
        public IEnumerable<County> GetAll(Func<County, bool> predicate = null)
        {
            return  _countyRepo.GetAll(predicate);
        }
        public async Task<County> GetById(int id)
        {
            return await _countyRepo.GetById(id);
        }
        public async Task<County> Update(County entity)
        {
            return await  _countyRepo.Update(entity);
        }
    }
}
