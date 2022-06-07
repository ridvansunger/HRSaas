using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class City
    {
        public City()
        {
            this.Counties = new HashSet<County>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string CityName { get; set; }
        public virtual ICollection<County> Counties { get; set; }

    }
}
