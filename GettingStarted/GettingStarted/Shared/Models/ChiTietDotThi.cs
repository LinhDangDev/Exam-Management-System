using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class ChiTietDotThi
{
    public int MaChiTietDotThi { get; set; }

    public string TenChiTietDotThi { get; set; } = null!;

    public int MaLopAo { get; set; }

    public int MaDotThi { get; set; }

    public string LanThi { get; set; } = null!;

    public virtual ICollection<CaThi> CaThis { get; set; } = new List<CaThi>();

    public virtual DotThi MaDotThiNavigation { get; set; } = null!;

    public virtual LopAo MaLopAoNavigation { get; set; } = null!;
}
