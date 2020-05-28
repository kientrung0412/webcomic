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


        public PaginationComic ListPage(Pagination pagination)
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

            var sql = WcDbContext.comics.OrderByDescending(c => c.UpdateAt).Skip(skip).Take(size).ToList();

            PaginationComic paginationComic = new PaginationComic(sizePage, page, sql);
            return paginationComic;
        }


#pragma warning disable 1998
        public async Task<comic> GetComicAs(int comicId)
#pragma warning restore 1998
        {
            var comic = WcDbContext.comics.Single(c => c.ComicId == comicId);
            return comic;
        }

        public List<comic> SearchAdvanced()
        {
            var list = WcDbContext.comics.SqlQuery(
                    " select comic.ComicId, NameComic, CommicBanner from comic join (select ComicId from comic_category where CategoryId in (1, 4) group by ComicId having COUNT(CategoryId) = 2 EXCEPT select ComicId from comic_category where CategoryId in (3) group by ComicId having COUNT(CategoryId) = 1) t1 on comic.ComicId = t1.ComicId where StatusComicId < 4 ")
                .ToList();
            return list;
        }

        public List<comic> SlideComic()
        {
            var list = WcDbContext.comics.OrderBy(comic => comic.ComicId).Take(10).ToList();
            return list;
        } 
        
        public List<comic> NewComic()
        {
            var list = WcDbContext.comics.OrderBy(comic => comic.ReleaseDate).Take(12).ToList();
            return list;
        }
        
    }
}