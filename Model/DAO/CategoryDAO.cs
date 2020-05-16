using System.Collections.Generic;
using System.Linq;
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

        public List<category> List()
        {
            var list = WcDbContext.categories.ToList();
            return list;
        }

        public int Add(category category)
        {
            var sql = WcDbContext.categories.Add(category);
            var n = WcDbContext.SaveChanges();
            return n;
        }
    }
}