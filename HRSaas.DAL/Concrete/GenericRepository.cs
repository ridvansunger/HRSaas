using HRSaas.DAL.Abstract;
using HRSaas.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSaas.DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        public HRSaasContext _Db;
        public GenericRepository(HRSaasContext Db)
        {
            _Db = Db;
        }

        public virtual async Task<T> Add(T entity)
        {
            await _Db.Set<T>().AddAsync(entity);
            _Db.SaveChanges();
            return entity;
           
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            _Db.Set<T>().Remove(entity);
            return await _Db.SaveChangesAsync() > 0 ? true : false;
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            return predicate == null ? _Db.Set<T>().ToList() : _Db.Set<T>().Where(predicate).ToList() ;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _Db.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Update(T entity)
        {

            _Db.Set<T>().Update(entity);
            await _Db.SaveChangesAsync();
            return entity;
        }
    }
}
