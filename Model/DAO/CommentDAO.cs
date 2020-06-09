using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class CommentDAO
    {
        public WCDbContext WcDbContext;

        public CommentDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public Boolean Add(comment comment)
        {
            var cmt = WcDbContext.comments.Add(comment);
            var n = WcDbContext.SaveChanges();

            return n > 0;
        }

        public List<comment> List(int chapterId)
        {
            var list = WcDbContext.comments
                .Where(comment => comment.StatusCommentId == 1 && comment.ChapterId == chapterId &&
                                  comment.user.StatusUserId == 1)
                .OrderByDescending(comment => comment.CommentTime).ToList();
            return list;
        }
    }
}