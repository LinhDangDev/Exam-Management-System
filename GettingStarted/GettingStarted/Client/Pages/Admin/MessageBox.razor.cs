using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace GettingStarted.Client.Pages.Admin
{
    public partial class MessageBox
    {
        [Parameter]
        public string? tenCaThi { get; set; }
        [Parameter]
        public string? ngayThi { get; set; }
        [Parameter]
        public int? thoiLuongThi { get; set; }
        [Parameter]
        public string? tenMonThi { get; set; }
        [Parameter]
        public string? tenDotThi { get; set; }
        [Parameter]
        public string? thoiGianBatDauThi { get; set; }
        [Parameter]
        public bool trangThai { get; set; }

        // EventCallBack
        [Parameter]
        public EventCallback onClickKichHoat { get; set; }
        [Parameter]
        public EventCallback onClickHuyKichHoat { get; set; }

        [Parameter]
        public EventCallback onClickDungCaThi { get; set; }
        [Parameter]
        public EventCallback onClickKetThucCaThi { get; set; }
        [Parameter]
        public EventCallback onClickThoat { get; set; }

        private System.Timers.Timer? timer { get; set; }
        private string? displayTime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Time();
            await base.OnInitializedAsync();
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += (sender, e) =>
            {
                displayTime = DateTime.Now.ToString("hh:mm:ss tt");
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            };
        }
        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
        }
    }
}
