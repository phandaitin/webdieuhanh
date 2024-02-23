using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApp.Areas.Admin.Data
{
    public partial class webdieuhanhContext : DbContext
    {
        public webdieuhanhContext()
        {
        }

        public webdieuhanhContext(DbContextOptions<webdieuhanhContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TDonvi> TDonvis { get; set; }
        public virtual DbSet<TMenu> TMenus { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.  ;  Initial Catalog=webdieuhanh  ;  User ID= webdieuhanh ;  Password= webdieuhanh  ;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TDonvi>(entity =>
            {
                entity.HasKey(e => e.MaDvId);

                entity.ToTable("T_Donvi");

                entity.Property(e => e.MaDv)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.TenDv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TMenu>(entity =>
            {
                entity.HasKey(e => e.Tt);

                entity.ToTable("T_Menu");

                entity.Property(e => e.Tt)
                    .ValueGeneratedNever()
                    .HasColumnName("TT");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenMenu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("T_User");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.MaDv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
