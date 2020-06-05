using System;
using Newtonsoft.Json;

namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;

    public partial class image_chapter
    {
        [Key]
        public int ImageChapterId { get; set; }

        public int? ChapterId { get; set; }

        [StringLength(256)]
        public String PartFile { get; set; }

        [JsonIgnore]
        public virtual chapter chapter { get; set; }
        
    }
}
