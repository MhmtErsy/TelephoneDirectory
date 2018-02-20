using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelephoneDirectory.Entities;

namespace TelephoneDirectory.Web.ViewModels
{
    public class DepartmentViewModel
    {
        public Department Department { get; set; }
        public List<Employee> DepartmentEmployees { get; set; }
    }
}