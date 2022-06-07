using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public bool FirstSingin { get; set; }
        //public bool IsPassive { get; set; }
        //public bool Persistent = true;
        //public bool Lock = true;
    }
}
