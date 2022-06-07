using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Department
    {
        public Department()
        {
            this.Personals = new HashSet<Personal>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string DepartmentName { get; set; }

        public ICollection<Personal> Personals { get; set; }


    }
}
