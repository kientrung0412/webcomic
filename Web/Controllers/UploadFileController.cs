using System;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace WebComic.Controllers
{
    public class UploadFileController : Controller
    {
        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file)
        {
            String path = Server.MapPath("~/Upload/Avatar");

            var fileUpload = UploadFile.Upload(file, path, file.FileName);
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
        
    }
}