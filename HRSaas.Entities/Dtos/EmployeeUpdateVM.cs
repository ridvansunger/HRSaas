using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string AddressDescription { get; set; }
        public string PostalCode { get; set; }
        public int? CityId { get; set; }
        public int? CountyId { get; set; }
    }
}
