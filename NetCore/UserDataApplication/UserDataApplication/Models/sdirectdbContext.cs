using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserDataApplication.Models
{
    public partial class sdirectdbContext : DbContext
    {
        public sdirectdbContext()
        {
        }

        public sdirectdbContext(DbContextOptions<sdirectdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City080723> City080723s { get; set; } = null!;
        public virtual DbSet<Country080723> Country080723s { get; set; } = null!;
        public virtual DbSet<State080723> State080723s { get; set; } = null!;
        public virtual DbSet<UserData080723> UserData080723s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.0.240;Database=sdirectdb;UID=sdirectdb;PWD=sdirectdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City080723>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__City0807__F2D21B763129FC65");

                entity.ToTable("City080723");

                entity.Property(e => e.CityName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City080723s)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__City08072__State__7B7CE48A");
            });

            modelBuilder.Entity<Country080723>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Country0__10D1609F3AC3A855");

                entity.ToTable("Country080723");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<State080723>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__State080__C3BA3B3AFA4C548E");

                entity.ToTable("State080723");

                entity.Property(e => e.StateName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.State080723s)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__State0807__Count__75C40B34");
            });

            modelBuilder.Entity<UserData080723>(entity =>
            {
                entity.ToTable("UserData080723");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImgLoc).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserData080723s)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__UserData0__CityI__6E77EF27");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserData080723s)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__UserData0__Count__6C8FA6B5");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserData080723s)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__UserData0__State__6D83CAEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
