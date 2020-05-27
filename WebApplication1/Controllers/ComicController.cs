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

            var list = comic.comic_category.ToList();
            var a = comicDao.SearchAdvanced();
            
            
            ViewBag.Title = comic.NameComic;
            ViewBag.Data = comic;

            return View();
        }

        public ActionResult ComicCategory()
        {
            return View();
        }


        
        // [HttpPost]
        // public ActionResult SearchAdvanced()
        // {
        //     
        // }
    }
}