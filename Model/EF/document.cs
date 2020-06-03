using Newtonsoft.Json;

namespace Model.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("document")]
    public partial class document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public document()
        {
            image_chapter = new HashSet<image_chapter>();
        }
        
        public int DocumentId { get; set; }

        [StringLength(256)] public string DocumentPart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<image_chapter> image_chapter { get; set; }
    }
}