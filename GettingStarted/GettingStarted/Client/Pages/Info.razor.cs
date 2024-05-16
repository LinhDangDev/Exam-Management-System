using GettingStarted.Client.DAL;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using GettingStarted.Client.Authentication;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GettingStarted.Client.Pages
{
    public partial class Info
    {
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        ApplicationDataService? myData { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        private SinhVien? sinhVien { get; set; }
        private CaThi? caThi { get; set; }
        private MonHoc? monHoc { get; set; }
        private ChiTietCaThi? chiTietCaThi { get; set; }
        string selectoption_cathi = "";
        private System.Timers.Timer? timer { get; set; }
        private string? displayTime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Start();
            Time();
            //xác thực người dùng
            var customAuthStateProvider = (authenticationStateProvider!= null) ? (CustomAuthenticationStateProvider)authenticationStateProvider: null;
            var token = (customAuthStateProvider!= null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                navManager?.NavigateTo("/");
            }
            await base.OnInitializedAsync();
        }
        private async Task getThongTinChiTietCaThi(long ma_sinh_vien)
        {
            // kiểm tra tham số
            if (ma_sinh_vien == -1)
                return;
            HttpResponseMessage? response = null;
                if(httpClient != null)
                    response = await httpClient.PostAsync($"api/Info/GetThongTinChiTietCaThi?ma_sinh_vien={ma_sinh_vien}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietCaThi = JsonSerializer.Deserialize<ChiTietCaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            else
            {
                if(js != null)
                    await js.InvokeAsync<bool>("alert", "Hiện tại thí sinh chưa có ca thi nào. Vui lòng liên hệ quản trị viên");
            }
            if(chiTietCaThi != null && myData != null && chiTietCaThi.MaCaThiNavigation != null)
            {
                myData.chiTietCaThi = chiTietCaThi;
                sinhVien = chiTietCaThi.MaSinhVienNavigation;
                myData.sinhVien = sinhVien;
                caThi = chiTietCaThi.MaCaThiNavigation;
                monHoc = chiTietCaThi.MaCaThiNavigation.MaChiTietDotThiNavigation.MaLopAoNavigation.MaMonHocNavigation;
            }
        }
        private async Task onClickDangXuat()
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn đăng xuất?");
            if (result && authenticationStateProvider != null)
            {
                await UpdateLogout();
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                navManager?.NavigateTo("/", true);
            }
        }
        private async Task UpdateLogout()
        {
            if(httpClient != null && myData != null && myData.sinhVien != null)
                await httpClient.PostAsync($"api/User/UpdateLogout?ma_sinh_vien={myData.sinhVien.MaSinhVien}", null);
        }
        private async Task OnClickBatDauThi()
        {
            // nếu sinh viên đã thi rồi thì sẽ không được thi lại
            //if(chiTietCaThi!= null && chiTietCaThi.DaHoanThanh && js != null)
            //{
            //    await js.InvokeVoidAsync("alert", "Bạn đã thi môn này. Vui lòng chọn môn thi khác");
            //    return;
            //}
            if (!CheckRadioButton() && js!= null)
            {
                await js.InvokeVoidAsync("alert", "Vui lòng chọn ca thi!");
                return;
            }
            await HandleUpdateBatDau();
            if(js != null)
                await js.InvokeVoidAsync("alert", "Bắt đầu thi.Chúc bạn sớm hoàn thành kết quả tốt nhất");
                navManager?.NavigateTo("/exam");
        }
        private async Task HandleUpdateBatDau()
        {
            if(chiTietCaThi != null)
                chiTietCaThi.ThoiGianBatDau = DateTime.Now;
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            if(httpClient != null)
                await httpClient.PostAsync("api/Info/UpdateBatDauThi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private async Task Start()
        {
            sinhVien = new SinhVien();
            caThi = new CaThi();
            displayTime = DateTime.Now.ToString("hh:mm:ss tt");
            var authState = (authenticationState!= null) ? await authenticationState : null;
            // lấy thông tin mã sinh viên từ claim
            long ma_sinh_vien = -1;
            // chuyển đổi string thành long
            if(authState!= null && authState.User.Identity != null)
                long.TryParse(authState.User.Identity.Name, out ma_sinh_vien);
            await getThongTinChiTietCaThi(ma_sinh_vien);
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
            if(timer != null)
                timer.Dispose();
        }
        private bool CheckRadioButton()
        {
            return !string.IsNullOrEmpty(selectoption_cathi);
        }
        void RadioChanged(ChangeEventArgs e)
        {
            selectoption_cathi = "true";
        }
    }
}
