﻿using System;
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
            var list = WcDbContext.categories
                .Where(c => c.NameCategory.ToLower().Contains(s.ToLower()) || c.CategoryId.ToString().Contains(s))
                .OrderBy(c => c.CategoryId)
                .ToList();

            return list;
        }
    }
}