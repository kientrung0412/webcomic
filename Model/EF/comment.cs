namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comment")]
    public partial class comment
    {
        public int CommentId { get; set; }

        public int? UserId { get; set; }

        public int? ChapterId { get; set; }

        public string CommentConten { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CommentTime { get; set; }

        public int? StatusCommentId { get; set; }

        public virtual chapter chapter { get; set; }

        public virtual status_comment status_comment { get; set; }

        public virtual user user { get; set; }
    }
}
