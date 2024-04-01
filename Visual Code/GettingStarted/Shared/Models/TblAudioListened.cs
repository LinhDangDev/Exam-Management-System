using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class TblAudioListened
{
    public long ListenId { get; set; }

    public int MaChiTietCaThi { get; set; }

    public string FileName { get; set; } = null!;

    public int ListenedCount { get; set; }
}
