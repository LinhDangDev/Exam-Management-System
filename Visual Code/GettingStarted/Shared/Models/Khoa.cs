using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class Khoa
{
    public int MaKhoa { get; set; }

    public string? TenKhoa { get; set; }

    public DateTime? NgayThanhLap { get; set; }

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
