using AutoMapper;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using System.Threading.Tasks;

namespace HRSaas.Business.Mapping
{

    public class GeneralProfile : Profile
    {
        IPersonalBusiness _personalBusiness;
        
        public GeneralProfile(IPersonalBusiness personalBusiness )
        {
            _personalBusiness = personalBusiness;
            
        }
        public async Task<Personal> ManualMapper(int id, PersonalDetailVm personalVM)
        {
            Personal personal = await _personalBusiness.GetById(personalVM.Id);
            personal.AddressId = personalVM.AddressId;
            personal.BirthDate = personalVM.BirthDate;
            personal.DepartmentId = personalVM.DepartmentId;
            personal.EndDate = personalVM.EndDate;
            personal.FirstName = personalVM.FirstName;
            personal.Gender = personalVM.Gender;
            personal.LastName = personalVM.LastName;
            personal.Password = personalVM.Password;
            personal.PersonalIsActive = personalVM.PersonalIsActive;
            personal.Phone = personalVM.Phone;
            personal.Salary = personalVM.Salary;
            personal.StartDate = personalVM.StartDate;
            personal.TitleId = personalVM.TitleId;

            return personal;
        }


        public GeneralProfile()
        {
            CreateMap<PackageVM, Package>();
            CreateMap<Package, PackageVM>();

            CreateMap<CompanyVM, Company>();
            CreateMap<Company, CompanyVM>();

            CreateMap<LeaveVM, Leave>();
            CreateMap<Leave, LeaveVM>();

            CreateMap<AdvanceVM, Advance>();
            CreateMap<Advance, AdvanceVM>();

            CreateMap<PersonalDetailVm, Address>()
                .ForMember("Description", t1 => t1.MapFrom(t1 => t1.AddressDescription))
                .ForMember("Id", t1 => t1.MapFrom(t1 => t1.AddressId));

            CreateMap<Address, PersonalDetailVm>()
                .ForMember("AddressDescription", t1 => t1.MapFrom(t1 => t1.Description))
                .ForMember("AddressId", t0 => t0.MapFrom(t0 => t0.Id));

            CreateMap<PersonalDetailVm, Personal>();
            CreateMap<Personal, PersonalDetailVm>();

            CreateMap<Expenditure, PersonalExpenditureVM>();
            CreateMap<PersonalExpenditureVM, Expenditure>();


            CreateMap<Company, CompanyVM>();

            CreateMap<Package, PackageVM>();

            //CreateMap<PersonalDetailVm, AddressUpdateDto>()
            //    .ForMember("Id",t0=>t0.MapFrom(t0=>t0.AddressId))
            //    .ForMember("Description",t0=>t0.MapFrom(t0=>t0.AddressDescription));


            //CreateMap<AddressUpdateDto, Address>();
            //CreateMap<Address, AddressUpdateDto>();

            //CreateMap<PersonalDetailVm, PersonalUpdateDto>();
            //CreateMap<PersonalUpdateDto, PersonalDetailVm>();

            //CreateMap<AddressAddDto, Address>();
            //CreateMap<Address, AddressDto>();

            //CreateMap<Personal, PersonalDetailDto>();
            //CreateMap<PersonalDetailDto, Personal>();

            //CreateMap<PersonalAddDto, PersonalDto>();
            //CreateMap<PersonalDto, PersonalAddDto>();

            //CreateMap<PersonalDto, Personal>();
            //CreateMap<Personal, PersonalDto>();

            //CreateMap<PersonalAddDto, Personal>();
            //CreateMap<Personal, PersonalAddDto>();


            //CreateMap<PersonalUpdateDto, Personal>();
            //CreateMap<Personal, PersonalUpdateDto>();

            //CreateMap<AddressUpdateDto, Personal>();
            //CreateMap<Personal, AddressUpdateDto>();

            //CreateMap<PersonalDetailDto, PersonalDetailDisplayVM>();
            //CreateMap<PersonalDetailDisplayVM, PersonalDetailDto>();

            //CreateMap<PersonalDto, PersonalUpdateDto>();
            //CreateMap<PersonalUpdateDto, PersonalDto>();

            //CreateMap<PersonalDetailVm, PersonalDto>();
            //CreateMap<PersonalDto, PersonalDetailVm>();
        }
    }
}
