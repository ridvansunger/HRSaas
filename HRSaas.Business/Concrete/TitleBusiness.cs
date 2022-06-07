using HRSaas.Business.Abstract;
using HRSaas.DAL.Abstract;
using HRSaas.DAL.Concrete;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class TitleBusiness:  ITitleBusiness
    {
        ITitleRepository _repo;
        public TitleBusiness(ITitleRepository repo)
        {
            _repo = repo;
        }

        public async Task<Title> Add(Title entity)
        {
            return await _repo.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
           return await _repo.Delete(id);
        }

        public IEnumerable<Title> GetAll(Func<Title, bool> predicate = null)
        {
           return _repo.GetAll(predicate);
        }

        public async Task<Title> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Title> Update(Title entity)
        {
            return await  _repo.Update(entity);
        }
    }
}
