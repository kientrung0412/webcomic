using System;
using System.Collections.Generic;
using Model.EF;

namespace Model.Models
{
    public class ComicCategoryFilte
    {
        public int ComicId { get; set; }
        public String BanerComic { get; set; }
        public String NameComic { get; set; }
        public ICollection<chapter> Chapters { get; set; }

    }
}