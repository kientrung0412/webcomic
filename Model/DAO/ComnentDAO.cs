using System.Collections.Generic;
using System.Linq;
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

        public List<comment> List(int chapterId)
        {
            var list = WcDbContext.comment.Where(comment => comment.StatusCommentId == 1 && comment.ChapterId == chapterId).ToList();
            return list;
        }
    }
}