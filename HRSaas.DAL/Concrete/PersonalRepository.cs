using HRSaas.DAL.Abstract;
using HRSaas.DAL.Context;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.DAL.Concrete
{
    public class PersonalRepository : GenericRepository<Personal>,IPersonalRepository
    {

        public PersonalRepository(HRSaasContext db) : base(db)
        {
        }
    }


}
