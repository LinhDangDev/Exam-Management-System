using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class DotThi
{
    public int MaDotThi { get; set; }

    public string? TenDotThi { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime? ThoiGianKetThuc { get; set; }

    public int? NamHoc { get; set; }

    public virtual ICollection<ChiTietDotThi> ChiTietDotThis { get; set; } = new List<ChiTietDotThi>();
}
