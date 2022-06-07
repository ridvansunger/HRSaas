using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Address
    {
        public Address()
        {
            this.Personals = new HashSet<Personal>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string AddressName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        
        [ForeignKey("County")]
        public int? CountyId { get; set; }
        public virtual County County { get; set; }

        public virtual ICollection<Personal> Personals { get; set; }
    }
}
