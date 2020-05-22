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

        // public ActionResult Index(HttpPostedFileBase file )
        // {
        //     if (file.ContentLength > 0)
        //     {
        //         var a = file.InputStream.Position;
        //         
        //         String fileName = String.Format("{0}.jpg", DateTime.Now.ToString("ddMMyy_hhmmss"));
        //         String filePath = Server.MapPath("~/Upload");
        //
        //         if (!Directory.Exists(filePath))
        //         {
        //             Directory.CreateDirectory(filePath);
        //         }
        //
        //         try
        //         {
        //             file.SaveAs(Path.Combine(filePath, fileName));
        //             ViewBag.Message = "Tải lên thành công!";
        //             return View();
        //         }
        //         catch (Exception e)
        //         {
        //             ViewBag.Message = String.Format("Tải lên thất bại!, {0}", e);
        //             return View();
        //         }
        //     }
        //     else
        //     {
        //         ViewBag.Message = String.Format("Có lỗi tải lên");
        //         return View();
        //     }
        // } 
        public ActionResult Index(HttpPostedFileBase[] files)
        {
            UploadFile uploadFile = new UploadFile();
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