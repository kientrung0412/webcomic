using System.Collections.Generic;
using System.Linq;
using up_down.Models;

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
            var list = WcDbContext.nation.ToList();
            return list;
        }

        public int Add(nation nation)
        {
            var sql = WcDbContext.nation.Add(nation);
            var n = WcDbContext.SaveChanges();
            return n;
        }
    }
}