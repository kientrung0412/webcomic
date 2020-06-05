using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace WebComic.Controllers
{
    public class UserController : Controller
    {
        // profile
        public ActionResult Index()
        {
            user user = new user();
            user.UserId = 1;
            user.RoleId = 1;

            Session["user"] = user;

            if (Session["user"] == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }
            else
            {
                return View();
            }
        }

        //from them truyen
        public ActionResult UpComic()
        {
            if (Session["user"] != null)
            {
                CategoryDAO categoryDao = new CategoryDAO();
                NationDAO nationDao = new NationDAO();

                ViewBag.Nations = nationDao.List();
                ViewBag.Categorys = categoryDao.List();

                return View();
            }
            else
            {
                return Redirect(Url.Action("Login", "User"));
            }
        }

        //them truyen moi
        [HttpPost]
        public ActionResult UpComic(String nameComic, String author, int[] category, int nation,
            HttpPostedFileBase file, String summary)
        {
            if (Session["user"] != null)
            {
                user user = (user) Session["user"];

                List<int> categorys = new List<int>();

                if (category.Length > 0)
                {
                    categorys = category.ToList();
                }

                comic comic = new comic();

                comic.NationId = nation;
                comic.UserId = user.UserId;
                comic.NameComic = nameComic;
                comic.AuthorComic = author;
                comic.SummaryComic = summary;

                ComicDAO comicDao = new ComicDAO();

                var add = comicDao.Add(comic, categorys);

                String path = String.Format("~/Upload/Truyen/{0}", add.ComicId);
                path = Server.MapPath(path);

                var a = UploadFile.Upload(file, path, "baner.jpg");

                path = String.Format("Upload/Truyen/{0}/baner.jpg", add.ComicId);

                comic.CommicBanner = path;

                var i = comicDao.Update(comic);
                return Content(i.ToString());
            }
            else
            {
                return Redirect(Url.Action("Login", "User"));
            }
        }
        
        //truyen dang theo tai khoan
        public ActionResult ComicsUser()
        {
            if (Session["user"] != null)
            {
                int page = Convert.ToInt32(Request["page"]);

                if (page <= 0)
                {
                    page = 1;
                }

                var user = (user) Session["user"];
                int userId = user.UserId;

                ComicDAO comicDao = new ComicDAO();
                var list = comicDao.ComicUser(new Pagination(10, page), userId);

                ViewBag.Comics = list.Comics;
                ViewBag.Page = list.Page;
                ViewBag.Numpage = list.PageSize;

                return View();
            }
            else
            {
                return Redirect(Url.Action("Login", "User"));
            }
        }
        
        //thay doi ten
        [HttpPost]
        public ActionResult ChangeName(String name)
        {
            return Content("gdh");
        }

        //thay doi mat khau
        [HttpPost]
        public ActionResult ChangePass(String oldPass, String newPass)
        {
            return Content("ghgs");
        }

        //lich su doc
        public ActionResult History()
        {
            var str = Request.Cookies["history"]?.Value;
            var history = MyConvert.StringToListDictionary(str);

            ViewBag.History = history;
            return View();
        }

        //dang xuat
        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect(Url.Action("Index", "Home"));
        }

        //from dang nhap
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return Redirect(Url.Action("Index", "User"));
            }
            else
            {
                return View();
            }
        }

        //dang nhap
        [HttpPost]
        public ActionResult Login(String mail, String password)
        {
            UserDAO userDao = new UserDAO();
            var check = userDao.Login(mail, password);
            if (check)
            {
                return Redirect(Url.Action("Index", "User"));
            }
            else
            {
                ViewBag.Mss = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return Redirect(Url.Action("Login", "User"));
            }
        }

        //tat ca chuyen
        public ActionResult ListAllComic()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return Redirect(Url.Action("Index", "User"));
            }
        }

        //kiem duyet truyen
        public ActionResult Censorship()
        {
            if (Session["user"] != null)
            {
                int page = Convert.ToInt32(Request["page"]);

                if (page <= 0)
                {
                    page = 1;
                }

                ComicDAO comicDao = new ComicDAO();
                var list = comicDao.CensorshipComic(new Pagination(10, page));

                ViewBag.Comics = list.Comics;
                ViewBag.Page = list.Page;
                ViewBag.Numpage = list.PageSize;

                return View();
            }
            else
            {
                return Redirect(Url.Action("Index", "User"));
            }
        }

        //thong qua truyen
        [HttpPost]
        public ActionResult UpdateCensorship()
        {
            String id = Request["id"];

            ComicDAO comicDao = new ComicDAO();

            var comic = comicDao.GetComicAs(Convert.ToInt32(id)).Result;
            comic.StatusComicId = 1;
            var n = comicDao.Update(comic);

            return Content((n == 1).ToString());
        }

        //xoa truyen
        [HttpPost]
        public ActionResult DeleteComic()
        {
            String id = Request["id"];

            ComicDAO comicDao = new ComicDAO();

            var n = comicDao.Delete(Convert.ToInt32(id));

            return Content((n == 1).ToString());
        }
    }
}