using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Models;

public partial class API_Application_V1Context : DbContext
{
    public API_Application_V1Context()
    {
    }

    public API_Application_V1Context(DbContextOptions<API_Application_V1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AppID> AppIDs { get; set; }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<ChiTietHocPhi> ChiTietHocPhis { get; set; }

    public virtual DbSet<ChiTietHocPhiTheoMonth> ChiTietHocPhiTheoMonths { get; set; }

    public virtual DbSet<ChiTietXinNghiPhep> ChiTietXinNghiPheps { get; set; }

    public virtual DbSet<ClassModel> ClassModels { get; set; }

    public virtual DbSet<DanThuocModel> DanThuocModels { get; set; }

    public virtual DbSet<DiemDanhModel> DiemDanhModels { get; set; }

    public virtual DbSet<DinhDuongModel> DinhDuongModels { get; set; }

    public virtual DbSet<HocPhi> HocPhis { get; set; }

    public virtual DbSet<HocPhiChiTietModel2> HocPhiChiTietModels { get; set; }

    public virtual DbSet<HocPhiModel2> HocPhiModels { get; set; }

    public virtual DbSet<KhoaHocModel> KhoaHocModels { get; set; }

    public virtual DbSet<MaterBieuDo> MaterBieuDos { get; set; }

    public virtual DbSet<MaterHocPhi> MaterHocPhis { get; set; }

    public virtual DbSet<NhatKy> NhatKies { get; set; }

    public virtual DbSet<OTKB> OTKBs { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SoBeNgoan> SoBeNgoans { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TKB1> TKB1s { get; set; }

    public virtual DbSet<TableImage> TableImages { get; set; }

    public virtual DbSet<TableLike> TableLikes { get; set; }

    public virtual DbSet<ThoiKhoaBieuModel> ThoiKhoaBieuModels { get; set; }

    public virtual DbSet<TinTucModel> TinTucModels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<XinNghiPhepModel> XinNghiPhepModels { get; set; }

    public virtual DbSet<HoatDongModel> HoatDongModels { get; set; }
    public virtual DbSet<PhanCongGiaoVienModel> PhanCongGiaoViens { get; set; }

    public virtual DbSet<GiaTiensModel> GiaTiens { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Data Source=BNK-TAMNH26\\SQLEXPRESS;Initial Catalog=API_Application_V1;Integrated Security=True;Trust Server Certificate=True");
    //=> optionsBuilder.UseSqlServer("Data Source=192.168.1.105;Initial Catalog=API_Application_V1;Persist Security Info=True;User ID=sa;Password=Admin@2023;Trust Server Certificate=True;Command Timeout=300");
    //=> optionsBuilder.UseSqlServer("Data Source=192.168.1.108;Initial Catalog=API_Application_V1;Persist Security Info=True;User ID=sa;Password=Admin@123;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppID>(entity =>
        {
            entity.ToTable("AppID");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Field1).HasMaxLength(255);
            entity.Property(e => e.Field10).HasMaxLength(255);
            entity.Property(e => e.Field2).HasMaxLength(255);
            entity.Property(e => e.Field3).HasMaxLength(255);
            entity.Property(e => e.Field4).HasMaxLength(255);
            entity.Property(e => e.Field5).HasMaxLength(255);
            entity.Property(e => e.Field6).HasMaxLength(255);
            entity.Property(e => e.Field7).HasMaxLength(255);
            entity.Property(e => e.Field8).HasMaxLength(255);
            entity.Property(e => e.Field9).HasMaxLength(255);
            entity.Property(e => e.IPServer).HasMaxLength(255);
            entity.Property(e => e.Logo).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.ToTable("BinhLuan");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.NhatKy).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.NhatKyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhatKy_BinhLuan");

            entity.HasOne(d => d.Student).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BinhLuan_Student");

            entity.HasOne(d => d.User).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BinhLuan_User");
        });

        modelBuilder.Entity<ChiTietHocPhi>(entity =>
        {
            entity.ToTable("ChiTietHocPhi");

            entity.Property(e => e.Content).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.ExpDate).HasColumnType("date");
            entity.Property(e => e.Months)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Years)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.HocPhi).WithMany(p => p.ChiTietHocPhis)
                .HasForeignKey(d => d.HocPhiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HocPhi_ChiTietHocPhi");
        });

        modelBuilder.Entity<ChiTietHocPhiTheoMonth>(entity =>
        {
            entity.ToTable("ChiTietHocPhiTheoMonth");

            entity.Property(e => e.Content).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Months)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Years)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.HocPhiChiTiet).WithMany(p => p.ChiTietHocPhiTheoMonths)
                .HasForeignKey(d => d.HocPhiChiTietId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHocPhi_ChiTietHocPhiTheoMonth");
        });

        modelBuilder.Entity<ChiTietXinNghiPhep>(entity =>
        {
            entity.ToTable("ChiTietXinNghiPhep");

            entity.Property(e => e.Content).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.XinNghiPhep).WithMany(p => p.ChiTietXinNghiPheps)
                .HasForeignKey(d => d.XinNghiPhepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_XinNghiPhep_ChiTietXinNghiPhep");
        });

        modelBuilder.Entity<ClassModel>(entity =>
        {
            entity.ToTable("ClassModel");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NameClass).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DanThuocModel>(entity =>
        {
            entity.ToTable("DanThuocModel");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocDate).HasColumnType("date");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiemDanhModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DiemDanh");

            entity.ToTable("DiemDanhModel");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DinhDuongModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DinhDuong");

            entity.ToTable("DinhDuongModel");

            entity.Property(e => e.Beo).HasMaxLength(256);
            entity.Property(e => e.BeoDinhMuc).HasMaxLength(256);
            entity.Property(e => e.BuoiChinhChieu).HasMaxLength(256);
            entity.Property(e => e.BuoiPhuChieu).HasMaxLength(256);
            entity.Property(e => e.BuoiSang).HasMaxLength(256);
            entity.Property(e => e.BuoiTrua).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Dam).HasMaxLength(256);
            entity.Property(e => e.DamDinhMuc).HasMaxLength(256);
            entity.Property(e => e.DocDate).HasColumnType("date");
            entity.Property(e => e.Duong).HasMaxLength(256);
            entity.Property(e => e.DuongDinhMuc).HasMaxLength(256);
            entity.Property(e => e.NangLuong).HasMaxLength(256);
            entity.Property(e => e.NangLuongDinhMuc).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<HocPhi>(entity =>
        {
            entity.ToTable("HocPhi");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.NameStudent).HasMaxLength(256);
            entity.Property(e => e.TotalMax).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.TotalMin).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<HocPhiChiTietModel2>(entity =>
        {
            entity.ToTable("HocPhiChiTietModel");

            entity.Property(e => e.Content).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Total).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.HocPhi).WithMany(p => p.HocPhiChiTietModels)
                .HasForeignKey(d => d.HocPhiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HocPhiModel_HocPhiChiTietModel");

        });

        modelBuilder.Entity<HocPhiModel2>(entity =>
        {
            entity.ToTable("HocPhiModel");

            entity.Property(e => e.ConLai).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Months).HasMaxLength(2);
            entity.Property(e => e.PhiOff).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.Total).HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Years).HasMaxLength(4);
        });

        modelBuilder.Entity<KhoaHocModel>(entity =>
        {
            entity.ToTable("KhoaHocModel");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MaterBieuDo>(entity =>
        {

            entity.ToTable("MaterBieuDo");
            modelBuilder.Entity<MaterBieuDo>()
           .Property(m => m.CanNang)
           .HasColumnType("DECIMAL(18, 2)");
            entity.Property(e => e.BMI).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocDate).HasColumnType("date");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MaterHocPhi>(entity =>
        {
            entity.ToTable("MaterHocPhi");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.DonViTinh).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<NhatKy>(entity =>
        {
            entity.ToTable("NhatKy");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.StudentId).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<PhanCongGiaoVienModel>(entity =>
        {
            entity.ToTable("PhanCongGiaoVien");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<OTKB>(entity =>
        {
            entity.ToTable("OTKB");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.ExpiryDate).HasColumnType("smalldatetime");
            entity.Property(e => e.TS).HasColumnType("smalldatetime");
            entity.Property(e => e.TokenHash)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.TokenSalt)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<SoBeNgoan>(entity =>
        {
            entity.ToTable("SoBeNgoan");

            entity.Property(e => e.CanNang).HasColumnType("decimal(19, 3)");
            entity.Property(e => e.ChieuCao).HasColumnType("decimal(19, 3)");
            entity.Property(e => e.ClassSBN).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NhanXet).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.CanNang).HasColumnType("decimal(19, 3)");
            entity.Property(e => e.ChieuCao).HasColumnType("decimal(19, 3)");
            entity.Property(e => e.Class1).HasMaxLength(256);
            entity.Property(e => e.Class2).HasMaxLength(256);
            entity.Property(e => e.Class3).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.GV1).HasMaxLength(256);
            entity.Property(e => e.GV2).HasMaxLength(256);
            entity.Property(e => e.GV3).HasMaxLength(256);
            entity.Property(e => e.NameStudent).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Year1).HasMaxLength(256);
            entity.Property(e => e.Year2).HasMaxLength(256);
            entity.Property(e => e.Year3).HasMaxLength(256);
            entity.Property(e => e.imagePatch).HasMaxLength(256);
        });

        modelBuilder.Entity<TKB1>(entity =>
        {
            entity.ToTable("TKB1");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Thu)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.otkb).WithMany(p => p.TKB1s)
                .HasForeignKey(d => d.otkbId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTKB_TKB1");
        });

        modelBuilder.Entity<TableImage>(entity =>
        {
            entity.ToTable("TableImage");

            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.ImageName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.ImagePatch)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.NhatKy).WithMany(p => p.TableImages)
                .HasForeignKey(d => d.NhatKyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhatKy_TableImage");

            entity.HasOne(d => d.Student).WithMany(p => p.TableImages)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableImage_Student");

            entity.HasOne(d => d.User).WithMany(p => p.TableImages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableImage_User");
        });

        modelBuilder.Entity<TableLike>(entity =>
        {
            entity.ToTable("TableLike");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.NhatKy).WithMany(p => p.TableLikes)
                .HasForeignKey(d => d.NhatKyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhatKy_TableLike");

            entity.HasOne(d => d.Student).WithMany(p => p.TableLikes)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableLike_Student");

            entity.HasOne(d => d.User).WithMany(p => p.TableLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableLike_User");
        });

        modelBuilder.Entity<ThoiKhoaBieuModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ThoiKhoaBieu");

            entity.ToTable("ThoiKhoaBieuModel");

            entity.Property(e => e.ClassTKB)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.Command).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTKB).HasMaxLength(256);
            entity.Property(e => e.Time06300720).HasMaxLength(256);
            entity.Property(e => e.Time07200730).HasMaxLength(256);
            entity.Property(e => e.Time07300815).HasMaxLength(256);
            entity.Property(e => e.Time08150845).HasMaxLength(256);
            entity.Property(e => e.Time08450900).HasMaxLength(256);
            entity.Property(e => e.Time09000930).HasMaxLength(256);
            entity.Property(e => e.Time09301015).HasMaxLength(256);
            entity.Property(e => e.Time10151115).HasMaxLength(256);
            entity.Property(e => e.Time11151400).HasMaxLength(256);
            entity.Property(e => e.Time14001415).HasMaxLength(256);
            entity.Property(e => e.Time14151500).HasMaxLength(256);
            entity.Property(e => e.Time15001515).HasMaxLength(256);
            entity.Property(e => e.Time15151540).HasMaxLength(256);
            entity.Property(e => e.Time15301630).HasMaxLength(256);
            entity.Property(e => e.Time16301730).HasMaxLength(256);
            entity.Property(e => e.Time17301815).HasMaxLength(256);
            entity.Property(e => e.days).HasMaxLength(50);
            entity.Property(e => e.months).HasMaxLength(50);
            entity.Property(e => e.years).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TinTucModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TinTuc");

            entity.ToTable("TinTucModel");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(256);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<HoatDongModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HoatDong");

            entity.ToTable("HoatDong");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Months).HasMaxLength(10);
            entity.Property(e => e.Years).HasMaxLength(10);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Address2).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Position).HasMaxLength(255);
            entity.Property(e => e.TS).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<XinNghiPhepModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_XinNghiPhep");

            entity.ToTable("XinNghiPhepModel");

            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });
        modelBuilder.Entity<GiaTiensModel>(entity =>
        {
            entity.ToTable("GiaTien"); // Tên bảng trong cơ sở dữ liệu

            // Các thuộc tính của bảng GiaTiens
            entity.Property(e => e.name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.gia).IsRequired(); // Loại dữ liệu decimal
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
