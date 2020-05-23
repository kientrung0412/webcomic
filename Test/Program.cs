using System;
using Model.DAO;
using Model.EF;
using Model.Models;

namespace Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DocumentDAO documentDao = new DocumentDAO();


            Console.WriteLine(documentDao.DeleteAs(1374).Result);
        }
    }
}