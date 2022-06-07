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
    public class DepartmentBusiness: IDepartmentBusiness
    {
        IDepartmentRepository _departmentRepo;
        public DepartmentBusiness(IDepartmentRepository repo)
        {
            _departmentRepo = repo;
        }

        public async Task<Department> Add(Department entity)
        {
            return await _departmentRepo.Add(entity);
        }
        public async Task<bool> Delete(int id)
        {
            return await _departmentRepo.Delete(id);
        }
        public IEnumerable<Department> GetAll(Func<Department, bool> predicate = null)
        {
            return _departmentRepo.GetAll(predicate);
        }
        public async Task<Department> GetById(int id)
        {
            return await _departmentRepo.GetById(id);
        }
        public async Task<Department> Update(Department entity)
        {
           return await _departmentRepo.Update(entity);
        }
    }
}
