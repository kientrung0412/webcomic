using System.Collections.Generic;
using System.Linq;
using Model.EF;

namespace Model.DAO
{
    public class NationDAO
    {
        public WCDbContext WcDbContext;

        public NationDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public List<nation> List()
        {
            var list = WcDbContext.nations.ToList();
            return list;
        }

        public int Add(nation nation)
        {
            var sql = WcDbContext.nations.Add(nation);
            var n = WcDbContext.SaveChanges();
            return n;
        }
    }
}