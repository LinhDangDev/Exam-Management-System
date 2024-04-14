using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class LopAo
{
    public int MaLopAo { get; set; }

    public string? TenLopAo { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public int? MaMonHoc { get; set; }

    public virtual ICollection<ChiTietDotThi> ChiTietDotThis { get; set; } = new List<ChiTietDotThi>();

    public virtual MonHoc? MaMonHocNavigation { get; set; }
}
