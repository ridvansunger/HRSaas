using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSaas.Business.Concrete
{
    public class AddressBusiness : IAddressBusiness
    {
        private IAddressRepository _IAddressRepository;
        private IMapper _mapper;
        public AddressBusiness(IAddressRepository repo, IMapper mapper)
        {
            _IAddressRepository = repo;            
            _mapper = mapper;
        }

        public async Task<Address> Add(Address entity)
        {
            return await _IAddressRepository.Add(entity);
        }
        public async Task<Address> Update(Address entity)
        {
            return await _IAddressRepository.Update(entity);
        }
        public async Task<bool> Delete(int id)
        {
            return await _IAddressRepository.Delete(id);
        }
        public IEnumerable<Address> GetAll(Func<Address, bool> predicate)
        {
            return _IAddressRepository.GetAll();
        }
        public async Task<Address> GetById(int id)
        {
            return await _IAddressRepository.GetById(id);
        }
    }
}
