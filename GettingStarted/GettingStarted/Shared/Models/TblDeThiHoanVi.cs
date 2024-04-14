using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblDeThiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaDeThi { get; set; }

    public string? KyHieuDe { get; set; }

    public DateTime NgayTao { get; set; }

    public Guid? Guid { get; set; }

    public virtual TblDeThi MaDeThiNavigation { get; set; } = null!;

    public virtual ICollection<TblNhomCauHoiHoanVi> TblNhomCauHoiHoanVis { get; set; } = new List<TblNhomCauHoiHoanVi>();
}
