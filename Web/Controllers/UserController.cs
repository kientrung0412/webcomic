using System;
using System.Web.Mvc;
using Model.DAO;
using Model.Models;

namespace WebComic.Controllers
{
    public class UserController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult History()
        {
            var str = Request.Cookies["history"].Value;
            var history = MyConvert.StringToListDictionary(str);

            ViewBag.History = history;
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect(Url.Action("Index", "Home"));
        }

        public ActionResult Login()
        {
            return View();
        }

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
    }
}