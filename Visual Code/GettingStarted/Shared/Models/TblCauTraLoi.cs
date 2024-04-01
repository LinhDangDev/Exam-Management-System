using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblCauTraLoi
{
    public int MaCauTraLoi { get; set; }

    public int MaCauHoi { get; set; }

    public int ThuTu { get; set; }

    public string? NoiDung { get; set; }

    public bool LaDapAn { get; set; }

    public bool HoanVi { get; set; }

    public virtual TblCauHoi MaCauHoiNavigation { get; set; } = null!;
}
