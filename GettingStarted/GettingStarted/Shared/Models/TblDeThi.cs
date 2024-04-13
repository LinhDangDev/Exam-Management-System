using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblDeThi
{
    public int MaDeThi { get; set; }

    public int MaMonHoc { get; set; }

    public string TenDeThi { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public int NguoiTao { get; set; }

    public string? GhiChu { get; set; }

    public bool? LuuTam { get; set; }

    public bool DaDuyet { get; set; }

    public int? TongSoDeHoanVi { get; set; }

    public bool BoChuongPhan { get; set; }

    public virtual ICollection<TblDeThiHoanVi> TblDeThiHoanVis { get; set; } = new List<TblDeThiHoanVi>();

    public virtual ICollection<TblNhomCauHoi> TblNhomCauHois { get; set; } = new List<TblNhomCauHoi>();
}
