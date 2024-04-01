using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblNhomCauHoi
{
    public int MaNhom { get; set; }

    public int MaDeThi { get; set; }

    public string TenNhom { get; set; } = null!;

    public string? NoiDung { get; set; }

    public int SoCauHoi { get; set; }

    public bool HoanVi { get; set; }

    public int ThuTu { get; set; }

    public int MaNhomCha { get; set; }

    public int SoCauLay { get; set; }

    public bool? LaCauHoiNhom { get; set; }

    public virtual TblDeThi MaDeThiNavigation { get; set; } = null!;
}
