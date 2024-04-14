using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class CaThi
{
    public int MaCaThi { get; set; }

    public string? TenCaThi { get; set; }

    public int MaChiTietDotThi { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public int MaDeThi { get; set; }

    public bool IsActivated { get; set; }

    public DateTime? ActivatedDate { get; set; }

    public int ThoiGianThi { get; set; }

    public bool KetThuc { get; set; }

    public DateTime? ThoiDiemKetThuc { get; set; }

    public string? MatMa { get; set; }

    public bool Approved { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public string? ApprovedComments { get; set; }

    public virtual ICollection<ChiTietCaThi> ChiTietCaThis { get; set; } = new List<ChiTietCaThi>();

    public virtual ChiTietDotThi MaChiTietDotThiNavigation { get; set; } = null!;
}
