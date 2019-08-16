using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyMovies.Model
{
    public partial class MyMoviesContext : DbContext
    {
        public MyMoviesContext()
        {
        }

        public MyMoviesContext(DbContextOptions<MyMoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:msaphase2mymovies.database.windows.net,1433;Initial Catalog=MyMovies;Persist Security Info=False;User ID=alexkim;Password=Scholar96;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comments__C3B4DFCA67D68E57");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MovieId");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieTitle).IsUnicode(false);

                entity.Property(e => e.ThumbnailUrl).IsUnicode(false);
            });
        }
    }
}
