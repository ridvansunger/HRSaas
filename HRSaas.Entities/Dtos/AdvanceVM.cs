using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class AdvanceVM
    {
        public int Id { get; set; }

        public int PersonalId { get; set; }

        public string NameAndSurname { get; set; }
        public string DepartmentName { get; set; }
        public string TitleName { get; set; }
        public decimal Salary { get; set; }
        public decimal AdvanceAmount { get; set; }
        public DateTime RequestDate { get; set; }
        public bool ApprovalStatus { get; set; }

        public bool SeenByManager { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Comment { get; set; }


        

       
    }
}
