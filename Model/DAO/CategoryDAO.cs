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

        public int Add(String name)
        {
            category category = new category();
            category.NameCategory = name;
            WcDbContext.categories.Add(category);

            var n = WcDbContext.SaveChanges();
            return n;
        }


        //Sử lý datatable
        public List<category> List()
        {
            var list = WcDbContext.categories.OrderBy(c => c.CategoryId).ToList();
            return list;
        }

        public category One(int id)
        {
            var ctg = WcDbContext.categories.Single(category => category.CategoryId == id);
            return ctg;
        }

        public Boolean Delete(int id)
        {
            var ctg = WcDbContext.categories.Single(category => category.CategoryId == id);
            WcDbContext.categories.Remove(ctg);

            var i = WcDbContext.SaveChanges();

            return i == 1;
        }
    }
}