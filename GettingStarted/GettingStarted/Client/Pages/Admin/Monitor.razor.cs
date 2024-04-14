using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GettingStarted.Client.Pages.Admin
{
    public partial class Monitor
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        private List<DotThi>? dotThis { get; set; }
        private List<MonHoc>? monHocs { get; set; }
        private List<LopAo>? lopAos { get; set; }
        private List<ChiTietDotThi>? chiTietDotThis { get; set; }
        private List<ChiTietCaThi>? chiTietCaThis { get; set; }
        private List<CaThi>? caThis { get; set; }
        private List<SinhVien>? sinhViens { get; set; }
        private int ma_dot_thi { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Start();
            dotThis = await httpClient.GetFromJsonAsync<List<DotThi>>("api/Monitor/GetAllDotThi");
            DotThi dotThi = new DotThi()
            {
                MaDotThi = -1, TenDotThi = "",
            };
            dotThis?.Insert(0, dotThi);
            await base.OnInitializedAsync();
        }
        private async Task OnChangeDotThiAsync(ChangeEventArgs e)
        {
            ma_dot_thi = int.Parse(e.Value.ToString());
            var response = await httpClient.PostAsync($"api/Monitor/GetMonHoc?ma_dot_thi={ma_dot_thi}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                monHocs = JsonSerializer.Deserialize<List<MonHoc>>(resultString,new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            }
            MonHoc monHoc = new MonHoc()
            {
                MaMonHoc = -1, TenMonHoc = "",
            };
            monHocs?.Insert(0, monHoc);
            StateHasChanged();
        }
        private async Task OnChangeMonThiAsync(ChangeEventArgs e)
        {
            StateHasChanged();
            int ma_mon_hoc = int.Parse(e.Value.ToString());
            var response = await httpClient.PostAsync($"api/Monitor/GetMaPhongThi?ma_mon_hoc={ma_mon_hoc}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                lopAos = JsonSerializer.Deserialize<List<LopAo>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            }
            LopAo lopAo = new LopAo()
            {
                MaLopAo = -1, TenLopAo = "",
            };
            lopAos?.Insert(0, lopAo);
            StateHasChanged();
        }
        private async Task OnChangeLopAoAsync(ChangeEventArgs e)
        {
            int ma_lop_ao = int.Parse(e.Value.ToString());
            var response = await httpClient.PostAsync($"api/Monitor/GetCaThi?ma_dot_thi={ma_dot_thi}&ma_lop_ao={ma_lop_ao}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                caThis = JsonSerializer.Deserialize<List<CaThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            CaThi caThi = new CaThi()
            {
                MaCaThi = -1, MaChiTietDotThi = -1, ThoiGianBatDau = DateTime.Now, MaDeThi = -1, IsActivated = false, ThoiGianThi = -1, KetThuc = false, Approved = false,
            };
            caThis?.Insert(0, caThi);
            StateHasChanged();
        }
        private async Task OnChangeCaThiAsync(ChangeEventArgs e)
        {
            int ma_ca_thi = int.Parse(e.Value.ToString());
            var respone = await httpClient.PostAsync($"api/Monitor/GetAllChiTietCaThi={ma_ca_thi}", null);
            if (respone.IsSuccessStatusCode)
            {
                var resultString = await respone.Content.ReadAsStringAsync();
                chiTietCaThis = JsonSerializer.Deserialize<List<ChiTietCaThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            StateHasChanged();
        }
        private void Start()
        {
            dotThis = new List<DotThi>();
            monHocs = new List<MonHoc>();
            lopAos = new List<LopAo>();
            chiTietDotThis = new List<ChiTietDotThi>();
            caThis = new List<CaThi>();
        }
    }
}
