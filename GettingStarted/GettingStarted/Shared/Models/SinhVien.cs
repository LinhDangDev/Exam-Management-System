using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class SinhVien
{
    public long MaSinhVien { get; set; }

    public string? HoVaTenLot { get; set; }

    public string? TenSinhVien { get; set; }

    public short? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public int? MaLop { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public string? MaSoSinhVien { get; set; }

    public Guid? StudentId { get; set; }

    public bool? IsLoggedIn { get; set; }

    public DateTime? LastLoggedIn { get; set; }

    public DateTime? LastLoggedOut { get; set; }

    public byte[]? Photo { get; set; }

    public virtual ICollection<ChiTietCaThi> ChiTietCaThis { get; set; } = new List<ChiTietCaThi>();
}
