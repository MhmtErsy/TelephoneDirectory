using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Entities.Messages
{
    public enum ErrorMessageCode
    {
        UserNameAlreadyExists = 207,
        AdminCouldNotFind = 210,
        UserNameorPassWrong=211,
        EmployeeIsADirector=212,
        UnauthorizedRequest = 403
    }
}
