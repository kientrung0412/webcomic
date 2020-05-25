using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Model.DAO;
using Model.EF;


namespace test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            String a = "CategoryId";
            category category = new category();
            Console.WriteLine(nameof(category) );
        }

    }
}