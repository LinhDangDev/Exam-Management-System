using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblNhomCauHoiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaNhom { get; set; }

    public int ThuTu { get; set; }

    public virtual TblDeThiHoanVi MaDeHvNavigation { get; set; } = null!;

    public virtual ICollection<TblChiTietDeThiHoanVi> TblChiTietDeThiHoanVis { get; set; } = new List<TblChiTietDeThiHoanVi>();
}
