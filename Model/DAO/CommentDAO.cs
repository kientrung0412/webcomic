using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using Model.Models;

namespace Model.DAO
{
    public class CommentDAO
    {
        public WCDbContext WcDbContext;

        public CommentDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public comment Add(comment comment)
        {
            var cmt = WcDbContext.comments.Add(comment);
            var n = WcDbContext.SaveChanges();

            if (n > 0)
            {
                cmt.user = WcDbContext.users.Single(user => user.UserId == cmt.UserId);

                return cmt;
            }

            return null;
        }

        public PaginationComment List(Pagination pagination, int chapterId)
        {
            var list = WcDbContext.comments
                .Where(comment => comment.StatusCommentId == 1 && comment.ChapterId == chapterId &&
                                  comment.user.StatusUserId == 1)
                .OrderByDescending(comment => comment.CommentTime);

            PaginationComment paginationComment = ListPage(pagination, list);
            return paginationComment;
        }

        public PaginationComment ListPage(Pagination pagination, IOrderedQueryable<comment> comments)
        {
            int page = pagination.Page;
            int size = pagination.Size;

            if (page < 1)
            {
                page = 1;
            }

            int skip = (page - 1) * size;

            int sizePage = comments.Count();

            if (sizePage % size > 0)
            {
                sizePage = sizePage / size + 1;
            }
            else
            {
                sizePage = sizePage / size;
            }

            var sql = comments.Skip(skip).Take(size).ToList();

            PaginationComment paginationComment = new PaginationComment(sizePage, page, sql);
            return paginationComment;
        }

        public Boolean ChangeStatus(int id)
        {
            var comment = WcDbContext.comments.Single(c => c.CommentId == id);
            comment.StatusCommentId = 2;

            var n = WcDbContext.SaveChanges();

            return n > 0;
        }

        public PaginationComment ListHide(Pagination pagination)
        {
            var list = WcDbContext.comments.Where(comment => comment.StatusCommentId == 2)
                .OrderByDescending(comment => comment.CommentTime);
            
            PaginationComment paginationComment = ListPage(pagination, list);
            return paginationComment;
        }
    }
}