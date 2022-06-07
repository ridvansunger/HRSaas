using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class PersonalExpenditureVM
    {
        public int Id { get; set; }
        public int PersonalId { get; set; }
        public string NameAndSurname { get; set; }
        public string DepartmentName { get; set; }
        public string TitleName { get; set; }
        public string ExDescription { get; set; }
        public DateTime? ExpenditureDate { get; set; }
        public decimal ExpenditureAmount { get; set; }
        public string Attachment { get; set; }
        public bool? Status { get; set; }
        public bool? SeenByManager { get; set; }
        public IFormFile File { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ApprovalDate { get; set; }


    }
}
