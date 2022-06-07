using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string LeaveTypeName { get; set; }
    }
}
