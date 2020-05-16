namespace Model.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            comics = new HashSet<comic>();
            comments = new HashSet<comment>();
        }


        public user(int userId, string username, int? roleId, int? statusUserId)
        {
            UserId = userId;
            Username = username;
            RoleId = roleId;
            StatusUserId = statusUserId;
        }

        public user(int userId, string username, string userPass, int? roleId, string userMail, string avatar,
            int? statusUserId)
        {
            UserId = userId;
            Username = username;
            UserPass = userPass;
            RoleId = roleId;
            UserMail = userMail;
            Avatar = avatar;
            StatusUserId = statusUserId;
        }

        public int UserId { get; set; }

        [StringLength(50)] public string Username { get; set; }

        [StringLength(32)] public string UserPass { get; set; }

        public int? RoleId { get; set; }

        [StringLength(100)] [Required] public string UserMail { get; set; }

        [StringLength(250)] public string Avatar { get; set; }

        public int? StatusUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comic> comics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }

        public virtual role Role { get; set; }

        public virtual status_user status_user { get; set; }
    }
}