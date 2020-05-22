using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> RatingAs(Rating rating)
        {
            comic comic = await WcDbContext.comics.SingleAsync(c => c.ComicId == rating.ComicId);
            var numRating = comic.NumRating;

            comic.Rating = ((comic.Rating * numRating) + rating.Point) / numRating++;
            comic.NumRating = numRating++;

            var n = await WcDbContext.SaveChangesAsync();

            return n;
        }

        public async Task<int> AddAs(comic comic)
        {
            var sql = WcDbContext.comics.Add(comic);
            var n = await WcDbContext.SaveChangesAsync();

            return n;
        }


        //***Lưu ý cao
        public async Task<PaginationComic> ListPg(Pagination pagination)
        {
            int page = pagination.Page;
            int size = pagination.Size;

            int skip = (page - 1) * size;

            int sizePage = WcDbContext.comics.Count();

            var sql = await WcDbContext.comics.OrderBy(c => c.ComicId).Skip(skip).Take(size).ToListAsync();

            PaginationComic paginationComic = new PaginationComic(sizePage, page, sql);
            return paginationComic;
        }
    }
}