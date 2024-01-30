using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VNETC_WebApp.Areas.Admin.Data
{
    public partial class VNETCContext : DbContext
    {
        public VNETCContext()
        {
        }

        public VNETCContext(DbContextOptions<VNETCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCauhinh> TCauhinhs { get; set; }
        public virtual DbSet<TDichvu> TDichvus { get; set; }
        public virtual DbSet<TDoitac> TDoitacs { get; set; }
        public virtual DbSet<TKhachhang> TKhachhangs { get; set; }
        public virtual DbSet<TNganhnghe> TNganhnghes { get; set; }
        public virtual DbSet<TNganhngheCt1> TNganhngheCt1s { get; set; }
        public virtual DbSet<TNganhngheCt2> TNganhngheCt2s { get; set; }
        public virtual DbSet<TNganhngheCt3> TNganhngheCt3s { get; set; }
        public virtual DbSet<TNganhngheCt4> TNganhngheCt4s { get; set; }
        public virtual DbSet<TPost> TPosts { get; set; }
        public virtual DbSet<TTeam> TTeams { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=VNETC;User ID= vnetc ; Password= vnetc ;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TCauhinh>(entity =>
            {
                entity.HasKey(e => e.CauhinhId);

                entity.ToTable("T_Cauhinh");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Camket1).HasMaxLength(500);

                entity.Property(e => e.Camket2).HasMaxLength(500);

                entity.Property(e => e.Camket3).HasMaxLength(500);

                entity.Property(e => e.Camket4).HasMaxLength(500);

                entity.Property(e => e.Camket5).HasMaxLength(500);

                entity.Property(e => e.Chinhanh1).HasMaxLength(500);

                entity.Property(e => e.Chinhanh2).HasMaxLength(500);

                entity.Property(e => e.Chinhanh3).HasMaxLength(500);

                entity.Property(e => e.Chinhanh4).HasMaxLength(500);

                entity.Property(e => e.Chinhanh5).HasMaxLength(500);

                entity.Property(e => e.Chungtoila)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Gioithieucongty).HasMaxLength(500);

                entity.Property(e => e.Kehoach).HasMaxLength(50);

                entity.Property(e => e.KehoachNoidung).HasMaxLength(500);

                entity.Property(e => e.KehoachThumb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ketluan).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.Sumenh).HasMaxLength(50);

                entity.Property(e => e.SumenhNoidung).HasMaxLength(500);

                entity.Property(e => e.SumenhThumb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Taisaolachungtoi).HasMaxLength(500);

                entity.Property(e => e.Tamnhin).HasMaxLength(50);

                entity.Property(e => e.TamnhinNoidung).HasMaxLength(500);

                entity.Property(e => e.TamnhinThumb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Videogioithieucongty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vpdd1)
                    .HasMaxLength(500)
                    .HasColumnName("VPDD1");

                entity.Property(e => e.Vpdd2)
                    .HasMaxLength(500)
                    .HasColumnName("VPDD2");

                entity.Property(e => e.Vpdd3)
                    .HasMaxLength(500)
                    .HasColumnName("VPDD3");

                entity.Property(e => e.Vpdd4)
                    .HasMaxLength(500)
                    .HasColumnName("VPDD4");

                entity.Property(e => e.Vpdd5)
                    .HasMaxLength(500)
                    .HasColumnName("VPDD5");

                entity.Property(e => e.Welcome).HasMaxLength(500);
            });

            modelBuilder.Entity<TDichvu>(entity =>
            {
                entity.HasKey(e => e.DichvuId);

                entity.ToTable("T_Dichvu");

                entity.Property(e => e.Mota).HasMaxLength(500);

                entity.Property(e => e.TenDv)
                    .HasMaxLength(100)
                    .HasColumnName("TenDV");
            });

            modelBuilder.Entity<TDoitac>(entity =>
            {
                entity.HasKey(e => e.DoitacId);

                entity.ToTable("T_Doitac");

                entity.Property(e => e.Link).HasMaxLength(500);

                entity.Property(e => e.Logo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TKhachhang>(entity =>
            {
                entity.HasKey(e => e.KhachhangId);

                entity.ToTable("T_Khachhang");

                entity.Property(e => e.Avarta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Chucvu).HasMaxLength(500);

                entity.Property(e => e.Hoten).HasMaxLength(500);

                entity.Property(e => e.Noidungdanhgia).HasMaxLength(1000);
            });

            modelBuilder.Entity<TNganhnghe>(entity =>
            {
                entity.HasKey(e => e.NganhngheId);

                entity.ToTable("T_Nganhnghe");

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.Dienthoai).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Gioithieu).HasMaxLength(500);

                entity.Property(e => e.Loaihinhdoanhnghiep).HasMaxLength(500);

                entity.Property(e => e.Mst)
                    .HasMaxLength(500)
                    .HasColumnName("MST");

                entity.Property(e => e.Nguoidaidien).HasMaxLength(500);

                entity.Property(e => e.Quanlyboi).HasMaxLength(500);

                entity.Property(e => e.SoDkkd)
                    .HasMaxLength(100)
                    .HasColumnName("SoDKKD");

                entity.Property(e => e.TenCongty).HasMaxLength(500);

                entity.Property(e => e.Tennganhnghe1).HasMaxLength(500);

                entity.Property(e => e.Tennganhnghe2).HasMaxLength(500);

                entity.Property(e => e.Tennganhnghe3).HasMaxLength(500);

                entity.Property(e => e.Tennganhnghe4).HasMaxLength(500);

                entity.Property(e => e.Tentienganh).HasMaxLength(500);

                entity.Property(e => e.Tenviettat).HasMaxLength(500);

                entity.Property(e => e.Thumbnganhnghe1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnganhnghe2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnganhnghe3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnganhnghe4)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TNganhngheCt1>(entity =>
            {
                entity.HasKey(e => e.NganhngheCt1id);

                entity.ToTable("T_NganhngheCT1");

                entity.Property(e => e.NganhngheCt1id).HasColumnName("NganhngheCT1Id");

                entity.Property(e => e.MaNn)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MaNN");

                entity.Property(e => e.TenNn)
                    .HasMaxLength(500)
                    .HasColumnName("TenNN");
            });

            modelBuilder.Entity<TNganhngheCt2>(entity =>
            {
                entity.HasKey(e => e.NganhngheCt2id);

                entity.ToTable("T_NganhngheCT2");

                entity.Property(e => e.NganhngheCt2id).HasColumnName("NganhngheCT2Id");

                entity.Property(e => e.MaNn)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MaNN");

                entity.Property(e => e.TenNn)
                    .HasMaxLength(500)
                    .HasColumnName("TenNN");
            });

            modelBuilder.Entity<TNganhngheCt3>(entity =>
            {
                entity.HasKey(e => e.NganhngheCt3id);

                entity.ToTable("T_NganhngheCT3");

                entity.Property(e => e.NganhngheCt3id).HasColumnName("NganhngheCT3Id");

                entity.Property(e => e.MaNn)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MaNN");

                entity.Property(e => e.TenNn)
                    .HasMaxLength(500)
                    .HasColumnName("TenNN");
            });

            modelBuilder.Entity<TNganhngheCt4>(entity =>
            {
                entity.HasKey(e => e.NganhngheCt4id);

                entity.ToTable("T_NganhngheCT4");

                entity.Property(e => e.NganhngheCt4id).HasColumnName("NganhngheCT4Id");

                entity.Property(e => e.MaNn)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MaNN");

                entity.Property(e => e.TenNn)
                    .HasMaxLength(500)
                    .HasColumnName("TenNN");
            });

            modelBuilder.Entity<TPost>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.ToTable("T_Post");

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.Chudautu).HasMaxLength(500);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.PostName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Slug)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tencongtrinh).HasMaxLength(500);

                entity.Property(e => e.Thumb1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Thumb2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Thumb3)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TTeam>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.ToTable("T_Team");

                entity.Property(e => e.Avarta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bangcap1).HasMaxLength(500);

                entity.Property(e => e.Bangcap2).HasMaxLength(500);

                entity.Property(e => e.Bangcap3).HasMaxLength(500);

                entity.Property(e => e.Chucvu).HasMaxLength(500);

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hoten).HasMaxLength(500);

                entity.Property(e => e.Instagram)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Twitter)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("twitter");

                entity.Property(e => e.Zalo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("T_User");

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
