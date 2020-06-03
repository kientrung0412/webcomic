using System;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace WebComic.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(String categoryId, String page)
        {
            if (page == null)
            {
                page = "1";
            }

            if (categoryId.Trim() != null)
            {
                ComicDAO comicDao = new ComicDAO();
                CategoryDAO categoryDao = new CategoryDAO();


                var list = comicDao.CategoryComic(new Pagination(16, Convert.ToInt32(page)), categoryId);

                ViewBag.ComicsMain = list.CategoryFiltes;
                ViewBag.Page = list.Page;
                ViewBag.Numpage = list.PageSize;
                ViewBag.Category = categoryId;
                ViewBag.NameCategory = categoryDao.One(Convert.ToInt32(categoryId)).NameCategory;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
            category category = new category();
            category.CategoryId = Convert.ToInt32(Request["id"]);
            return Content("true");
        }


        public ActionResult _Navbar()
        {
            CategoryDAO categoryDao = new CategoryDAO();
            ViewBag.Data = categoryDao.List();

            return PartialView("_Navbar");
        }
    }
}