using Model.EF;

namespace Model.DAO
{
    public class ComicDAO
    {
        public WCDbContext WcDbContext;

        public ComicDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public int Add(comic comic)
        {
            var sql = WcDbContext.comics.Add(comic);
            var n = WcDbContext.SaveChanges();

            return n;
        }
    }
}