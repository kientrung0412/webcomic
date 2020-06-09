using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace WebComic.Controllers
{
    public class ChapterController : Controller
    {
        // GET
        public ActionResult Index(int chapterId)
        {
            int page = Convert.ToInt32(Request["page"]);
            
            if (page < 1)
            {
                page = 1;
            }

            ChapterDAO chapterDao = new ChapterDAO();
            CommentDAO commentDao = new CommentDAO();

            chapterDao.UpdateView(chapterId);

            var chapter = chapterDao.Select(chapterId);
            var comicId = chapter.comic.ComicId;
            var now = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

            var list = commentDao.List(new Pagination(7, page), chapterId);

            if (Request.Cookies["history"] != null)
            {
                var str = Request.Cookies["history"].Value;
                var history = MyConvert.StringToListDictionary(str);
                var b = history.ContainsKey(comicId.ToString());

                if (b)
                {
                    history[chapter.comic.ComicId.ToString()] = now;
                }
                else
                {
                    history.Add(comicId.ToString(), now);
                }

                var strCookie = MyConvert.ListDictionaryToString(history);

                var cookie = new HttpCookie("history");
                cookie.Expires = DateTime.Now.AddDays(30);

                cookie.Value = strCookie;

                Response.AppendCookie(cookie);
            }
            else
            {
                Dictionary<string, string> history = new Dictionary<string, string>();
                history.Add(comicId.ToString(), now);

                var str = MyConvert.ListDictionaryToString(history);

                var cookie = new HttpCookie("history");
                cookie.Expires = DateTime.Now.AddDays(30);

                cookie.Value = str;

                Response.AppendCookie(cookie);
            }


            ViewBag.Title = String.Format("{0} {1}", chapter.comic.NameComic, chapter.NameChapter);
            ViewBag.Home = comicId;
            ViewBag.Chapter = chapter;
            ViewBag.Chapters = chapterDao.ListChapterComic(Convert.ToInt32(chapter.ComicId));
            ViewBag.Comments = list.Comments;
            ViewBag.Page = list.Page;
            ViewBag.Numpage = list.PageSize;

            return View();
        }
    }
}