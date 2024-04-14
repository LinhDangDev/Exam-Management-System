using GettingStarted.Client.DAL;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using GettingStarted.Client.Authentication;
using System.Net.Http.Headers;
using System.Net;

namespace GettingStarted.Client.Pages
{
    public partial class Exam
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        ApplicationDataService myData { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager navManager { get; set;}
        private SinhVien? sinhVien { get; set; }
        private CaThi? caThi { get; set; }
        private ChiTietCaThi? chiTietCaThi { get; set; }
        private List<TblChiTietDeThiHoanVi>? chiTietDeThiHoanVis { get; set; }
        private List<TblNhomCauHoi>? nhomCauHois { get; set; }
        private List<TblCauHoi>? cauHois {get; set;}
        private List<TblCauTraLoi>? cauTraLois { get; set; }
        private int thu_tu_ma_nhom { get; set; }
        private int thu_tu_ma_cau_hoi { get; set; }
        private List<string> alphabet { get; set; }
        private System.Timers.Timer timer { get; set; }
        private string displayTime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            /////////////////////////////////////////
            myData.ma_ca_thi = 1;
            ////////////////////////////////////////
            //xử lí url để lấy giá trị mã sinh viên
            thu_tu_ma_cau_hoi = thu_tu_ma_nhom = -1;
            alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
            processUrl();
            await Start();
            Time(); // xử lí countdown
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if(!string.IsNullOrWhiteSpace(token))
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
            var response = await httpClient.PostAsync($"api/Exam/GetThongTinSinhVien?ma_sinh_vien={myData.ma_sinh_vien}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                sinhVien = JsonSerializer.Deserialize<SinhVien>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getThongTinChiTietCaThi()
        {
            var response = await httpClient.PostAsync($"api/Exam/GetChiTietCaThiSelectBy_SinhVien?ma_ca_thi={myData.ma_ca_thi}&ma_sinh_vien={myData.ma_sinh_vien}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietCaThi = JsonSerializer.Deserialize<ChiTietCaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getThongTinCaThi()
        {
            var response = await httpClient.PostAsync($"api/Exam/GetThongTinCaThi?ma_ca_thi={myData.ma_ca_thi}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                caThi = JsonSerializer.Deserialize<CaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getChiTietDeThiHoanVi()
        {
            var response = await httpClient.PostAsync($"api/Exam/HandleAllQuestionExams?ma_de_thi_hoan_vi={chiTietCaThi.MaDeThi}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietDeThiHoanVis = JsonSerializer.Deserialize<List<TblChiTietDeThiHoanVi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getNoiDungMaNhom()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            var response = await httpClient.PostAsync("api/Exam/GetNoiDungCauHoiNhom", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                nhomCauHois = JsonSerializer.Deserialize<List<TblNhomCauHoi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getNoiDungCauHoi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            var response = await httpClient.PostAsync("api/Exam/GetNoiDungCauHoi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                cauHois = JsonSerializer.Deserialize<List<TblCauHoi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getAllCauTraLoi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            var response = await httpClient.PostAsync("api/Exam/GetAllCauTraLoi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                cauTraLois = JsonSerializer.Deserialize<List<TblCauTraLoi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task Start()
        {
            sinhVien = new SinhVien();
            caThi = new CaThi();
            chiTietCaThi = new ChiTietCaThi();
            chiTietDeThiHoanVis = new List<TblChiTietDeThiHoanVi>();
            nhomCauHois = new List<TblNhomCauHoi>();
            cauHois = new List<TblCauHoi>();
            cauTraLois = new List<TblCauTraLoi>();
            await getThongTinSV();
            await getThongTinChiTietCaThi();
            await getThongTinCaThi();
            await getChiTietDeThiHoanVi();
            await getNoiDungMaNhom();
            await getNoiDungCauHoi();
            await getAllCauTraLoi();
        }

        private void processUrl()
        {
            string url = navManager.Uri.ToString();
            //thực hiện cắt chuỗi để lấy dữ liệu msv={encode}
            int start = url.IndexOf("=");
            string encodedData = url.Substring(start + 1, url.Length - start - 1);
            Console.WriteLine(encodedData);
            myData.ma_sinh_vien = long.Parse(WebUtility.UrlDecode(encodedData));
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            int tong_so_giay = caThi.ThoiGianThi * 60;
            int so_gio_hien_tai = tong_so_giay;
            displayTime = FormatTime(tong_so_giay);
            timer.Elapsed += (sender, e) =>
            {
                so_gio_hien_tai--;
                if (so_gio_hien_tai >= 0)
                {
                    displayTime = FormatTime(so_gio_hien_tai);
                    InvokeAsync(StateHasChanged); // Cập nhật giao diện người dùng
                }
                else
                {
                    timer.Stop(); // Dừng timer khi countdown kết thúc
                }
            };
        }
        string FormatTime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";// format từ giây sang phút/giây với D2 là 2 số nguyên
        }
        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
