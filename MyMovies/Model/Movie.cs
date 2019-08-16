using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int MovieLikes { get; set; }
        [Required]
        [Column("ThumbnailURL")]
        [StringLength(255)]
        public string ThumbnailUrl { get; set; }
        [Column("isFavourite")]
        public bool IsFavourite { get; set; }

        [InverseProperty("Movie")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
