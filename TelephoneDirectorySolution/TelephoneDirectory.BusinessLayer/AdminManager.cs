using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Entities;
using TelephoneDirectory.Entities.LoginViewModel;
using TelephoneDirectory.BusinessLayer.Result;
using TelephoneDirectory.Entities.Messages;

namespace TelephoneDirectory.BusinessLayer
{
    public class AdminManager : ManagerBase<Admin>
    {
        public BusinessLayerResult<Admin> LoginAdmin(LoginViewModel model)
        {
            Admin a = Find(x => x.Username == model.Username && x.Password == model.Password);
            BusinessLayerResult<Admin> res = new BusinessLayerResult<Admin>();

            res.Result = a;
            if (a == null)
            {
                res.AddError(ErrorMessageCode.UserNameorPassWrong, "Kullanıcı adı ya da parola uyuşmuyor.");
            }
            return res;
        }
    }
}
