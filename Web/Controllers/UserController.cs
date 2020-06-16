using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_an.Controllers.ultis;
using Model.DAO;
using Model.EF;
using Model.Models;
using Newtonsoft.Json;
using Web.Models;

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

                if (CheckAdmin())
                {
                    comic.StatusComicId = 1;
                }

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
        public int ChangePass(String oldPass, String newPass, String reNewPass)
        {
            if (CheckStatusUser())
            {
                //Chưa đăng nhập hoặc đã bị ban
                return 0;
            }

            if (reNewPass.Trim().Equals(newPass.Trim()))
            {
                if (newPass.Trim().Length > 4)
                {
                    var b = new UserDAO().ChangePass(new ChangePass(_user.UserId, oldPass, newPass));

                    //b=-4 maạt khẩu không chính xác
                    //b=1 thành công
                    return b;

                }

                //mật khẩu ngắn hơn 4 ký tự
                return -3;
            }

            //Hai mật khẩu không khớp
            return -2;
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

            ViewBag.Mss = null;

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
            return View();
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
        public Boolean UpdateCensorship(int id)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                ComicDAO comicDao = new ComicDAO();

                var comic = comicDao.UpdateCensorship(id);

                return comic;
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

        //hien thi danh sach user
        public ActionResult Users()
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

                UserDAO userDao = new UserDAO();
                RoleDAO roleDao = new RoleDAO();
                StatusUserDAO statusUserDao = new StatusUserDAO();

                var list = userDao.Users(new Pagination(10, page));

                ViewBag.Users = list.Comics;
                ViewBag.Page = list.Page;
                ViewBag.Numpage = list.PageSize;
                ViewBag.Roles = roleDao.List();
                ViewBag.StatusUsers = statusUserDao.List();

                return View();
            }

            return RedirectToAction("Index");
        }

        //thay doi trang thai user
        [HttpPost]
        public Boolean ChangeSttAndRoles(int roleId, int sttId, int id)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                user user = new user();
                user.RoleId = roleId;
                user.StatusUserId = sttId;
                user.UserId = id;

                UserDAO userDao = new UserDAO();

                var n = userDao.Update(user);

                return (n > 0);
            }

            return false;
        }

        //Đăng bình luận

        [HttpPost]
        public String Comment(int chapterId, String content)
        {
            if (CheckStatusUser())
            {
                return null;
            }

            comment comment = new comment();

            comment.ChapterId = chapterId;
            comment.CommentConten = content;
            comment.UserId = _user.UserId;


            CommentDAO commentDao = new CommentDAO();

            var cmt = commentDao.Add(comment);

            var json = JsonConvert.SerializeObject(cmt);

            return json;
        }

        //doi trang thai binh luan
        [HttpPost]
        public Boolean ChangeStatusCmt(int id)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {
                CommentDAO commentDao = new CommentDAO();

                var b = commentDao.ChangeStatus(id);

                return b;
            }

            return false;
        }

        //form sửa chuyện

        public ActionResult UpdateComic(int comicId)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                ViewBag.Comics = new ComicDAO().GetComic(comicId);
                ViewBag.Nations = new NationDAO().List();
                ViewBag.Categories = new CategoryDAO().List();
                ViewBag.Mss = -1;

                return View();
            }

            return RedirectToAction("Index");
        }

        //sửa chuyện

        [HttpPost]
        public ActionResult UpdateComic(String nameComic, String author, int[] category, int nation,
            HttpPostedFileBase file, String summary, int comicId)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckViewer())
            {
                ComicDAO comicDao = new ComicDAO();

                comic comic = new comic();
                comic.ComicId = comicId;
                comic.NameComic = nameComic;
                comic.AuthorComic = author;
                comic.NationId = nation;
                comic.SummaryComic = summary;
                comic.UserId = _user.UserId;

                var b = comicDao.Update(comic, category);

                ViewBag.Mss = -1;

                if (file != null)
                {
                    String path = String.Format("~/Upload/Truyen/{0}", comicId);
                    path = Server.MapPath(path);
                    var messenger = UploadFile.Upload(file, path, "baner.jpg");
                    ViewBag.Mss = messenger.Code;
                }

                ViewBag.Comics = new ComicDAO().GetComic(comicId);
                ViewBag.Nations = new NationDAO().List();
                ViewBag.Categories = new CategoryDAO().List();

                // return RedirectToAction("UpdateComic", new {comicId = comicId});
                return View();
            }


            return RedirectToAction("Index");
        }

        //Form quyên
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //quen mat khau
        [HttpPost]
        public String ForgotPassword(String email, String username)
        {
            UserDAO userDao = new UserDAO();

            if (new CheckEmail().isEmail(email))
            {
                var pass = userDao.ForgotPassword(email, username);
                if (pass != null)
                {
                    MailHelper mailHelper = new MailHelper();
                    mailHelper.SendMail(email, "Mật khẩu mới Của bạn là", pass);

                    return true.ToString();
                }
            }

            return false.ToString();
        }

        //dăng ký
        public ActionResult Registration()
        {
            return View();
        }

        //send code
        public int SendCode(String email)
        {
            var delayTime = 0;

            if (Session["code"] != null)
            {
                var now = ((DateTime) Session["time"]);
                delayTime = Convert.ToInt32((DateTime.Now - now).TotalMinutes);
            }

            if (Session["code"] == null || delayTime > 1)
            {
                if (new CheckEmail().isEmail(email))
                {
                    if (new UserDAO().CheckMail(email) < 1)
                    {
                        Random random = new Random();
                        String code = random.Next(100000, 999999).ToString();

                        MailHelper mailHelper = new MailHelper();
                        var send = mailHelper.SendMail(email, "Mã xác thực của bạn là", code);


                        Session["code"] = code;
                        Session["email"] = email;
                        Session["time"] = DateTime.Now;

                        return Convert.ToInt32(send);
                    }

                    // mail đã dùng
                    return -6;
                }

                //không khớp định dạng mail
                return -1;
            }

            //vùa gửi mail xong
            return -2;
        }

        //đăng ký
        [HttpPost]
        public int Registration(String email, String username, String password, String rePassword, String code)
        {
            if (Session["code"] != null)
            {
                if (Session["code"].Equals(code))
                {
                    if (Session["email"].Equals(email))
                    {
                        if (password.Equals(rePassword))
                        {
                            if (password.Trim().Length > 4)
                            {
                                if (username.Trim().Length > 2)
                                {
                                    UserDAO userDao = new UserDAO();
                                    user user = new user();
                                    user.UserMail = email;
                                    user.UserPass = StringToMd5.GetMd5Hash(password);
                                    user.Username = username;

                                    var a = userDao.Registration(user);

                                    if (a)
                                    {
                                        Session["code"] = null;
                                        Session["email"] = null;
                                    }

                                    return Convert.ToInt32(a);
                                }

                                // user quá ngắn
                                return -8;
                            }

                            //mật khẩu quá ngắn
                            return -7;
                        }

                        //Hai mật khẩu không khớp
                        return -5;
                    }

                    //Không đúng mail
                    return -4;
                }

                //mã code ko khớp
                return -2;
            }

            //Chưa có mã
            return -3;
        }

        //thay đổi avatar
        [HttpPost]
        public ActionResult ChangerAvatar(HttpPostedFileBase file)
        {
            if (file != null)
            {
                String filePath = Server.MapPath("~/Upload/Avatar");
                String fileName = String.Format("{0}.jpg", _user.UserId);

                var a = UploadFile.Upload(file, filePath, fileName);
            }

            return RedirectToAction("Index");
        }
        
        //Quản lý cmt ẩn

        public ActionResult ListComment(int page = 1)
        {
            if (CheckStatusUser())
            {
                return RedirectToAction("Login");
            }

            if (!CheckAdmin())
            {
                return RedirectToAction("Index");
            }

            if (page < 1)
            {
                page = 1;
            }
            
            var list = new CommentDAO().ListHide(new Pagination(10,page));

            ViewBag.Cmts = list.Comments;
            ViewBag.Numpage = list.PageSize;
            ViewBag.Page = list.Page;

            return View();
        }
        
        //delete cmt
        [HttpPost]
        public Boolean DeleteCmt(int id)
        {
            if (CheckStatusUser())
            {
                return false;
            }

            if (CheckAdmin())
            {

                var b = new CommentDAO().Delete(id);
                
                return b;
            }

            return false;
        }
        
    }
}