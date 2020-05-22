using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.Models;

namespace WebComic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files)
        {
            var p = files.Rank;
            
            UploadFile uploadFile = new UploadFile();
            //đường dẫn lưu file
            String filePath = Server.MapPath("~/Upload");

            int count = files.Length;
            int n = 0;
            
            foreach (HttpPostedFileBase file in files)
            {
                var upload = uploadFile.Upload(file, filePath);
                if (upload.Code == 1)
                {
                    n++;
                }
            }
            
            var a = n;
            
            String mss = String.Format("thành công {0}, thất bại {1}", n, (count - n));
            ViewBag.Message = mss;
            
            
            return View();
        }
    }
}