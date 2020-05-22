using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace WebComic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileBase)
        {
            if (fileBase.ContentLength > 0)
            {
                String fileName = String.Format("{0}", DateTime.Now);
                String filePath = Server.MapPath("~/Upload");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                try
                {
                    fileBase.SaveAs(Path.Combine(filePath, fileName));
                    ViewBag.Message = "Tải lên thành công!";
                    return View();
                }
                catch (Exception e)
                {
                    ViewBag.Message = String.Format("Tải lên thất bại!, {0}", e);
                    return View();
                }
            }
            else
            {
                ViewBag.Message = String.Format("Có lỗi tải lên");
                return View();
            }
        }
    }
}