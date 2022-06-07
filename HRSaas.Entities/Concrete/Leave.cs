using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Personal")]
        public int PersonalId { get; set; }
        [ForeignKey("LeaveType")]
        public int LeaveTypeId { get; set; }
        public bool ApprovalStatus { get; set; }
        public bool SeenByManager { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime RequestDate { get; set; }
        [StringLength(255)]
        public string Comment { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
