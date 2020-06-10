using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;
using Newtonsoft.Json;

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

        public ActionResult Home()
        {
            var page = Convert.ToInt32(Request["page"]);

            if (page < 1)
            {
                page = 1;
            }

            ComicDAO comicDao = new ComicDAO();
            var list = comicDao.NewUpComic(new Pagination(12, page));

            // news comic

            List<comic> comics = list.Comics;
            ViewBag.ComicsMain = comics;

            int numPage = list.PageSize;
            ViewBag.Numpage = numPage;

            ViewBag.Page = page;

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
            ViewBag.Data = comicDao.NewComic().OrderByDescending(comic => comic.UserId);

            return PartialView("_ComicNew");
        }


        public ActionResult SearchAdvanced(String[] arrayIn, String[] arrayNotIn, String nameComic, String status,
            String author, String nation, String sort)
        {
            List<String> listIn = new List<string>();
            List<String> listNotIn = new List<string>();

            if (arrayIn != null)
            {
                listIn = arrayIn.ToList();
            }

            if (arrayNotIn != null)
            {
                listNotIn = arrayNotIn.ToList();
            }


            SuperSearch superSearch = new SuperSearch(listIn, listNotIn, Convert.ToInt32(nation),
                Convert.ToInt32(status),
                nameComic, author);

            ComicDAO comicDao = new ComicDAO();
            CategoryDAO categoryDao = new CategoryDAO();
            StatusComicDAO statusComicDao = new StatusComicDAO();
            NationDAO nationDao = new NationDAO();

            var comics = comicDao.SearchAdvanced(superSearch, new Pagination(16, Convert.ToInt32(1)), sort);

            ViewBag.Comics = comics.Comics;

            ViewBag.Numpage = comics.PageSize;

            ViewBag.Page = comics.Page;

            ViewBag.Search = nameComic;

            ViewBag.Categorys = categoryDao.List().OrderBy(category => category.CategoryId);

            ViewBag.Status = statusComicDao.ListUser();

            ViewBag.Nations = nationDao.List();

            ViewBag.ListInAc = listIn;
            
            ViewBag.ListNotInAc = listNotIn;

            ViewBag.AuthorAc = author;

            ViewBag.SortAc = sort;

            ViewBag.StatusAc = status;

            ViewBag.NationAc = nation;
            
            return View();
        }

        public String Test(String[] arrayIn, String[] arrayNotIn, String nameComic, String status, String author,
            String nation, String page, String sort)
        {
            List<String> listIn = new List<string>();
            List<String> listNotIn = new List<string>();

            if (arrayIn != null)
            {
                listIn = arrayIn.ToList();
            }

            if (arrayNotIn != null)
            {
                listNotIn = arrayNotIn.ToList();
            }


            SuperSearch superSearch = new SuperSearch(listIn, listNotIn, Convert.ToInt32(nation),
                Convert.ToInt32(status),
                nameComic, author);

            ComicDAO comicDao = new ComicDAO();
            Pagination pagination = new Pagination(16, Convert.ToInt32(page));


            PaginationComic paginationComic = comicDao.SearchAdvanced(superSearch, pagination, sort);


            String json = JsonConvert.SerializeObject(paginationComic, Formatting.Indented);

            return json;
        }
        
        public ActionResult History()
        {
            var str = Request.Cookies["history"]?.Value;

            List<comic> list = new List<comic>();
            
            if (str != null)
            {
                var history = MyConvert.StringToListDictionary(str);
                int[] comicIds = Array.ConvertAll(history.Keys.ToArray(), s => int.Parse(s));

                list  = new ComicDAO().Histories(comicIds);
            }

            ViewBag.History = list;
            return View();
        }
        
    }
}