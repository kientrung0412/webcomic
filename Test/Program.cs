using System;
using Model.DAO;
using Model.Models;

namespace Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            ComicDAO comicDao = new ComicDAO();
           var a = comicDao.ListPg(new Pagination(1, 1));
        }
    }
}