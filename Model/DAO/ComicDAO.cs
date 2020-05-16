using System.Collections.Generic;
using up_down.Models;

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
            var sql = WcDbContext.comic.Add(comic);
            var n = WcDbContext.SaveChanges();
            return n;
        }
        
    }
}