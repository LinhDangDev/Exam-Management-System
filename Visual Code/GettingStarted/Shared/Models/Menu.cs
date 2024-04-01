using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string MenuTitle { get; set; } = null!;

    public string? MenuDescription { get; set; }

    public string? MenuUrl { get; set; }

    public string? MenuValuepath { get; set; }

    public int? MenuParentId { get; set; }

    public int MenuOrder { get; set; }
}
