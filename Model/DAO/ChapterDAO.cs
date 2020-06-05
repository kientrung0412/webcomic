using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class ChapterDAO
    {
        public WCDbContext WcDbContext;

        public ChapterDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public chapter Add(chapter chapter)
        {
            var c = WcDbContext.chapters.Add(chapter);
            WcDbContext.SaveChanges();
            
            c.FolderImage = String.Format("/Upload/truyen/{0}/{1}", c.ComicId, c.ChapterId);
            var comic = WcDbContext.comics.Single(comic1 => comic1.ComicId == c.ComicId);
            comic.UpdateAt = DateTime.Now;

            WcDbContext.SaveChanges();

            return c;
        }

        public async Task<int> DeleteAs(int id)
        {
            var select = await WcDbContext.chapters.FindAsync(id);

            var sql = WcDbContext.chapters.Remove(select);

            var n = await WcDbContext.SaveChangesAsync();

            return n;
        }

        public void UpdateView(int id)
        {
            var chapter = WcDbContext.chapters.Single(c => c.ChapterId == id);
            int? a = chapter.View;
            chapter.View = a + 1;
            WcDbContext.SaveChanges();
        }

        public chapter Select(int id)
        {
            var chapter = WcDbContext.chapters.Single(c => c.ChapterId == id);
            return chapter;
        }

        public List<chapter> ListChapterComic(int id)
        {
            var chapters = WcDbContext.chapters.Where(c => c.ComicId == id).OrderBy(c => c.ChapterId)
                .ToList();
            return chapters;
        }
    }
}