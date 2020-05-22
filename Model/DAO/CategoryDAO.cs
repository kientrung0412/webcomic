using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using up_down.Models;

namespace Model.DAO
{
    public class CategoryDAO
    {
        public WCDbContext WcDbContext;

        public CategoryDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public async Task<List<category>> ListAs()
        {
            var list = await WcDbContext.categories.ToListAsync();
            return list;
        }

        public async Task<int> AddAs(category category)
        {
            WcDbContext.categories.Add(category);
            var n = await WcDbContext.SaveChangesAsync();
            return n;
        }
    }
}