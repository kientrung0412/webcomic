using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<role>> List()
        {
            var list = await WcDbContext.roles.ToListAsync();
            return list;
        }
    }
}