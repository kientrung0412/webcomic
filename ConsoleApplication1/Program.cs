using System;
using Model.DAO;
using Model.Models;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UserDAO userDao = new UserDAO();
            var a = userDao.checkMail("a");
            
            Console.Write(a);
        }
    }
}