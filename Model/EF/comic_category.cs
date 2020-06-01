using Newtonsoft.Json;

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comic_category
    {
        [Key]
        public int ComicCategoryId { get; set; }

        public int? ComicId { get; set; }

        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual category category { get; set; }

        [JsonIgnore]
        public virtual comic comic { get; set; }
    }
}
