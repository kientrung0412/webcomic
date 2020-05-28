using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace WebComic.Controllers
{
    public class ComicController : Controller
    {
        // GET
        public ActionResult Index(int comicId)
        {
            ComicDAO comicDao = new ComicDAO();
            comic comic = comicDao.GetComicAs(comicId).Result;


            ViewBag.Title = comic.NameComic;
            ViewBag.Data = comic;

            return View();
        }

        public ActionResult ComicCategory()
        {
            return View();
        }

        public ActionResult _Slide()
        {
            ComicDAO comicDao = new ComicDAO();
            ViewBag.Data = comicDao.SlideComic();

            return PartialView("_Slide");
        }
        public ActionResult _ComicNew()
        {
            ComicDAO comicDao = new ComicDAO();
            ViewBag.Data = comicDao.NewComic();

            return PartialView("_ComicNew");
        }

        
        // [HttpPost]
        // public ActionResult SearchAdvanced()
        // {
        //     
        // }
    }
}