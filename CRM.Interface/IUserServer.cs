using System.Web.Mvc;

namespace CRM.Interface
{
    public interface IUserServer
    {

        JsonResult GetUsers();
        int Add(string userStr);
        JsonResult Find(string name);
        int Edit(int id, string eUserStr);
        int Delete(int id);
        JsonResult SelectUser(int id);
        JsonResult GetPageUsers(int currentPage);
        int GetUserNumSize();
    }
}
