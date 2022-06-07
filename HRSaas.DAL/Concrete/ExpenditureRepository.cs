using HRSaas.DAL.Abstract;
using HRSaas.DAL.Context;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.DAL.Concrete
{
    public class ExpenditureRepository : GenericRepository<Expenditure>, IExpenditureRepository
    {
        private HRSaasContext _context;
        public ExpenditureRepository(HRSaasContext db) : base(db)
        {
            _context = db;
        }

        public string GetForFileName(string userMail)
        {
            var resultSet = (from t0 in _context.Users
                            join t1 in _context.Companies
                            on t0.CompanyId equals t1.Id
                            where t0.Email == userMail
                            select new { t1.CompanyName,t0.FirstName, t0.LastName}).FirstOrDefault();

            return  resultSet.CompanyName +"-"+ resultSet.FirstName + "-" + resultSet.LastName;
             
        }
    }
}
