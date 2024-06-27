using Microsoft.AspNetCore.Components;

namespace GettingStarted.Client.Pages.Admin.MessageBox
{
    public partial class MBCongGio
    {
        [Parameter]
        public string? tenSinhVien { get; set; }
        [Parameter]
        public string? MSSV { get; set; }
        [Parameter]
        public string? tenCaThi { get; set; }
        [Parameter]
        public string? tenMonThi { get; set; }
        [Parameter]
        public DateTime? ngayThi { get; set; }
        [Parameter]
        public int? thoiLuongThi { get; set; }
        [Parameter]
        public EventCallback onClickLuu { get; set; }
        [Parameter]
        public EventCallback onClickThoat { get; set; }
        public int? thoiGianCongThem { get; set; }
        public string? lyDoCong { get; set; }

    }
}
