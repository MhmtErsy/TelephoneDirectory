using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TelephoneDirectory.Web.ViewModels
{
    public class AddEmployeViewModel
    {
        [DisplayName("Ad"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required, StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        public int? DirectorId { get; set; }

        public int? DepartmentId { get; set; }
    }
}