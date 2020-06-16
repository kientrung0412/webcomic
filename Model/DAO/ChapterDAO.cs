using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using Model.Models;

namespace Model.DAO
{
    public class ChapterDAO
    {
        public WCDbContext WcDbContext;

        public ChapterDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public chapter Add(chapter chapter, int userId)
        {
            try
            {
                var comic = WcDbContext.comics.Single(c =>
                    c.UserId == userId && c.user.RoleId < 3 && c.StatusComicId < 4 && c.ComicId == chapter.ComicId);
                if (comic != null)
                {
                    chapter.NumChapter = WcDbContext.chapters.Where(chapter1 => chapter1.ComicId == chapter.ComicId)
                        .Max(chapter1 => chapter1.NumChapter) + 1;

                    //thêm chapter
                    var ct = WcDbContext.chapters.Add(chapter);

                    //Cập nhật time
                    comic.UpdateAt = DateTime.Now;

                    ct.FolderImage = String.Format("/Upload/truyen/{0}/{1}_{2}", ct.ComicId,
                        ct.NameChapter.Replace(" ", "_"), DateTime.Now.ToString("ddMMyy_hhmm"));

                    WcDbContext.SaveChanges();

                    return ct;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public int Delete(chapter ct, int userId)
        {
            var chapter =
                WcDbContext.chapters.Single(c => c.ChapterId == ct.ChapterId && c.comic.UserId == userId);

            var sql = WcDbContext.chapters.Remove(chapter);

            var n = WcDbContext.SaveChanges();

            return n;
        }

        public void UpdateView(int id)
        {
            var chapter = WcDbContext.chapters.Single(c => c.ChapterId == id);
            int? a = chapter.View;
            chapter.View = a + 1;
            WcDbContext.SaveChanges();
        }

        public Boolean SortChapter(int[] sort, int userId)
        {
            for (int i = 0; i < sort.Length; i++)
            {
                int id = sort[i];
                var ct = WcDbContext.chapters.Single(c => c.ChapterId == id && c.comic.UserId == userId);
                ct.NumChapter = i;
            }

            var n = WcDbContext.SaveChanges();

            return n >0;
        }

        public chapter Select(int id)
        {
            var chapter = WcDbContext.chapters.Single(c => c.ChapterId == id && c.comic.StatusComicId < 4);
            return chapter;
        }

        public List<chapter> ListChapterComic(int id)
        {
            var chapters = WcDbContext.chapters.Where(c => c.ComicId == id).OrderBy(c => c.NumChapter)
                .ToList();
            return chapters;
        }
    }
}