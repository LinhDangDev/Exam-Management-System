using GettingStarted.Client.DAL;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using GettingStarted.Client.Authentication;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.JSInterop;

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
        NavigationManager navManager { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        private SinhVien? sinhVien { get; set; }
        private CaThi? caThi { get; set; }
        private ChiTietCaThi? chiTietCaThi { get; set; }
        private static List<TblChiTietDeThiHoanVi>? chiTietDeThiHoanVis { get; set; }
        private List<TblNhomCauHoi>? nhomCauHois { get; set; }
        private List<TblCauHoi>? cauHois { get; set; }
        private static List<TblCauTraLoi>? cauTraLois { get; set; }
        private static List<ChiTietBaiThi>? chiTietBaiThis { get; set; }
        private int thu_tu_ma_nhom { get; set; }
        private int thu_tu_ma_cau_hoi { get; set; }
        private List<string> alphabet { get; set; }
        public static List<int> listDapAn { get; set; }// lưu vết các đáp án sinh viên chọn
        private System.Timers.Timer timer { get; set; }
        private string displayTime { get; set; }
        private static int maAudio { get; set; } // phân biệt từng audio để ktra số lần nghe từng audio đó
        protected override async Task OnInitializedAsync()
        {
            thu_tu_ma_cau_hoi = thu_tu_ma_nhom = -1;
            alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
            await Start();
            if(caThi == null || caThi.TenCaThi == null)
            {
                await js.InvokeVoidAsync("alert", "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại");
                navManager.NavigateTo("/info");
                return;
            }
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
            myData.ma_de_thi_hoan_vi = chiTietCaThi.MaDeThi;
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
        private async Task onClickThoat()
        {
            bool result = await js.InvokeAsync<bool>("exitExam");
            if (result)
            {
                navManager.NavigateTo("/info");
            }
        }
        private async Task onClickLuuBai()
        {
            myData.listDapAnKhoanh = listDapAn;
            await js.InvokeVoidAsync("saveExam");
            await UpdateChiTietBaiThi();
        }
        private async Task onClickNopBai()
        {
            await onClickLuuBai();
            var result = await js.InvokeAsync<bool>("submitExam");
            if (result)
                navManager.NavigateTo("/result");
        }
        private async Task Start()
        {
            maAudio = 0;
            chiTietBaiThis = new List<ChiTietBaiThi>();
            sinhVien = new SinhVien();
            caThi = new CaThi();
            chiTietCaThi = new ChiTietCaThi();
            chiTietDeThiHoanVis = new List<TblChiTietDeThiHoanVi>();
            nhomCauHois = new List<TblNhomCauHoi>();
            cauHois = new List<TblCauHoi>();
            cauTraLois = new List<TblCauTraLoi>();
            listDapAn = new List<int>();
            await getThongTinSV();
            await getThongTinChiTietCaThi();
            await getThongTinCaThi();
            await getChiTietDeThiHoanVi();
            await getNoiDungMaNhom();
            await modifyNhomCauHoi();
            await getNoiDungCauHoi();
            await getAllCauTraLoi();
            // khởi tạo giá trị list câu hỏi ban đầu
            taoListCauHoiRong(chiTietDeThiHoanVis.Count);
            InsertChiTietBaiThi();
        }
        private void taoListCauHoiRong(int tong_so_cau_hoi)
        {
            for(int i = 0; i < tong_so_cau_hoi; i++)
            {
                listDapAn.Add(0);
            }
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            int tong_so_giay = caThi.ThoiGianThi * 60;
            int so_giay_hien_tai = tong_so_giay;
            displayTime = FormatTime(tong_so_giay);
            timer.Elapsed += async (sender, e) =>
            {
                so_giay_hien_tai--;
                // cứ mỗi n giây thì hệ thống tự động lưu bài của SV
                if(so_giay_hien_tai % 30 == 0)
                {
                    await UpdateChiTietBaiThi();
                }
                if (so_giay_hien_tai >= 0)
                {
                    displayTime = FormatTime(so_giay_hien_tai);
                    await InvokeAsync(StateHasChanged); // Cập nhật giao diện người dùng
                }
                else
                {
                    timer.Stop(); // Dừng timer khi countdown kết thúc
                    await onClickLuuBai();
                    navManager.NavigateTo("/result");
                }
            };
        }
        // insert các các dòng dữ liệu chiTietBaiThi, và lấy dữ liệu của chiTietbaiThi
        private async void InsertChiTietBaiThi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            var response = await httpClient.PostAsync($"api/Exam/InsertChiTietBaiThi?ma_chi_tiet_ca_thi={chiTietCaThi?.MaChiTietCaThi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietBaiThis = JsonSerializer.Deserialize<List<ChiTietBaiThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if(chiTietBaiThis != null && chiTietCaThi != null)
            {
                foreach (var item in chiTietBaiThis)
                    item.MaChiTietCaThiNavigation = chiTietCaThi;
            }
        }
        private async Task UpdateChiTietBaiThi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietBaiThis);
            await httpClient.PostAsync("api/Exam/UpdateChiTietBaiThi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private string FormatTime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";// format từ giây sang phút/giây với D2 là 2 số nguyên
        }
        public void Dispose()
        {
            if(timer != null)
            {
                timer.Dispose();
            }
        }
        // xử lí các dạng kiểu dữ liệu
        private async Task modifyNhomCauHoi()
        {
            int stt = 0;
            foreach (var item in chiTietDeThiHoanVis)
            {
                if (item.MaNhom != stt)
                {
                    stt = item.MaNhom;
                    var cauHoiNhom = nhomCauHois?.FirstOrDefault(p => p.MaNhom == stt);
                    if(cauHoiNhom != null)
                    {
                        // xử lí kiểu dữ liệu âm thanh
                        cauHoiNhom.NoiDung = await handleAudio(cauHoiNhom.NoiDung);
                    }
                }
            }
        }
        private async Task<string> handleAudio(string? text)
        {
            //tìm thẻ tag audio
            if (!string.IsNullOrEmpty(text) && text.Contains("<audio"))
            {
                // lấy tên file name của audio
                int indexsource = text.IndexOf("source src=\"") + "source src=\"".Length; // phần đầu của source
                int indexendsource = text.IndexOf("\"/> </audio>"); // phần cuối của source
                string source = text.Substring(indexsource, indexendsource - indexsource);// source file ./audio/hello.mp3
                int index_filename = source.LastIndexOf("/");
                string filename = source.Substring(index_filename + 1);// tên filename
                int solannghe = await getSoLanNghe(chiTietCaThi.MaChiTietCaThi, filename);
                // thêm 1 số function, lưu ý có 2 whitespace đầu và cuối
                string addText = $" controlsList=\"nodownload\" onplay=\"playMusic(this, 'listenedCount{maAudio}')\" onpause=\"pauseMusic(this, 'listenedCount{maAudio}')\" ";
                int index = text.IndexOf("<audio"); // tìm chỉ số của "<audio"
                // thêm thông tin số lần nghe
                text = text.Insert(index + "<audio".Length, addText);
                // với thuộc tính id ta có thể thay đổi số lần nghe
                // với thuộc tính class ta css và thuộc tính readonly ngăn cấm sinh viên chỉnh sửa con số
                string addButton = $"<input class=\"fileAudio\" id=\"listenedCount{maAudio}\" type=\"button\" value=\"{solannghe}\" style=\"border-radius: 50%; border-style: none; font: 16px; cursor:not-allowed;\"></input>";
                text = text.Insert(text.Length, addButton);
                maAudio++;
            }
            return text;
        }
        private async Task<int> getSoLanNghe(int ma_chi_tiet_ca_thi, string filename)
        {
            var response = await httpClient.PostAsync($"api/Exam/AudioListendCount?ma_chi_tiet_ca_thi={ma_chi_tiet_ca_thi}&filename={filename}", null);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return 0;
        }
        
        [JSInvokable] // Đánh dấu hàm để có thể gọi từ JavaScript
        public static Task<int> GetDapAnFromJavaScript(int vi_tri_cau_hoi, int vi_tri_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        {
            // Xử lý giá trị được truyền từ JavaScript
            listDapAn[vi_tri_cau_hoi - 1] = vi_tri_cau_tra_loi;
            ChiTietBaiThi? chiTietBaiThi = chiTietBaiThis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
            chiTietBaiThi.CauTraLoi = cauTraLois?.FirstOrDefault(p => p.MaCauHoi == ma_cau_hoi && p.ThuTu == vi_tri_cau_tra_loi)?.MaCauTraLoi;
            int? KetQua = chiTietDeThiHoanVis?.FirstOrDefault(p => p.MaCauHoi == ma_cau_hoi && p.MaNhom == ma_nhom)?.DapAn;
            chiTietBaiThi.KetQua = (KetQua == chiTietBaiThi.CauTraLoi) ? true : false;
            return Task.FromResult<int>(vi_tri_cau_tra_loi);
        }
    }
}
