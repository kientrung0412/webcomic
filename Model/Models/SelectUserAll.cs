using System;

namespace Model.Models
{
    public class SelectUserAll
    {
        private int userId;
        private String username;
        private String roleName;
        private int roleId;
        private String avatar;
        private String statusUserName;
        private int statusUserId;

        public int UserId
        {
            get => userId;
            set => userId = value;
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string RoleName
        {
            get => roleName;
            set => roleName = value;
        }

        public int RoleId
        {
            get => roleId;
            set => roleId = value;
        }

        public string Avatar
        {
            get => avatar;
            set => avatar = value;
        }

        public string StatusUserName
        {
            get => statusUserName;
            set => statusUserName = value;
        }

        public int StatusUserId
        {
            get => statusUserId;
            set => statusUserId = value;
        }
    }
}