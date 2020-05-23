using System;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace WebComic.Controllers
{
    public class UploadFileController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file)
        {
            
            String path = Server.MapPath("~/Upload/Avatar");
            UploadFile uploadFile = new UploadFile();

            var a = file;
            
            var fileUpload = uploadFile.Upload(file, path);
            if (fileUpload.Code == 1)
            {
                ViewBag.Mss = fileUpload.Mss;
            }
            else
            {
                ViewBag.Mss = fileUpload.Mss;
            }

            return Content("ok");
        }

        [HttpPost]
        public ActionResult UploadChapterImage(HttpPostedFileBase[] files)
        {
            String path = Server.MapPath("~/Upload/Truyen");
            UploadFile uploadFile = new UploadFile();

            int numFile = files.Length;
            int numDone = 0;


            foreach (HttpPostedFileBase file in files)
            {
                var fileUpload = uploadFile.Upload(file, path);
                if (fileUpload.Code == 1)
                {
                    numDone++;
                }
            }

            int numFall = numFile - numDone;

            return Content(String.Format("Thành công {0}, thất bại {1}", numDone, numFall));
        }
    }
}