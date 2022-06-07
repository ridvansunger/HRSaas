using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class PackageVM
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int UserCount { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool PackageIsActive { get; set; }
        public int DurationTime { get; set; }
    }
}
