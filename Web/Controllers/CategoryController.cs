using System;
using System.Linq;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace WebComic.Controllers
{
    public class CategoryController : Controller
    {

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