using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NEACSwimmingPoolMang.API.Models
{
    public partial class NEACSwimmingPoolMangQas1Context : DbContext
    {
        public NEACSwimmingPoolMangQas1Context()
        {
        }

        public NEACSwimmingPoolMangQas1Context(DbContextOptions<NEACSwimmingPoolMangQas1Context> options)
          : base(options)
        {
        }

        public virtual DbSet<ClassMangBannerDatum> ClassMangBannerData { get; set; }
        public virtual DbSet<Filedatum> Filedata { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:NEACSwimmingPoolMangContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ClassMangBannerDatum>(entity =>
            {
                entity.ToTable("class_mang_banner_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                  .HasColumnType("datetime")
                  .HasColumnName("create_time");

                entity.Property(e => e.CreaterId).HasColumnName("creater_id");

                entity.Property(e => e.Enddate)
                  .HasColumnType("datetime")
                  .HasColumnName("enddate");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.ImgGuid).HasColumnName("img_guid");

                entity.Property(e => e.Link)
                  .IsUnicode(false)
                  .HasColumnName("link");

                entity.Property(e => e.Startdate)
                  .HasColumnType("datetime")
                  .HasColumnName("startdate");

                entity.Property(e => e.SysStatus).HasColumnName("sys_status");

                entity.Property(e => e.Title)
                  .HasColumnName("title");

                entity.Property(e => e.UpdateTime)
                  .HasColumnType("datetime")
                  .HasColumnName("update_time");

                entity.Property(e => e.UpdaterId).HasColumnName("updater_id");
            });

            modelBuilder.Entity<Filedatum>(entity =>
            {
                entity.ToTable("filedata");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                  .HasColumnType("datetime")
                  .HasColumnName("create_time");

                entity.Property(e => e.CreaterId).HasColumnName("creater_id");

                entity.Property(e => e.FileStream)
                  .IsRequired()
                  .HasColumnName("file_stream");

                entity.Property(e => e.FileName)
                 .HasMaxLength(255)
                 .HasColumnName("fileName");

                entity.Property(e => e.Filetype)
                  .HasMaxLength(100)
                  .IsUnicode(false)
                  .HasColumnName("filetype");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.SysStatus).HasColumnName("sys_status");

                entity.Property(e => e.UpdateTime)
                  .HasColumnType("datetime")
                  .HasColumnName("update_time");

                entity.Property(e => e.UpdaterId).HasColumnName("updater_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(255)
                  .IsUnicode(false)
                  .HasColumnName("email");

                entity.Property(e => e.Password)
                  .IsRequired()
                  .HasMaxLength(255)
                  .IsUnicode(false)
                  .HasColumnName("password");

                entity.Property(e => e.Username)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false)
                  .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}