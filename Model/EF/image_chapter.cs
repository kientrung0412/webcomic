using Newtonsoft.Json;

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class image_chapter
    {
        [Key]
        public int ImageChapterId { get; set; }

        public int? ChapterId { get; set; }

        public int? DocumentId { get; set; }

        [JsonIgnore]
        public virtual chapter chapter { get; set; }

        [JsonIgnore]
        public virtual document document { get; set; }
    }
}
