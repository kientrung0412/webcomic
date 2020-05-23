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
        
    }
}