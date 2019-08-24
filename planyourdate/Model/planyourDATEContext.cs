using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace planyourdate.Model
{
    public partial class planyourDATEContext : DbContext
    {
        public planyourDATEContext()
        {
        }

        public planyourDATEContext(DbContextOptions<planyourDATEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Place> Place { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:planyourdate.database.windows.net,1433;Initial Catalog=planyourDATE;Persist Security Info=False;User ID=liam;Password=A1s2d3f4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.Photo1).IsUnicode(false);

                entity.Property(e => e.PhotoName).IsUnicode(false);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlaceId");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.PhotoRef).IsUnicode(false);

                entity.Property(e => e.PlaceAddress).IsUnicode(false);

                entity.Property(e => e.PlaceGeolat).IsUnicode(false);

                entity.Property(e => e.PlaceGeolng).IsUnicode(false);

                entity.Property(e => e.PlaceName).IsUnicode(false);
            });
        }
    }
}
