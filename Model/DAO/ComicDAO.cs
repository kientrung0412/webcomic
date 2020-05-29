using System;
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

        public List<comic> SearchAdvanced(SuperSearch search)
        {
            char c = ',';
            String strIn = "''";
            String strNotIn = "''";
            String sqlJoin;

            int countListIn = search.ListIn.Count;

            if (countListIn != 0)
            {
                strIn = search.ListIn.Aggregate((s, s1) => s + c + s1);
            }


            int countListNotIn = search.ListNotIn.Count;
            if (countListNotIn != 0)
            {
                strNotIn = search.ListNotIn.Aggregate((s, s1) => s + c + s1);
            }

            if (countListIn > 0)
            {
                sqlJoin = String.Format(
                    "join (select ComicId from comic_category where CategoryId in ({0}) group by ComicId having COUNT(CategoryId) = {1} EXCEPT select ComicId from comic_category where CategoryId in ({2}) group by ComicId having COUNT(CategoryId) = {3}) t1 on comic.ComicId = t1.ComicId",
                    strIn, countListIn,
                    strNotIn, countListNotIn
                );
            }
            else
            {
                sqlJoin = String.Format(
                    "join (select ComicId from comic_category where CategoryId in ({0}) group by ComicId having COUNT(CategoryId) = {1}) t1 on comic.ComicId = t1.ComicId",
                    strNotIn, countListNotIn
                );
            }

            if (countListIn ==0 && countListNotIn ==0)
            {
                sqlJoin = "";
            }
            


            String sql = String.Format(
                " select * from comic {0} where StatusComicId < 4 ",
                sqlJoin
            );

            if (search.StatusId > 0)
            {
                sql = sql + String.Format(" AND StatusComicId = {0} ", search.StatusId);
            }

            if (search.NationId > 0)
            {
                sql = sql + String.Format(" AND NationId = {0} ", search.NationId);
            }

            if (search.NameComic != null || !search.NameComic.Trim().Equals(""))
            {
                sql = sql + String.Format(" AND NameComic LIKE '{0}'", search.NameComic);
            }
            
            var list = WcDbContext.comics.SqlQuery(sql).ToList();

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