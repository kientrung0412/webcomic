using System;
using System.Text.RegularExpressions;

namespace Web.Models
{
    public class CheckEmail
    {
        public Boolean isEmail(String s)
        {
            Regex regex = new Regex(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$");
            return regex.IsMatch(s);
        }
    }
}