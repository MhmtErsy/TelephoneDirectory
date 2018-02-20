using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Entities
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [DisplayName("Name"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır..")]
        public string Name { get; set; }

        [DisplayName("Surname"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır..")]
        public string Surname { get; set; }

        [DisplayName("Username"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır..")]
        public string Username { get; set; }

        [DisplayName("Password"), Required, StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı max. {1} - min. {2} karakter olmalıdır..")]
        public string Password { get; set; }
        
    }
}
