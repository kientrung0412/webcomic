using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;
using Newtonsoft.Json;

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
            ViewBag.Page =page;
            
            return View();
        }
        

        [HttpPost]
        public ActionResult GetList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            String searchValue = Request["search[value]"];
            String sortColumnName = Request["columns[" + Request["order[0][column]"] + "][data]"];
            String sortDirection = Request["order[0][dir]"];


            CategoryDAO categoryDao = new CategoryDAO();

            List<category> list = categoryDao.List();

            int totalRow = list.Count;

            //Tìm kiếm


            if (!String.IsNullOrEmpty(searchValue))
            {
                list = categoryDao.ListSearch(searchValue);
            }

            int filterRow = list.Count;

            //Sắp xếp


            // list = list.(order )

            //Phân trang

            list = list.Skip(start).Take(length).ToList();

            return Json(
                new {data = list, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = filterRow},
                JsonRequestBehavior.AllowGet);
        }
    }
}