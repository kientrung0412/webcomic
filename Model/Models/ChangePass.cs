using System;

namespace Model.Models
{
    public class ChangePass
    {
        private int userId;
        private String oldPass;
        private String newPass;

        public ChangePass(int userId, string oldPass, string newPass)
        {
            this.userId = userId;
            this.oldPass = oldPass;
            this.newPass = newPass;
        }

        public int UserId
        {
            get => userId;
            set => userId = value;
        }

        public string OldPass
        {
            get => oldPass;
            set => oldPass = value;
        }

        public string NewPass
        {
            get => newPass;
            set => newPass = value;
        }
    }
}