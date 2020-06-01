using System.Web.Mvc;

namespace WebComic.Controllers
{
    public class UserController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}