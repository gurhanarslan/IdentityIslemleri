using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdenty.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Kullanıcı adını boş bırakmayınız.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password yerini boş bırakmayınız.")]
        [StringLength(12,MinimumLength =6,ErrorMessage ="6-12  Karakter arasında şifre giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Mail adresinizi giriniz.")]
        [EmailAddress]
        public string Email { get; set; }


    }
}
