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

        public async Task<List<SelectUserAll>> ListUser()
        {
            var list = await (from user in WcDbContext.users
                join statusUser in WcDbContext.status_user on user.StatusUserId equals statusUser.StatusUserId
                join role in WcDbContext.roles on user.RoleId equals role.RoleId
                select new SelectUserAll()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    Avatar = user.Avatar,
                    StatusUserId = statusUser.StatusUserId,
                    StatusUserName = statusUser.StatusUserName
                }).ToListAsync();

            return list;
        }

        //kiểm tra người dùng có trong data base chưa
        public user OneUser(int userId)
        {
            user sql = WcDbContext.users.Single(u => u.UserId == userId);
            return sql;
        }

        public async Task<int> UpdateAS(user user)
        {
            user sql = await WcDbContext.users.SingleAsync(u => u.UserId == user.UserId);

            sql.Username = user.Username;
            sql.RoleId = user.RoleId;
            sql.StatusUserId = user.StatusUserId;

            var n = await WcDbContext.SaveChangesAsync();

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
    }
}