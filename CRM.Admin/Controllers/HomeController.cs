using System.Web.Mvc;

namespace CRM.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home 1Page1";

            return View();
        }
    }
}
