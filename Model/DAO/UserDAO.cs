using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<SelectUserAll> ListUser()
        {
            var list = (from user in WcDbContext.users
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
                }).ToList();
            return list;
        }

        public user oneUser(int userId)
        {
            user sql = WcDbContext.users.Single(u => u.UserId == userId);
            return sql;
        }

        public int update(user user)
        {
            // String sql = String.Format("exec UpdateSttUser @UserId = {0}, @Username = {1}, @SttId = {2}, @RoleId = {3}",
            //     user.UserId, user.Username, user.StatusUserId, user.RoleId);
            // var n = WcDbContext.Database.ExecuteSqlCommand(sql);

            user sql = WcDbContext.users.Single(u => u.UserId == user.UserId);

            sql.Username = user.Username;
            sql.RoleId = user.RoleId;
            sql.StatusUserId = user.StatusUserId;

            var n = WcDbContext.SaveChanges();

            return n;
        }


        public int changePass(ChangePass changePass)
        {
            user user = oneUser(changePass.UserId);
            String oldPass = StringToMd5.GetMd5Hash(changePass.OldPass);

            if (user.UserPass == oldPass)
            {
                user.UserPass = StringToMd5.GetMd5Hash(changePass.NewPass);
                var n = WcDbContext.SaveChanges();
                return n;
            }
            else
            {
                //mật khẩu cũ không khóp
                return -1;
            }
        }

        public int signUp(user user)
        {
            var sql = WcDbContext.users.Add(user);
            var n = WcDbContext.SaveChanges();
            return n;
        }
    }
}