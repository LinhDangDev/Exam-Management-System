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

namespace GettingStarted.Client.Pages
{
    public partial class Info
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        ApplicationDataService myData { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        private SinhVien? sinhVien { get; set; }
        private CaThi? caThi { get; set; }
        private MonHoc? monHoc { get; set; }
        private ChiTietCaThi? chiTietCaThi { get; set; }
        string selectoption_cathi = "";
        protected override async Task OnInitializedAsync()
        {
            //processUrl();
            await Start();
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                navManager.NavigateTo("/");
            }
            await base.OnInitializedAsync();
        }
        private async Task getThongTinSV()
        {
            var response = await httpClient.PostAsync($"api/Info/GetThongTinSinhVienTuMSSV?ma_so_sinh_vien={myData.ma_so_sinh_vien}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                sinhVien = JsonSerializer.Deserialize<SinhVien>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            myData.ma_sinh_vien = sinhVien.MaSinhVien;
        }
        private async Task getThongTinCaThi()
        {
            var response = await httpClient.PostAsync($"api/Info/GetThongTinCaThi?ma_sinh_vien={myData.ma_sinh_vien}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                caThi = JsonSerializer.Deserialize<CaThi?>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            myData.ma_ca_thi = caThi.MaCaThi;
        }
        private async Task GetThongTinMonThi()
        {
            var response = await httpClient.PostAsync($"api/Info/GetThongTinMonThi?ma_ca_thi={myData.ma_ca_thi}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                monHoc = JsonSerializer.Deserialize<MonHoc>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            myData.ten_mon_hoc = monHoc.TenMonHoc;
        }
        private async Task getThongTinChiTietCaThi()
        {
            var response = await httpClient.PostAsync($"api/Info/GetChiTietCaThiSelectBy_SinhVien?ma_ca_thi={myData.ma_ca_thi}&ma_sinh_vien={myData.ma_sinh_vien}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietCaThi = JsonSerializer.Deserialize<ChiTietCaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task onClickDangXuat()
        {
            bool result = await js.InvokeAsync<bool>("confirm", "Bạn muốn đăng xuất?");
            if (result)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                navManager.NavigateTo("/", true);
            }
        }
        private async Task OnClickBatDauThi()
        {
            if (!CheckRadioButton())
            {
                await js.InvokeVoidAsync("alert", "Vui lòng chọn ca thi!");
                return;
            }
            await HandleUpdateBatDau();
            await js.InvokeVoidAsync("alert", "Bắt đầu thi.Chúc bạn sớm hoàn thành kết quả tốt nhất");
            navManager.NavigateTo("/exam");
        }
        private async Task HandleUpdateBatDau()
        {
            chiTietCaThi.ThoiGianBatDau = DateTime.Now;
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            await httpClient.PostAsync("api/Info/UpdateBatDau", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private async Task Start()
        {
            sinhVien = new SinhVien();
            caThi = new CaThi();
            var authState = await authenticationState;
            myData.ma_so_sinh_vien = authState?.User.Identity?.Name;
            await getThongTinSV();
            await getThongTinCaThi();
            await GetThongTinMonThi();
            await getThongTinChiTietCaThi();
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
