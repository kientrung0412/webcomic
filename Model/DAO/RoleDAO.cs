using System.Collections.Generic;
using System.Linq;
using Model.EF;

namespace Model.DAO
{
    public class RoleDAO
    {
        public WCDbContext WcDbContext;

        public RoleDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public List<role> List()
        {
            var list = WcDbContext.roles.ToList();
            return list;
        }
    }
}