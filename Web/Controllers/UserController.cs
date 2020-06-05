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
        private user _user;

        public UserController()
        {
            user user = new user();
            user.UserId = 1;
            user.RoleId = 1;
            
            // Session["user"] = user;


            try
            {
                // _user = (user) Session["user"];
                _user = user;
            }
            catch
            {
                _user = null;
            }
        }

        // profile
        public ActionResult Index()
        {
            if (_user == null)
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
            if (_user != null)
            {
                CategoryDAO categoryDao = new CategoryDAO();
                NationDAO nationDao = new NationDAO();

                ViewBag.Nations = nationDao.List();
                ViewBag.Categorys = categoryDao.List();
                ViewBag.Mess = -1;

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
            if (_user != null)
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

                try
                {
                    ComicDAO comicDao = new ComicDAO();

                    var add = comicDao.Add(comic, categorys);

                    String path = String.Format("~/Upload/Truyen/{0}", add.ComicId);
                    path = Server.MapPath(path);

                    var a = UploadFile.Upload(file, path, "baner.jpg");

                    path = String.Format("/Upload/Truyen/{0}/baner.jpg", add.ComicId);

                    comic.CommicBanner = path;

                    var i = comicDao.Update(comic);

                    if (i > 0)
                    {
                        ViewBag.Mess = 1;
                    }
                    else
                    {
                        ViewBag.Mess = 0;
                    }

                    CategoryDAO categoryDao = new CategoryDAO();
                    NationDAO nationDao = new NationDAO();

                    ViewBag.Nations = nationDao.List();
                    ViewBag.Categorys = categoryDao.List();
                }
                catch
                {
                    ViewBag.Mess = 0;
                }

                return View();
            }
            else
            {
                return Redirect(Url.Action("Login", "User"));
            }
        }

        //truyen dang theo tai khoan
        public ActionResult ComicsUser()
        {
            if (_user != null)
            {
                int page = Convert.ToInt32(Request["page"]);

                if (page <= 0)
                {
                    page = 1;
                }

                int userId = _user.UserId;

                ComicDAO comicDao = new ComicDAO();
                PaginationComic list = comicDao.ComicUser(new Pagination(10, page), userId);

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
            if (_user != null)
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
            if (_user != null && _user.RoleId == 1)
            {
                int page = Convert.ToInt32(Request["page"]);

                if (page <= 0)
                {
                    page = 1;
                }

                ComicDAO comicDao = new ComicDAO();
                var list = comicDao.List(new Pagination(10, page));

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

        //kiem duyet truyen
        public ActionResult Censorship()
        {
            if (_user != null && _user.RoleId == 1)
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
            try
            {
                String id = Request["id"];

                ComicDAO comicDao = new ComicDAO();

                var n = comicDao.Delete(Convert.ToInt32(id));

                return Content((n > 0).ToString());
            }
            catch
            {
                return Content("false");
            }
        }

        //hiển thị và thêm mới chapter
        public ActionResult Chapter()
        {
            if (_user == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }
            else
            {
                int id = Convert.ToInt32(Request["idChapter"]);

                ChapterDAO chapterDao = new ChapterDAO();
                var chapters = chapterDao.ListChapterComic(id);

                ViewBag.Chapters = chapters;

                return View();
            }
        }
    }
}