using System.Web.Mvc;

namespace WebComic.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
        
    }
}