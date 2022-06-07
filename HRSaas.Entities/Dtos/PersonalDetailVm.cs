using Microsoft.AspNetCore.Http;
using System;

namespace HRSaas.Entities.Dtos
{
    public class PersonalDetailVm
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tc { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public string Photo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public bool PersonalIsActive { get; set; }
        public int DepartmentId { get; set; }
        public string DepertmentName { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string AddressDescription { get; set; }
        public string PostalCode { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int? CompanyId { get; set; }
    }
}
