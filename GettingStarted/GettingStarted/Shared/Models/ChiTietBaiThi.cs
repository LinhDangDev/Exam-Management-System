using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class ChiTietBaiThi
{
    public long MaChiTietBaiThi { get; set; }

    public int MaChiTietCaThi { get; set; }

    public long MaDeHv { get; set; }

    public int MaNhom { get; set; }

    public int MaCauHoi { get; set; }

    public int? CauTraLoi { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public bool? KetQua { get; set; }

    public int ThuTu { get; set; }

    public virtual ChiTietCaThi MaChiTietCaThiNavigation { get; set; } = null!;
}
