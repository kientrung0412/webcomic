using Newtonsoft.Json;

namespace Model.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("chapter")]
    public partial class chapter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public chapter()
        {
            comments = new HashSet<comment>();
            image_chapter = new HashSet<image_chapter>();
            View = 0;
        }

        public int ChapterId { get; set; }

        public int? NumChapter { get; set; }

        [StringLength(50)] public string NameChapter { get; set; }

        public int? ComicId { get; set; }

        public int? View { get; set; }

        [StringLength(256)] public string FolderImage { get; set; }
        [JsonIgnore] public virtual comic comic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<comment> comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<image_chapter> image_chapter { get; set; }
    }
}