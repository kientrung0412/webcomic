using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<nation>> ListAs()
        {
            var list = await WcDbContext.nations.ToListAsync();
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