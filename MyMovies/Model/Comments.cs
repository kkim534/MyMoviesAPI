using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Model
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int? MovieId { get; set; }
        [Required]
        [StringLength(255)]
        public string Comment { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Comments")]
        public virtual Movie Movie { get; set; }
    }
}
