using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Title
    {
        public Title()
        {
            this.Personals = new HashSet<Personal>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string TitleName { get; set; }
        public virtual ICollection<Personal> Personals { get; set; }

    }
}
