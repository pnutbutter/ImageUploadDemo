using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AS.ImageAlbum.DataAccess
{
    public partial class WebsiteDBContext : DbContext
    {
        public WebsiteDBContext()
        {
        }

        public WebsiteDBContext(DbContextOptions<WebsiteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblImage> TblImage { get; set; }
        public virtual DbSet<TblImageTag> TblImageTag { get; set; }
        public virtual DbSet<TblTag> TblTag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ImageUploadConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__tblImage__7516F70C84CBAFDC");

                entity.ToTable("tblImage");

                entity.HasIndex(e => e.ImageName)
                    .IsUnique();

                entity.HasIndex(e => e.ImageUrl)
                    .IsUnique();

                entity.Property(e => e.ImageId).ValueGeneratedNever();

                entity.Property(e => e.AlbumImage)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.ImageAlt)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageURL")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblImageTag>(entity =>
            {
                entity.HasKey(e => e.ImageTagId)
                    .HasName("PK__tblImage__AE6DAB248FAB2F0A");

                entity.ToTable("tblImageTag");

                entity.HasIndex(e => new { e.ImageId, e.TagId })
                    .IsUnique();

                entity.Property(e => e.ImageTagId).ValueGeneratedNever();

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.TblImageTag)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblImageTag_tblImage");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TblImageTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblImageTag_tblTag");
            });

            modelBuilder.Entity<TblTag>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__tblTag__657CF9AC623A8CD7");

                entity.ToTable("tblTag");

                entity.Property(e => e.TagId).ValueGeneratedNever();

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
