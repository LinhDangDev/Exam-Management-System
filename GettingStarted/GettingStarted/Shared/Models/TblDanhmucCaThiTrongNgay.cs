using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblDanhmucCaThiTrongNgay
{
    public int MaCaTrongNgay { get; set; }

    public string TenCaTrongNgay { get; set; } = null!;

    public int GioBatDau { get; set; }

    public int PhutBatDau { get; set; }

    public int GioKetThuc { get; set; }

    public int PhutKetThuc { get; set; }

    public int CaThiCode { get; set; }
}
