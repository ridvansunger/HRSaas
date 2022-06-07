using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Company
    {
        public Company()
        {
            this.CompanyPersonals = new HashSet<Personal>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string TaxIdNumber { get; set; }

        [StringLength(255)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }


        [StringLength(255)]
        public string CompanyMail { get; set; }


        [StringLength(500)]
        public string Address { get; set; }


        [StringLength(500)]
        public string Description { get; set; }

        public DateTime PackageStartDate { get; set; }


        [ForeignKey("Package")]
        public int? PackageId { get; set; }
        public virtual Package Package { get; set; }
        
        public virtual ICollection<Personal> CompanyPersonals { get; set; }
    }
}
