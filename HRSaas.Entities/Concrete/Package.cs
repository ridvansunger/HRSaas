using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Package
    {

        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string PackageName { get; set; }

        public int UserCount { get; set; }

        [StringLength(255)]
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool PackageIsActive { get; set; }

        public int DurationTime { get; set; }

        public virtual Company Company { get; set; }

    }
}
