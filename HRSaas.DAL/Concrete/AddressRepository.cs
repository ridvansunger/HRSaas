using HRSaas.DAL.Abstract;
using HRSaas.DAL.Context;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;

namespace HRSaas.DAL.Concrete
{
    public class AddressRepository: GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(HRSaasContext db):base(db)
        {
        }
    }
}
