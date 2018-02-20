using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DataAccessLayer;
using TelephoneDirectory.Entities;

namespace TelephoneDirectory.BusinessLayer
{
    public class Test
    {
        Repository<Admin> repo_admin = new Repository<Admin>();

        public Test()
        {
            DatabaseContext db = new DatabaseContext();
            db.Admins.ToList();
        }
        public void UpdateTest()
        {

        }

        public void DeleteTest()
        {

        }
    }
}
