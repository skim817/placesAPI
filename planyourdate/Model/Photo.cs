using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace planyourdate.Model
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public int? PlaceId { get; set; }
        [Column("Photo")]
        [StringLength(510)]
        public string Photo1 { get; set; }
        [StringLength(255)]
        public string PhotoName { get; set; }
        public bool IsAvaliable { get; set; }

        [ForeignKey("PlaceId")]
        [InverseProperty("Photo")]
        public virtual Place Place { get; set; }
    }
}
