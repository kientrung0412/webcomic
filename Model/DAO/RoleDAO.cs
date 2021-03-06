﻿using System.Collections.Generic;
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

        public List<role> List()
        {
            var list = WcDbContext.roles.ToList();
            return list;
        }
    }
}