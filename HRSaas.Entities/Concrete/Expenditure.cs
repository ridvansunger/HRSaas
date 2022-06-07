using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Expenditure
    {

        [Key]
        public int Id { get; set; }

        //personel açıklaması
        [StringLength(255)]
        public string ExDescription { get; set; }

        public DateTime ExpenditureDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExpenditureAmount { get; set; }

        [StringLength(500)]
        public string Attachment { get; set; }


        //masraf durumu(onay, red)
        public bool Status { get; set; }

        public bool SeenByManager { get; set; }

        public DateTime RequestDate { get; set; }
        public DateTime? ApprovalDate { get; set; }


        [ForeignKey("Personal")]
        public int PersonalId { get; set; }
        public virtual Personal Personal { get; set; }


    }
}
