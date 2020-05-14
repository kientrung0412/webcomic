namespace up_down.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class comic_category
    {
        [Key]
        public int ComicCategoryId { get; set; }

        public int? ComicId { get; set; }

        public int? CategoryId { get; set; }

        public virtual category category { get; set; }

        public virtual comic comic { get; set; }
    }
}
