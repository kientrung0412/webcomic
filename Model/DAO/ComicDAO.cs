using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model.EF;
using Model.Models;

namespace Model.DAO
{
    public class ComicDAO
    {
        public WCDbContext WcDbContext;

        public ComicDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public int rating(Rating rating)
        {
            comic comic = WcDbContext.comics.Single(c => c.ComicId == rating.ComicId);
            var numRating = comic.NumRating;

            comic.Rating = ((comic.Rating * numRating) + rating.Point) / numRating++;
            comic.NumRating = numRating++;

            var n = WcDbContext.SaveChanges();

            return n;
        }

        public int Add(comic comic)
        {
            var sql = WcDbContext.comics.Add(comic);
            var n = WcDbContext.SaveChanges();

            return n;
        }

        public PaginationComic ListPg(Pagination pagination)
        {
            int page = pagination.Page;
            int size = pagination.Size;

            int skip = (page - 1) * size;

            int sizePage = WcDbContext.comics.Count();

            var sql = WcDbContext.comics.OrderBy(c => c.ComicId).Skip(skip).ToList();

            PaginationComic paginationComic = new PaginationComic(sizePage, page, sql);
            return paginationComic;
        }
    }
}