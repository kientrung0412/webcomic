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

        public async Task<int> AddAs(chapter chapter)
        {
            var sql = WcDbContext.chapters.Add(chapter);
            var n = await WcDbContext.SaveChangesAsync();
            return n;
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