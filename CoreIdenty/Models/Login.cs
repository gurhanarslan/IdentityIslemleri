using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdenty.Models
{

    public class Login
    {
        [Required]
        [UIHint("Email")]
        public string Email { get; set; }
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
