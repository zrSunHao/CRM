using CRM.Dal.Abstract;
using CRM.Dal.Concrete;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Bll.Abstract
{
    public interface IUserServer
    {

        List<User> GetUsers();
        int add(User user);
        List<User> Find(string name);
        int Edit(User eUser);
        int delete(int id);
        User SelectUser(int id);
        List<User> GetPageUsers(int currentPage);
        int getUserNumSize();
    }
}
