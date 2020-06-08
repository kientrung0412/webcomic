using System.Web;
using Model.EF;

namespace Model.Models
{
    public class SessionUser
    {
        public static user GetSession()
        {
            HttpContext context = HttpContext.Current;
            user user = (user) context.Session["user"];

            return user;
        }

        public static void SetSession(user user)
        {
            HttpContext context = HttpContext.Current;
            context.Session["user"] = user;
        }
    }
}