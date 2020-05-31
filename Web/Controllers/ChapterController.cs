using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebComic.Controllers
{
    public class ChapterController : Controller
    {
        // GET
        public ActionResult Index(int chapterId)
        {
            ChapterDAO chapterDao = new ChapterDAO();
            chapterDao.UpdateView(chapterId);

            if (Request.Cookies["history"] != null)
            {
                var json = Request.Cookies["history"].Value;
                // JObject o = JObject.Parse(json);
                // JArray a = (JArray)o[0];
                // JToken[] list = a.ToArray();
            }
            else
            {
                NameValueCollection ints = new NameValueCollection();

                ints.Add(chapterId.ToString(),DateTime.Now.ToString("{0:dd/MM/yyyy hh:mm }"));

                var cookie = new HttpCookie("history");
                cookie.Values.Add(ints);
                
                
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.AppendCookie(cookie);
            }

            ViewBag.Data = chapterDao.Select(chapterId);

            // ViewBag.Title  =  

            return View();
        }
    }
}