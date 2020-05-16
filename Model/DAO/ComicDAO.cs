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
    }
}