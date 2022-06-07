using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Business.Abstract
{
    public interface ITitleBusiness 
    {
        IEnumerable<Title> GetAll(Func<Title, bool> predicate = null);
        Task<Title> GetById(int id);
        Task<Title> Add(Title entity);
        Task<Title> Update(Title entity);
        Task<bool> Delete(int id);
    }
}
