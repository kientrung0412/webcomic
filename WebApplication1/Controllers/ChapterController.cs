using System;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace WebComic.Controllers
{
    public class ChapterController : Controller
    {
        // GET
        public ActionResult Index(int chapterId)
        {
            ChapterDAO chapterDao = new ChapterDAO();
            chapterDao.UpdateView(chapterId);
            
            
            var cookie = new HttpCookie("comicId", chapterId.ToString());
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.AppendCookie(cookie);
            return View();
        }
    }
}