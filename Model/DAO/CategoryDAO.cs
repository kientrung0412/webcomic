using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using Model.Models;

namespace Model.DAO
{
    public class CategoryDAO
    {
        public WCDbContext WcDbContext;

        public CategoryDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public async Task<int> AddAs(String name)
        {
            category category = new category();
            WcDbContext.categories.Add(category);
            var n = await WcDbContext.SaveChangesAsync();
            return n;
        }


        //Sử lý datatable
        public List<category> List()
        {
            var list = WcDbContext.categories.OrderBy(c => c.CategoryId).ToList();
            return list;
        }

        public List<category> ListSearch(String s)
        {
            var stringSearch = String.Format("%{0}%", s.ToLower());


            var list = WcDbContext.categories
                .Where(c => DbFunctions.Like(c.NameCategory, stringSearch))
                .OrderBy(c => c.CategoryId)
                .ToList();

            return list;
        }

        public category One(int id)
        {
            var ctg = WcDbContext.categories.Single(category => category.CategoryId == id);
            return ctg;
        }
    }
}