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
    }
}