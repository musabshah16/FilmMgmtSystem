using System;
using FilmMgmtSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FilmMgmtSystem.EFCore
{
    public partial class FilmMgmtSystemContext : DbContext
    {
        public FilmMgmtSystemContext()
        {
        }

        public FilmMgmtSystemContext(DbContextOptions<FilmMgmtSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Language> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-QDGBF05D\\SQLEXPRESS01;Database=FilmMgmtSystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.Actorid).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Categoryid).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Film");

                entity.Property(e => e.Filmid).ValueGeneratedNever();

                entity.Property(e => e.FilmDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Releaseyear).HasColumnType("date");

                entity.Property(e => e.Rentalduration).HasColumnType("date");

                entity.Property(e => e.Specialfeatures)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Actorid)
                    .HasConstraintName("FK__Film__Actorid__2D27B809");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__Film__Categoryid__2E1BDC42");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Languageid)
                    .HasConstraintName("FK__Film__Languageid__2C3393D0");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Languageid).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
