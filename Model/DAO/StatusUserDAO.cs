using System.Collections.Generic;
using System.Linq;
using Model.EF;

namespace Model.DAO
{
    public class StatusUserDAO
    {
        public WCDbContext WcDbContext;

        public StatusUserDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public List<status_user> List()
        {
            var list = WcDbContext.status_user.ToList();

            return list;
        }
    }
}