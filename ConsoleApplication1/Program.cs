using Model.DAO;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CategoryDAO categoryDao =new CategoryDAO();
            categoryDao.List();
        }
    }
}