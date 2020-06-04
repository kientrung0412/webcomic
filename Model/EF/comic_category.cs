using Newtonsoft.Json;

namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;

    public partial class comic_category
    {
        public comic_category(int? categoryId)
        {
            CategoryId = categoryId;
        }

        public comic_category()
        {
        }

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
