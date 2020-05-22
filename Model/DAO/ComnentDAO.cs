using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using up_down.Models;

namespace Model.DAO
{
    public class ComnentDAO
    {
        public WCDbContext WcDbContext;

        public ComnentDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public async Task<List<comment>> ListAs(int chapterId)
        {
            var list = await WcDbContext.comments.Where(comment => comment.StatusCommentId == 1 && comment.ChapterId == chapterId).ToListAsync();
            return list;
        }
    }
}