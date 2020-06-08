using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.Ajax.Utilities;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace WebComic.Controllers
{
    public class UserController : Controller
    {
        user _user;

        public UserController()
        {
            try
            {
                _user = SessionUser.GetSession();
            }
            catch
            {
                _user = null;
            }
        }

        private Boolean CheckStatusUser()
        {
            user user = (user) SessionUser.GetSession();
            return (user == null || user.StatusUserId != 1);
        }

        private Boolean CheckAdmin()
        {
            user user = SessionUser.GetSession();

            Boolean b = user.RoleId == 1;

            return (b);
        }


        private Boolean CheckViewer()
        {
            user user = SessionUser.GetSession();

            Boolean b = user.RoleId > 2;

            return (b);
        }

        // profile
        public ActionResult Index()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        //from them truyen
        public ActionResult UpComic()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                CategoryDAO categoryDao = new CategoryDAO();
                NationDAO nationDao = new NationDAO();

                ViewBag.Nations = nationDao.List();
                ViewBag.Categorys = categoryDao.List();
                ViewBag.Mss = -1;

                return View();
            }

            return RedirectToAction("Index");
        }

        //them truyen moi
        [HttpPost]
        public ActionResult UpComic(String nameComic, String author, int[] category, int nation,
            HttpPostedFileBase file, String summary)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                List<int> categorys = new List<int>();

                if (category != null)
                {
                    categorys = category.ToList();
                }

                comic comic = new comic();

                comic.NationId = nation;
                comic.UserId = _user.UserId;
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
                        ViewBag.Mss = 1;
                    }
                    else
                    {
                        ViewBag.Mss = 0;
                    }

                    CategoryDAO categoryDao = new CategoryDAO();
                    NationDAO nationDao = new NationDAO();

                    ViewBag.Nations = nationDao.List();
                    ViewBag.Categorys = categoryDao.List();
                }
                catch
                {
                    ViewBag.Mss = 0;
                }

                return View();
            }

            ViewBag.Mss = 0;
            return View();
        }

        //truyen dang theo tai khoan
        public ActionResult ComicsUser()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

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

        //thay doi ten
        [HttpPost]
        public ActionResult ChangeName(String name)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            return Content("gdh");
        }

        //thay doi mat khau
        [HttpPost]
        public ActionResult ChangePass(String oldPass, String newPass)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            return Content("ghgs");
        }

        //lich su doc
        public ActionResult History()
        {
            var str = Request.Cookies["history"]?.Value;

            var history = new Dictionary<string, string> { };

            if (str != null)
            {
                history = MyConvert.StringToListDictionary(str);
            }

            ViewBag.History = history;
            return View();
        }

        //dang xuat
        public ActionResult Logout()
        {
            SessionUser.SetSession(null);
            return Redirect(Url.Action("Index", "Home"));
        }

        //from dang nhap
        public ActionResult Login()
        {
            if (!CheckStatusUser())
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //dang nhap
        [HttpPost]
        public ActionResult Login(String mail, String password)
        {
            if (SessionUser.GetSession() != null)
            {
                return RedirectToAction("Index");
            }

            UserDAO userDao = new UserDAO();
            var user = userDao.Login(mail, password);

            if (user != null)
            {
                SessionUser.SetSession(user);

                return Redirect(Url.Action("Index", "User"));
            }

            ViewBag.Mss = "Thông tin tài khoản hoặc mật khẩu không chính xác";
            return Redirect(Url.Action("Login", "User"));
        }

        //tat ca chuyen
        public ActionResult ListAllComic()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (CheckAdmin())
            {
                int page = Convert.ToInt32(Request["page"]);

                if (page <= 0)
                {
                    page = 1;
                }

                ComicDAO comicDao = new ComicDAO();
                var list = comicDao.List(new Pagination(10, page));

                StatusComicDAO statusComicDao = new StatusComicDAO();
                var statusComics = statusComicDao.ListAll();

                ViewBag.Comics = list.Comics;
                ViewBag.Page = list.Page;
                ViewBag.Numpage = list.PageSize;
                ViewBag.StatusComics = statusComics;

                return View();
            }

            return Redirect(Url.Action("Index", "User"));
        }

        //kiem duyet truyen
        public ActionResult Censorship()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (CheckAdmin())
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

            return Redirect(Url.Action("Index", "User"));
        }

        //thong qua truyen
        [HttpPost]
        public Boolean UpdateCensorship()
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                String id = Request["id"];

                ComicDAO comicDao = new ComicDAO();

                var comic = comicDao.GetComicAs(Convert.ToInt32(id)).Result;
                comic.StatusComicId = 1;
                var n = comicDao.Update(comic);

                return n == 1;
            }

            return false;
        }

        //xoa truyen
        [HttpPost]
        public Boolean DeleteComic()
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (!CheckViewer())
            {
                comic comic = new comic();
                comic.user = _user;
                comic.ComicId = Convert.ToInt32(Request["id"]);

                ComicDAO comicDao = new ComicDAO();

                var n = comicDao.Delete(comic);

                return n > 0;
            }

            return false;
        }

        //hiển thị và thêm mới chapter
        public ActionResult Chapter(String comicId)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                int id = Convert.ToInt32(comicId);

                ChapterDAO chapterDao = new ChapterDAO();
                var chapters = chapterDao.ListChapterComic(id);

                Messenger messenger = new Messenger();
                messenger.Code = 3;

                ViewBag.Chapters = chapters;
                ViewBag.Mss = messenger;

                return View();
            }

            return RedirectToAction("Index");
        }

        //them moi chapter
        [HttpPost]
        public ActionResult Chapter(String namechapter, HttpPostedFileBase[] files, String comicId)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                chapter chapter = new chapter();
                chapter.NameChapter = namechapter;
                chapter.ComicId = Convert.ToInt32(comicId);
                int userId = _user.UserId;

                ChapterDAO chapterDao = new ChapterDAO();
                var c = chapterDao.Add(chapter, userId);

                Messenger mss = new Messenger();

                if (c != null)
                {
                    String path = String.Format("~{0}", c.FolderImage);
                    path = Server.MapPath(path);

                    int s = 0;
                    int f = 0;

                    for (int i = 0; i < files.Length; i++)
                    {
                        Messenger messenger = UploadFile.Upload(files[i], path, String.Format("{0}.jpg", i));
                        if (messenger.Code == 1)
                        {
                            s++;
                        }
                        else
                        {
                            f++;
                        }
                    }

                    mss.Mss = String.Format("Tải lên thành công {0}, thất bại {1}", s, f);
                    mss.Code = 1;
                }
                else
                {
                    mss.Code = 0;
                }

                var chapters = chapterDao.ListChapterComic(Convert.ToInt32(comicId));

                ViewBag.Mss = mss;
                ViewBag.Chapters = chapters;

                return View();
            }

            return View();
        }


        //xóa chapter
        public Boolean DeleteChapter()
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (!CheckViewer())
            {
                chapter chapter = new chapter();
                chapter.ChapterId = Convert.ToInt32(Request["id"]);

                int userId = _user.UserId;

                ChapterDAO chapterDao = new ChapterDAO();

                var n = chapterDao.Delete(chapter, userId);

                return (n == 1);
            }

            return false;
        }

        //thể loại
        public ActionResult Category()
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (CheckAdmin())
            {
                CategoryDAO categoryDao = new CategoryDAO();
                var categorys = categoryDao.List();

                ViewBag.Categories = categorys;
                ViewBag.Mss = new Messenger();

                return View();
            }


            return RedirectToAction("Index");
        }

        //thêm thể lọai
        [HttpPost]
        public ActionResult Category(String namecategory)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (CheckAdmin())
            {
                CategoryDAO categoryDao = new CategoryDAO();
                var b = categoryDao.Add(namecategory);
                var categorys = categoryDao.List();


                Messenger messenger = new Messenger();

                if (b == 1)
                {
                    messenger.Code = 1;
                    messenger.Mss = "Thêm mới thành công";
                }
                else
                {
                    messenger.Code = 0;
                    messenger.Mss = "Thêm mới thất bại";
                }

                ViewBag.Mss = messenger;
                ViewBag.Categories = categorys;


                return View();
            }

            return RedirectToAction("Index");
        }

        //xóa thể loại
        [HttpPost]
        public Boolean DeleteCategory(String id)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                CategoryDAO categoryDao = new CategoryDAO();
                var b = categoryDao.Delete(Convert.ToInt32(id));

                return b;
            }

            return false;
        }

        //sửa tên thể laoi
        [HttpPost]
        public Boolean EditCategory(int id, String name)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                CategoryDAO categoryDao = new CategoryDAO();
                var b = categoryDao.Edit(id, name);

                return b;
            }

            return false;
        }

        //thay đôi trang thái truyện
        [HttpPost]
        public Boolean ChangeStatusComic(int id, int stt)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                ComicDAO comicDao = new ComicDAO();
                Boolean b = comicDao.ChangeStatusComic(id, stt);

                return b;
            }

            return false;
        }
    }
}