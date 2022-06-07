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
    public class CityRepository:GenericRepository<City>,ICityRepository
    {
        public CityRepository(HRSaasContext db):base(db)
        {

        }
    }
}
