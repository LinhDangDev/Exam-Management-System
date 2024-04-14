using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblChiTietDeThiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaNhom { get; set; }

    public int MaCauHoi { get; set; }

    public int ThuTu { get; set; }

    public string? HoanViTraLoi { get; set; }

    public int? DapAn { get; set; }

    public virtual TblNhomCauHoiHoanVi Ma { get; set; } = null!;
}
