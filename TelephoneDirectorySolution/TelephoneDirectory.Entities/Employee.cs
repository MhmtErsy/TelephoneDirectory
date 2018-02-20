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
    [Table("Employees")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [DisplayName("Name"), Required, StringLength(25, ErrorMessage = "{0} area must be max. {1}")]
        public string Name { get; set; }

        [DisplayName("Surname"), Required, StringLength(25, ErrorMessage = "{0} area must be max. {1}")]
        public string Surname { get; set; }

        
        [DisplayName("Telephone Number"),DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public Nullable<int> DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public virtual Employee Director { get; set; }

        public Nullable<int> DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Departman { get; set; }

        
        

    }
}
