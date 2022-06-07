using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
   public interface IExpenditureBusiness
    {
        IEnumerable<Expenditure> GetAll(Func<Expenditure, bool> predicate = null);
        Task<Expenditure> GetById(int id);
        Task<Expenditure> Add(Expenditure entity);
        Task<Expenditure> Update(Expenditure entity);
        Task<bool> Delete(int id);
        string GetForFileNameAsync(string userMail);
       
    }
}
