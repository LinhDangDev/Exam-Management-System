using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GettingStarted.Shared.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaThi> CaThis { get; set; }

    public virtual DbSet<ChiTietBaiThi> ChiTietBaiThis { get; set; }

    public virtual DbSet<ChiTietCaThi> ChiTietCaThis { get; set; }

    public virtual DbSet<ChiTietDotThi> ChiTietDotThis { get; set; }

    public virtual DbSet<DotThi> DotThis { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<LopAo> LopAos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<SinhVienLopAo> SinhVienLopAos { get; set; }

    public virtual DbSet<TblAudioListened> AudioListeneds { get; set; }

    public virtual DbSet<TblCauHoi> CauHois { get; set; }

    public virtual DbSet<TblCauHoiMa> CauHoiMas { get; set; }

    public virtual DbSet<TblCauTraLoi> CauTraLois { get; set; }

    public virtual DbSet<TblChiTietCauHoiMa> ChiTietCauHoiMas { get; set; }

    public virtual DbSet<TblChiTietDeThi> ChiTietDeThis { get; set; }

    public virtual DbSet<TblChiTietDeThiHoanVi> ChiTietDeThiHoanVis { get; set; }

    public virtual DbSet<TblDanhmucCaThiTrongNgay> DanhmucCaThiTrongNgays { get; set; }

    public virtual DbSet<TblDeThi> DeThis { get; set; }

    public virtual DbSet<TblDeThiHoanVi> DeThiHoanVis { get; set; }

    public virtual DbSet<TblNhomCauHoi> NhomCauHois { get; set; }

    public virtual DbSet<TblNhomCauHoiHoanVi> TblNhomCauHoiHoanVis { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
<<<<<<< HEAD

=======
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
>>>>>>> main
        => optionsBuilder.UseSqlServer("Server=DESKTOP-URQSHUO\\SQLEXPRESS;Database=TracNghiem_HUTECH;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaThi>(entity =>
        {
            entity.HasKey(e => e.MaCaThi).HasName("PK_shift");

            entity.ToTable("ca_thi");

            entity.Property(e => e.MaCaThi).HasColumnName("ma_ca_thi");
            entity.Property(e => e.ActivatedDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovedComments).HasMaxLength(500);
            entity.Property(e => e.ApprovedDate).HasColumnType("date");
            entity.Property(e => e.MaChiTietDotThi).HasColumnName("ma_chi_tiet_dot_thi");
            entity.Property(e => e.MatMa).HasMaxLength(128);
            entity.Property(e => e.TenCaThi)
                .HasMaxLength(50)
                .HasColumnName("ten_ca_thi");
            entity.Property(e => e.ThoiDiemKetThuc).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");

            entity.HasOne(d => d.MaChiTietDotThiNavigation).WithMany(p => p.CaThis)
                .HasForeignKey(d => d.MaChiTietDotThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ca_thi_chi_tiet_dot_thi");
        });

        modelBuilder.Entity<ChiTietBaiThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietBaiThi).HasName("PK_task_detail");

            entity.ToTable("chi_tiet_bai_thi");

            entity.Property(e => e.MaChiTietCaThi).HasColumnName("ma_chi_tiet_ca_thi");
            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaChiTietCaThiNavigation).WithMany(p => p.ChiTietBaiThis)
                .HasForeignKey(d => d.MaChiTietCaThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_bai_thi_chi_tiet_ca_thi");
        });

        modelBuilder.Entity<ChiTietCaThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietCaThi).HasName("PK_shift_detail");

            entity.ToTable("chi_tiet_ca_thi");

            entity.Property(e => e.MaChiTietCaThi).HasColumnName("ma_chi_tiet_ca_thi");
            entity.Property(e => e.DaHoanThanh).HasColumnName("da_hoan_thanh");
            entity.Property(e => e.DaThi).HasColumnName("da_thi");
            entity.Property(e => e.Diem)
                .HasDefaultValueSql("((-1))")
                .HasColumnName("diem");
            entity.Property(e => e.GioCongThem).HasColumnName("gio_cong_them");
            entity.Property(e => e.LyDoCong).HasColumnName("ly_do_cong");
            entity.Property(e => e.MaCaThi).HasColumnName("ma_ca_thi");
            entity.Property(e => e.MaDeThi).HasColumnName("ma_de_thi");
            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
            entity.Property(e => e.SoCauDung)
                .HasDefaultValueSql("((0))")
                .HasColumnName("so_cau_dung");
            entity.Property(e => e.ThoiDiemCong)
                .HasColumnType("datetime")
                .HasColumnName("thoi_diem_cong");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_ket_thuc");
            entity.Property(e => e.TongSoCau)
                .HasDefaultValueSql("((0))")
                .HasColumnName("tong_so_cau");

            entity.HasOne(d => d.MaCaThiNavigation).WithMany(p => p.ChiTietCaThis)
                .HasForeignKey(d => d.MaCaThi)
                .HasConstraintName("FK_chi_tiet_ca_thi_ca_thi");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.ChiTietCaThis)
                .HasForeignKey(d => d.MaSinhVien)
                .HasConstraintName("FK_chi_tiet_ca_thi_sinh_vien");
        });

        modelBuilder.Entity<ChiTietDotThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDotThi).HasName("PK_phase_detail");

            entity.ToTable("chi_tiet_dot_thi");

            entity.Property(e => e.MaChiTietDotThi).HasColumnName("ma_chi_tiet_dot_thi");
            entity.Property(e => e.LanThi)
                .HasMaxLength(200)
                .HasColumnName("lan_thi");
            entity.Property(e => e.MaDotThi).HasColumnName("ma_dot_thi");
            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.TenChiTietDotThi)
                .HasMaxLength(200)
                .HasColumnName("ten_chi_tiet_dot_thi");

            entity.HasOne(d => d.MaDotThiNavigation).WithMany(p => p.ChiTietDotThis)
                .HasForeignKey(d => d.MaDotThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_dot_thi_dot_thi1");

            entity.HasOne(d => d.MaLopAoNavigation).WithMany(p => p.ChiTietDotThis)
                .HasForeignKey(d => d.MaLopAo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_dot_thi_lop_ao");
        });

        modelBuilder.Entity<DotThi>(entity =>
        {
            entity.HasKey(e => e.MaDotThi);

            entity.ToTable("dot_thi");

            entity.Property(e => e.MaDotThi).HasColumnName("ma_dot_thi");
            entity.Property(e => e.TenDotThi)
                .HasMaxLength(150)
                .HasColumnName("ten_dot_thi");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_ket_thuc");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK_department");

            entity.ToTable("khoa");

            entity.Property(e => e.MaKhoa).HasColumnName("ma_khoa");
            entity.Property(e => e.NgayThanhLap)
                .HasColumnType("datetime")
                .HasColumnName("ngay_thanh_lap");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(30)
                .HasColumnName("ten_khoa");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK_class");

            entity.ToTable("lop");

            entity.Property(e => e.MaLop).HasColumnName("ma_lop");
            entity.Property(e => e.MaKhoa).HasColumnName("ma_khoa");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.TenLop)
                .HasMaxLength(50)
                .HasColumnName("ten_lop");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK_lop_khoa");
        });

        modelBuilder.Entity<LopAo>(entity =>
        {
            entity.HasKey(e => e.MaLopAo).HasName("PK_class_virtual");

            entity.ToTable("lop_ao");

            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.MaMonHoc).HasColumnName("ma_mon_hoc");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.TenLopAo)
                .HasMaxLength(200)
                .HasColumnName("ten_lop_ao");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.LopAos)
                .HasForeignKey(d => d.MaMonHoc)
                .HasConstraintName("FK_lop_ao_mon_hoc");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("menu");

            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.MenuDescription)
                .HasMaxLength(200)
                .HasColumnName("menu_description");
            entity.Property(e => e.MenuOrder).HasColumnName("menu_order");
            entity.Property(e => e.MenuParentId).HasColumnName("menu_parent_id");
            entity.Property(e => e.MenuTitle)
                .HasMaxLength(100)
                .HasColumnName("menu_title");
            entity.Property(e => e.MenuUrl)
                .HasMaxLength(100)
                .HasColumnName("menu_url");
            entity.Property(e => e.MenuValuepath)
                .HasMaxLength(100)
                .HasColumnName("menu_valuepath");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.MaMonHoc).HasName("PK_subject");

            entity.ToTable("mon_hoc");

            entity.Property(e => e.MaMonHoc).HasColumnName("ma_mon_hoc");
            entity.Property(e => e.MaSoMonHoc)
                .HasMaxLength(50)
                .HasColumnName("ma_so_mon_hoc");
            entity.Property(e => e.TenMonHoc)
                .HasMaxLength(200)
                .HasColumnName("ten_mon_hoc");
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien).HasName("PK_student");

            entity.ToTable("sinh_vien");

            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
            entity.Property(e => e.DiaChi)
                .HasColumnType("text")
                .HasColumnName("dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .HasColumnName("dien_thoai");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh).HasColumnName("gioi_tinh");
            entity.Property(e => e.HoVaTenLot)
                .HasMaxLength(300)
                .HasColumnName("ho_va_ten_lot");
            entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");
            entity.Property(e => e.LastLoggedIn)
                .HasColumnType("datetime")
                .HasColumnName("last_logged_in");
            entity.Property(e => e.LastLoggedOut)
                .HasColumnType("datetime")
                .HasColumnName("last_logged_out");
            entity.Property(e => e.MaLop).HasColumnName("ma_lop");
            entity.Property(e => e.MaSoSinhVien)
                .HasMaxLength(50)
                .HasColumnName("ma_so_sinh_vien");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("datetime")
                .HasColumnName("ngay_sinh");
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TenSinhVien)
                .HasMaxLength(50)
                .HasColumnName("ten_sinh_vien");
        });

        modelBuilder.Entity<SinhVienLopAo>(entity =>
        {
            entity.HasKey(e => e.MaSinhVienLopAo);

            entity.ToTable("sinh_vien_lop_ao");

            entity.Property(e => e.MaSinhVienLopAo).HasColumnName("ma_sinh_vien_lop_ao");
            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
        });

        modelBuilder.Entity<TblAudioListened>(entity =>
        {
            entity.HasKey(e => e.ListenId);

            entity.ToTable("tbl_AudioListened");

            entity.Property(e => e.ListenId).HasColumnName("ListenID");
        });

        modelBuilder.Entity<TblCauHoi>(entity =>
        {
            entity.HasKey(e => e.MaCauHoi);

            entity.ToTable("tbl_CauHoi");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.HoanVi)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KieuNoiDung).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.TieuDe).HasMaxLength(250);
        });

        modelBuilder.Entity<TblCauHoiMa>(entity =>
        {
            entity.HasKey(e => e.MaCauHoiMa);

            entity.ToTable("tbl_CauHoiMa");
        });

        modelBuilder.Entity<TblCauTraLoi>(entity =>
        {
            entity.HasKey(e => e.MaCauTraLoi);

            entity.ToTable("tbl_CauTraLoi");

            entity.Property(e => e.MaCauHoi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaCauHoiNavigation).WithMany(p => p.TblCauTraLois)
                .HasForeignKey(d => d.MaCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_CauTraLoi_tbl_CauHoi");
        });

        modelBuilder.Entity<TblChiTietCauHoiMa>(entity =>
        {
            entity.HasKey(e => new { e.MaCauHoiMa, e.MaChiTietBaiThi });

            entity.ToTable("tbl_ChiTietCauHoiMa");
        });

        modelBuilder.Entity<TblChiTietDeThi>(entity =>
        {
            entity.HasKey(e => new { e.MaNhom, e.MaCauHoi });

            entity.ToTable("tbl_ChiTietDeThi");

            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<TblChiTietDeThiHoanVi>(entity =>
        {
            entity.HasKey(e => new { e.MaDeHv, e.MaNhom, e.MaCauHoi });

            entity.ToTable("tbl_ChiTietDeThiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.HoanViTraLoi).HasMaxLength(4);
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Ma).WithMany(p => p.TblChiTietDeThiHoanVis)
                .HasForeignKey(d => new { d.MaDeHv, d.MaNhom })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_ChiTietDeThiHoanVi_tbl_NhomCauHoiHoanVi");
        });

        modelBuilder.Entity<TblDanhmucCaThiTrongNgay>(entity =>
        {
            entity.HasKey(e => e.MaCaTrongNgay).HasName("PK_tbl_CaThiTrongNgay");

            entity.ToTable("tbl_danhmuc_CaThiTrongNgay");

            entity.Property(e => e.CaThiCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TenCaTrongNgay).HasMaxLength(100);
        });

        modelBuilder.Entity<TblDeThi>(entity =>
        {
            entity.HasKey(e => e.MaDeThi);

            entity.ToTable("tbl_DeThi");

            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.LuuTam)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MaMonHoc).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiTao).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TenDeThi).HasMaxLength(250);
        });

        modelBuilder.Entity<TblDeThiHoanVi>(entity =>
        {
            entity.HasKey(e => e.MaDeHv);

            entity.ToTable("tbl_DeThiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.KyHieuDe).HasMaxLength(50);
            entity.Property(e => e.MaDeThi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaDeThiNavigation).WithMany(p => p.TblDeThiHoanVis)
                .HasForeignKey(d => d.MaDeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_DeThiHoanVi_tbl_DeThi1");
        });

        modelBuilder.Entity<TblNhomCauHoi>(entity =>
        {
            entity.HasKey(e => e.MaNhom);

            entity.ToTable("tbl_NhomCauHoi");

            entity.Property(e => e.LaCauHoiNhom).HasDefaultValueSql("((0))");
            entity.Property(e => e.MaDeThi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.MaNhomCha).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.SoCauLay).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TenNhom).HasMaxLength(250);

            entity.HasOne(d => d.MaDeThiNavigation).WithMany(p => p.TblNhomCauHois)
                .HasForeignKey(d => d.MaDeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_NhomCauHoi_tbl_DeThi");
        });

        modelBuilder.Entity<TblNhomCauHoiHoanVi>(entity =>
        {
            entity.HasKey(e => new { e.MaDeHv, e.MaNhom }).HasName("PK_tbl_NhomHoanVi");

            entity.ToTable("tbl_NhomCauHoiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaDeHvNavigation).WithMany(p => p.TblNhomCauHoiHoanVis)
                .HasForeignKey(d => d.MaDeHv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_NhomCauHoiHoanVi_tbl_DeThiHoanVi");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users");

            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasColumnType("ntext");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FailedPwdAnswerWindowStart).HasColumnType("datetime");
            entity.Property(e => e.FailedPwdAttemptWindowStart).HasColumnType("datetime");
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");
            entity.Property(e => e.LoginName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
