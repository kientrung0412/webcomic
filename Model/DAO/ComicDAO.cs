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


        public async Task<PaginationComic> ListPageAs(Pagination pagination)
        {
            int page = pagination.Page;
            int size = pagination.Size;

            int skip = (page - 1) * size;

            int sizePage = WcDbContext.comics.Count();

            if (sizePage % size > 0)
            {
                sizePage = sizePage / size + 1;
            }
            else
            {
                sizePage = sizePage / size;
            }

            var sql = await WcDbContext.comics.OrderBy(c => c.ComicId).Skip(skip).Take(size).ToListAsync();

            PaginationComic paginationComic = new PaginationComic(sizePage, page, sql);
            return paginationComic;
        }

        public async Task<comic> GetComicAs(int comicId)
        {
            var comic = WcDbContext.comics.Single(c => c.ComicId == comicId);
            return comic;
        }
    }
}