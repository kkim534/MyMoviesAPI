using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyMovies.Model
{
    public partial class Movie
    {
        public Movie()
        {
            Comments = new HashSet<Comments>();
        }

        public int MovieId { get; set; }
        [Required]
        [StringLength(255)]
        public string MovieTitle { get; set; }
        [Required]
        [Column("ThumbnailURL")]
        [StringLength(255)]
        public string ThumbnailUrl { get; set; }
        [Column("isFavourite")]
        public bool IsFavourite { get; set; }
        [StringLength(255)]
        public string Cast { get; set; }
        [StringLength(255)]
        public string Director { get; set; }
        [Column("MovieDbID")]
        public int? MovieDbId { get; set; }
        [StringLength(8000)]
        public string Plot { get; set; }

        [InverseProperty("Movie")]
        public virtual ICollection<Comments> Comments { get; set; }

        [DataContract]
        public class MovieDTO
        {
            [DataMember]
            public int MovieId { get; set; }

            [DataMember]
            public string MovieTitle { get; set; }

            [DataMember]
            public string Director { get; set; }

            [DataMember]
            public string Cast { get; set; }

            [DataMember]
            public string ThumbnailUrl { get; set; }

            [DataMember]
            public bool IsFavourite { get; set; }

            [DataMember]
            public int MovieDbId { get; set; }

            [DataMember]
            public string Plot { get; set; }
        }
    }
}
