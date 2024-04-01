using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblCauHoi
{
    public int MaCauHoi { get; set; }

    public string? TieuDe { get; set; }

    public int KieuNoiDung { get; set; }

    public string? NoiDung { get; set; }

    public string? GhiChu { get; set; }

    public bool? HoanVi { get; set; }

    public virtual ICollection<TblCauTraLoi> TblCauTraLois { get; set; } = new List<TblCauTraLoi>();
}
