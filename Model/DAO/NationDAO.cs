using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var list =  WcDbContext.nations.ToList();
            return list;
        }

        public async Task<int> AddAs(nation nation)
        {
            WcDbContext.nations.Add(nation);
            var n = await WcDbContext.SaveChangesAsync();
            return n;
        }
    }
}