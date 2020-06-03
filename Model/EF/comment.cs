using Newtonsoft.Json;

namespace Model.EF
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [JsonIgnore]
        public virtual chapter chapter { get; set; }

        [JsonIgnore]
        public virtual status_comment status_comment { get; set; }

        [JsonIgnore]
        public virtual user user { get; set; }
    }
}
