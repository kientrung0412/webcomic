using System.Collections.Generic;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace WebComic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ComicDAO comicDao = new ComicDAO();
            var list = comicDao.NewUpComic(new Pagination(12, 1));

            // news comic
            List<comic> comics = list.Comics;
            ViewBag.ComicsMain = comics;

            //phan trang
            int numPage = list.PageSize;
            ViewBag.Numpage = numPage;

            int page = list.Page;
            ViewBag.Page = page;

            return View();
        }
    }
}