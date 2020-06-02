using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Configuration;
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
            var chapter = chapterDao.Select(chapterId);

            ViewBag.Chapter = chapter;

            if (Request.Cookies["history"] != null)
            {
                var json = Request.Cookies["history"].Value;
                // JObject o = JObject.Parse(json);
                // JArray a = (JArray)o[0];
                // JToken[] list = a.ToArray();
            }
            else
            {
                ListDictionary history = new ListDictionary();
                history.Add(chapter.ComicId.ToString(), DateTime.Now.ToString("{0:dd/MM/yyyy hh:mm }"));

                var cookie = new HttpCookie("history");
                cookie.Expires = DateTime.Now.AddDays(30);

                var binFormatter = new BinaryFormatter();
                var mStream = new MemoryStream();
              

                // cookie.Value = s;

                Response.AppendCookie(cookie);
            }


            // ViewBag.Title  =  

            return View();
        }
    }
}