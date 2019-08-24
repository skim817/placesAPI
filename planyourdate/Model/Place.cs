using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace planyourdate.Model
{
    public partial class Place
    {
        public Place()
        {
            Photo = new HashSet<Photo>();
        }

        public int PlaceId { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceName { get; set; }
        public int? RankBy { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceAddress { get; set; }
        public string Comment { get; set; }
        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        [Column("isFavourite")]
        public bool IsFavourite { get; set; }
        [Column("isOpenNow")]
        public bool? IsOpenNow { get; set; }
        [StringLength(255)]
        public string PlaceGeolat { get; set; }
        [StringLength(255)]
        public string PlaceGeolng { get; set; }
        [Column("photoRef")]
        [StringLength(510)]
        public string PhotoRef { get; set; }

        [InverseProperty("Place")]
        public virtual ICollection<Photo> Photo { get; set; }
    }

    [DataContract]
    public class PlaceDTO
    {
        [DataMember]
        public int PlaceId { get; set; }

        [DataMember]
        public string PlaceName { get; set; }

        [DataMember]
        public int RankBy { get; set; }

        [DataMember]
        public string PhotoRef { get; set; }

        [DataMember]
        public string PlaceAddress { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public bool IsFavourite { get; set; }

        [DataMember]
        public string PlaceGeolat { get; set; }

        [DataMember]
        public string PlaceGeolng { get; set; }

        [DataMember]
        public bool IsOpenNow { get; set; }

    }


}
