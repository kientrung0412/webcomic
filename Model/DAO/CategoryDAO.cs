using System.Collections.Generic;
using System.Linq;
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

        public List<category> List()
        {
            var list = WcDbContext.category.ToList();
            return list;
        }

        public int Add(category category)
        {
            var sql = WcDbContext.category.Add(category);
            var n = WcDbContext.SaveChanges();
            return n;
        }
    }
}