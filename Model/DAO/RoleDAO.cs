using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<role>> ListAs()
        {
            var list = await WcDbContext.roles.ToListAsync();
            return list;
        }
    }
}