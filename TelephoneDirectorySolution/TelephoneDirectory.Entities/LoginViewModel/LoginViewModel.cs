using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Entities.LoginViewModel
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır..")]
        public string Username { get; set; }

        [DisplayName("Parola"), Required, StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı max. {1} - min. {2} karakter olmalıdır..")]
        public string Password { get; set; }
    }
}
