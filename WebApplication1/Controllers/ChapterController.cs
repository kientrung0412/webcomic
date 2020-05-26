using System;
using System.Web;
using System.Web.Mvc;

namespace WebComic.Controllers
{
    public class ChapterController : Controller
    {
        // GET
        public ActionResult Index(int chapterId)
        {
            return View();
        }
    }
}