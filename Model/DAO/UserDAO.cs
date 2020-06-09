using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using Model.Models;

namespace Model.DAO
{
    public class UserDAO
    {
        public WCDbContext WcDbContext;

        public UserDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public PaginationUser ListPage(Pagination pagination, IOrderedQueryable<user> users)
        {
            int page = pagination.Page;
            int size = pagination.Size;

            if (page < 1)
            {
                page = 1;
            }

            int skip = (page - 1) * size;

            int sizePage = users.Count();

            if (sizePage % size > 0)
            {
                sizePage = sizePage / size + 1;
            }
            else
            {
                sizePage = sizePage / size;
            }

            var sql = users.Skip(skip).Take(size).ToList();

            PaginationUser paginationUser = new PaginationUser(sizePage, page, sql);
            return paginationUser;
        }

        public PaginationUser Users(Pagination pagination)
        {
            var users = WcDbContext.users.OrderBy(user => user.UserId);
            PaginationUser list = ListPage(pagination, users);

            return list;
        }

        //Lấy ra 1 người dùng
        public user OneUser(int userId)
        {
            user sql = WcDbContext.users.Single(u => u.UserId == userId);
            return sql;
        }

        public int Update(user user)
        {
            user sql = WcDbContext.users.Single(u => u.UserId == user.UserId);
            
            sql.RoleId = user.RoleId;
            sql.StatusUserId = user.StatusUserId;

            var n = WcDbContext.SaveChanges();

            return n;
        }


        public async Task<int> ChangePassAs(ChangePass changePass)
        {
            user user = OneUser(changePass.UserId);
            String oldPass = StringToMd5.GetMd5Hash(changePass.OldPass);

            if (user.UserPass == oldPass)
            {
                user.UserPass = StringToMd5.GetMd5Hash(changePass.NewPass);
                var n = await WcDbContext.SaveChangesAsync();
                return n;
            }
            else
            {
                //mật khẩu cũ không khóp
                return -1;
            }
        }

        //đăng ký
        public int SignUp(user user)
        {
            var a = CheckMail(user.UserMail);

            if (a == 0)
            {
                var sql = WcDbContext.users.Add(user);
                var n = WcDbContext.SaveChanges();
                //cho phép đăng ký
                return n;
            }
            else
            {
                //email đã tồn tại
                return -1;
            }
        }


        public int CheckMail(String mail)
        {
            var a = WcDbContext.users.Where(user => user.UserMail == mail).Count();
            return a;
        }

        public user Login(String email, String password)
        {
            try
            {
                password = StringToMd5.GetMd5Hash(password);
                var user = WcDbContext.users.Single(u => u.UserMail == email && u.UserPass == password);

                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}