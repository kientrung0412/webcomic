using System.Collections.Generic;
using System.Linq;
using Model.EF;

namespace Model.DAO
{
    public class StatusComicDAO
    {
        public WCDbContext WcDbContext;

        public StatusComicDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public List<status_comic> ListUser()
        {
            var list = WcDbContext.status_comic.Where(comic => comic.StatusComicId < 4)
                .OrderBy(comic => comic.StatusComicId).ToList();
            return list;
        }
    }
}