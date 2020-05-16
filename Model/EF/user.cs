namespace up_down.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            comic = new HashSet<comic>();
            comment = new HashSet<comment>();
        }

        public int UserId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(25)]
        public string UserPass { get; set; }

        public int? DevolutionId { get; set; }

        [StringLength(100)]
        public string UserMail { get; set; }

        [StringLength(250)]
        public string Avatar { get; set; }

        public int? StatusUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comic> comic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comment { get; set; }

        public virtual role Role { get; set; }

        public virtual status_user status_user { get; set; }
    }
}
