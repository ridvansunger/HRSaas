using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Concrete
{
    public class Personal:IdentityUser<int>
    {
        public Personal()
        {
            this.Advances = new HashSet<Advance>();
            this.Leaves = new HashSet<Leave>();
        }

        //[Key]
        //public int Id { get; set; }
        //true erkek, false kadın
        public bool? Gender { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(11)]
        public string Tc { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        public bool IsActivated { get; set; }

        private string defaultMaleImageName = "DefaultMaleImage.png";
        private string defaultFemaleImageName = "DefaultFemaleImage.png";
        private string image = null;

        [StringLength(255)]
        public string Photo
        {
            get { return image; }
            set { image=value == null ? Gender == true ? defaultMaleImageName : defaultFemaleImageName : value; }
        }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        [ForeignKey("Title")]
        public int? TitleId { get; set; }
        public virtual Title Title { get; set; }

        public bool PersonalIsActive { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public DateTime BirthDate { get; set; }
        public virtual ICollection<Advance> Advances { get; set; }
        //Aldığı Maaş
        public decimal? Salary { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }

        //Totalde Kalan Yıllık İzin Hakkı
        public float? AnnualLeave { get; set; }


        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Expenditure> Expenditures { get; set; }
    }
}
