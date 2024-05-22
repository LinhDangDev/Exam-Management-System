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
        public static readonly int GIAY_CAP_NHAT = 30; // tự động lưu bài sau n (giây)
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
        private SinhVien? sinhVien { get; set; }
        private CaThi? caThi { get; set; }
        private ChiTietCaThi? chiTietCaThi { get; set; }
        private static List<TblChiTietDeThiHoanVi>? chiTietDeThiHoanVis { get; set; }
        private List<TblNhomCauHoi>? nhomCauHois { get; set; }
        private static List<TblCauHoi>? cauHois { get; set; }
        private static List<TblCauTraLoi>? cauTraLois { get; set; }
        private static List<ChiTietBaiThi>? chiTietBaiThis { get; set; }
        private static List<string>? cau_da_chons { get; set; }
        private List<string>? alphabet { get; set; }
        public static List<int>? listDapAn { get; set; }// lưu vết các đáp án sinh viên chọn
        private System.Timers.Timer? timer { get; set; }
        private string? displayTime { get; set; }
        private static int maAudio { get; set; } // phân biệt từng audio để ktra số lần nghe từng audio đó
        private string pattern = @"\$\$|\[|\]"; // các kí hiệu latex
        private async Task checkPage()
        {
            if ((myData == null || myData.chiTietCaThi == null || myData.sinhVien == null) && js != null)
            {
                await js.InvokeVoidAsync("alert", "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại");
                navManager?.NavigateTo("/info");
                return;
            }
            if(myData != null && myData.chiTietCaThi != null)
            {
                khoiTaoBanDau();
                chiTietCaThi = myData.chiTietCaThi;
                caThi = myData.chiTietCaThi.MaCaThiNavigation;
                sinhVien = myData.sinhVien;
                await Start();
                Time(); // xử lí countdown
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await checkPage();
            alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
            //xác thực người dùng
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
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
        private async Task getChiTietDeThiHoanVi(long? ma_de_hoan_vi)
        {
            HttpResponseMessage? response = null;
            if(httpClient != null)
                response = await httpClient.PostAsync($"api/Exam/GetThongTinChiTietDeThiHV?ma_de_thi_hoan_vi={ma_de_hoan_vi}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietDeThiHoanVis = JsonSerializer.Deserialize<List<TblChiTietDeThiHoanVi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if (chiTietDeThiHoanVis != null && myData != null)
                myData.chiTietDeThiHoanVis = chiTietDeThiHoanVis;
        }

        private async Task getNoiDungCauHoi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            HttpResponseMessage? response = null;
            if(httpClient != null)
                response = await httpClient.PostAsync("api/Exam/GetNoiDungCauHoi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                cauHois = JsonSerializer.Deserialize<List<TblCauHoi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task getNoiDungCauHoiNhom()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.PostAsync("api/Exam/GetNoiDungCauHoiNhom", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                nhomCauHois = JsonSerializer.Deserialize<List<TblNhomCauHoi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task onClickNopBai()
        {
            if(js != null)
            {
                var result = await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn nộp bài?");
                if (result)
                {
                    await UpdateChiTietBaiThi();
                    if (myData != null)
                    {
                        myData.chiTietBaiThis = chiTietBaiThis;
                        myData.listDapAnKhoanh = listDapAn;
                    }
                    navManager?.NavigateTo("/result");
                }
            }
        }
        private void khoiTaoBanDau()
        {
            maAudio = 0;
            chiTietBaiThis = new List<ChiTietBaiThi>();
            sinhVien = new SinhVien();
            if (myData != null)
                sinhVien = myData.sinhVien;
            caThi = new CaThi();
            chiTietCaThi = new ChiTietCaThi();
            chiTietDeThiHoanVis = new List<TblChiTietDeThiHoanVi>();
            nhomCauHois = new List<TblNhomCauHoi>();
            cauHois = new List<TblCauHoi>();
            cauTraLois = new List<TblCauTraLoi>();
            listDapAn = new List<int>();
            cau_da_chons = new List<string>();
        }
        private async Task Start()
        {
            if(myData != null && myData.chiTietCaThi != null)
            {
                await getChiTietDeThiHoanVi(myData.chiTietCaThi.MaDeThi);
                await getNoiDungCauHoiNhom();
                await modifyNhomCauHoi();
                await getNoiDungCauHoi();
            }
            // khởi tạo giá trị list câu hỏi ban đầu
            if(chiTietDeThiHoanVis != null)
                taoListCauHoiRong(chiTietDeThiHoanVis.Count);
            await InsertChiTietBaiThi();
            ProcessTiepTucThi();
        }
        private void taoListCauHoiRong(int tong_so_cau_hoi)
        {
            for(int i = 0; i < tong_so_cau_hoi; i++)
            {
                if(listDapAn != null)
                    listDapAn.Add(0);
            }
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            int tong_so_giay = 0;
            // cập nhật thời gian thi còn lại cho sinh viên nếu bị out
            int? thoi_gian_con_lai = (int?)thoiGianConLaiLooseData();
            //if (thoi_gian_con_lai != null)
            //{
            //    // trừ khoảng thời gian sinh đã làm trước đó
            //    tong_so_giay -= (int)thoi_gian_con_lai;
            //}
            if (caThi != null)
            {
                tong_so_giay += caThi.ThoiGianThi * 60;
                displayTime = FormatTime(tong_so_giay);
            }
            int so_giay_hien_tai = tong_so_giay;
            timer.Elapsed += async (sender, e) =>
            {
                so_giay_hien_tai--;
                // cứ mỗi n giây thì hệ thống tự động lưu bài của SV
                if(so_giay_hien_tai % GIAY_CAP_NHAT == 0)
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
                    await onClickNopBai();
                    navManager?.NavigateTo("/result");
                }
            };
        }
        // insert các các dòng dữ liệu chiTietBaiThi, và lấy dữ liệu của chiTietbaiThi
        private async Task InsertChiTietBaiThi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
            HttpResponseMessage? response = null;
            if (httpClient != null && chiTietCaThi != null)
                response = await httpClient.PostAsync($"api/Exam/InsertChiTietBaiThi?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response!= null && response.IsSuccessStatusCode)
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
        // Xử lí việc thí sinh bị out ra khi đang làm bài
        private void ProcessTiepTucThi()
        {
            DateTime? thoi_gian = chiTietCaThi?.ThoiGianBatDau;
            thoi_gian = thoi_gian?.AddMilliseconds(GIAY_CAP_NHAT);
            // check thời gian bắt đầu làm bài vì bài lưu lần đầu sau n phút, tức là nếu sinh viên làm chưa tới n phút thì chắc chắn dữ liệu chưa có
            if(chiTietBaiThis != null && thoi_gian != null && thoi_gian < DateTime.Now)
            {
                foreach(var item in chiTietBaiThis)
                {
                    if(item.CauTraLoi != null && js != null)
                    {
                        var listCauTraLoi = cauHois?.FirstOrDefault(p => p.MaCauHoi == item.MaCauHoi)?.TblCauTraLois.ToList();
                        var thu_tu_tra_loi_thuc_te = listCauTraLoi?.FirstOrDefault(p => p.MaCauTraLoi == item.CauTraLoi)?.ThuTu.ToString();
                        // lấy thông tin thứ tự của câu hỏi cho thẻ tag a 
                        int so_cau_hoi = timViTriCauHoiLooseData(item.MaNhom, item.MaCauHoi);
                        var ds_hoanvi_traloi = chiTietDeThiHoanVis?.FirstOrDefault(p => p.MaNhom == item.MaNhom && p.MaCauHoi == item.MaCauHoi)?.HoanViTraLoi;
                        int? thu_tu_tra_loi_hoan_vi = null;
                        if (thu_tu_tra_loi_thuc_te != null)
                            thu_tu_tra_loi_hoan_vi = ds_hoanvi_traloi?.IndexOf(thu_tu_tra_loi_thuc_te.ToString()) + 1;
                        string id = $"btn Nhom:{item.MaNhom} CauHoi:{item.MaCauHoi} ThuTu:{thu_tu_tra_loi_hoan_vi}";
                        cau_da_chons?.Add(id); // thêm id danh tính cho button
                        string id_so_cau_hoi = "SoCauHoi: " + so_cau_hoi;
                        cau_da_chons?.Add(id_so_cau_hoi); // thêm id danh tính cho cho textbox số câu hỏi
                        // cập nhật lại danh sách sinh viên đã khoanh
                        if (listDapAn != null)
                            listDapAn[so_cau_hoi - 1] = (int)item.CauTraLoi;
                    }
                }
            }
        }
        private int timViTriCauHoiLooseData(int ma_nhom, int ma_cau_hoi)
        {
            int so_cau_hoi = 0;
            if (chiTietDeThiHoanVis != null)
            {
                foreach(var item in chiTietDeThiHoanVis)
                {
                    so_cau_hoi++;
                    if (item.MaNhom == ma_nhom && item.MaCauHoi == ma_cau_hoi)
                        return so_cau_hoi;
                }
            }
            return 0;
        }
        private double? thoiGianConLaiLooseData()
        {
            TimeSpan? result = null;
            if(chiTietCaThi != null && chiTietCaThi.DaHoanThanh == false && chiTietCaThi.DaThi == true)
            {
                DateTime? thoi_gian_luu_lan_cuoi = chiTietBaiThis?[0].NgayCapNhat;
                DateTime? thoi_gian_bat_dau_thi = chiTietBaiThis?[0].NgayTao;
                result = thoi_gian_luu_lan_cuoi - thoi_gian_bat_dau_thi;
            }
            return result?.TotalSeconds;
        }

        private async Task UpdateChiTietBaiThi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietBaiThis);
            if (httpClient != null)
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
            if(chiTietDeThiHoanVis != null)
            {
                foreach (var item in chiTietDeThiHoanVis)
                {
                    if (item.MaNhom != stt)
                    {
                        stt = item.MaNhom;
                        var cauHoiNhom = nhomCauHois?.FirstOrDefault(p => p.MaNhom == stt);
                        if (cauHoiNhom != null)
                        {
                            // xử lí kiểu dữ liệu âm thanh
                            cauHoiNhom.NoiDung = await handleAudio(cauHoiNhom.NoiDung);
                        }
                    }
                }
            }
        }
        private async Task<string?> handleAudio(string? text)
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
                int solannghe = 0;
                if(myData != null && myData.chiTietCaThi != null)
                    solannghe = await getSoLanNghe(myData.chiTietCaThi.MaChiTietCaThi, filename);
                // thêm 1 số function, lưu ý có 2 whitespace đầu và cuối
                string addText = $" controlsList=\"nodownload\" onplay=\"playMusic(this, 'listenedCount{maAudio}')\" onpause=\"pauseMusic(this, 'listenedCount{maAudio}')\" ";
                int index = text.IndexOf("<audio"); // tìm chỉ số của "<audio"
                // thêm thông tin số lần nghe
                text = text.Insert(index + "<audio".Length, addText);
                // với thuộc tính id ta có thể thay đổi số lần nghe
                string addButton = $"<input class=\"fileAudio\" id=\"listenedCount{maAudio}\" type=\"button\" value=\"{solannghe}\" style=\"border-radius: 50%; border-style: none; font: 16px; cursor:not-allowed;\"></input>";
                text = text.Insert(text.Length, addButton);
                maAudio++;
            }
            return text ?? null;
        }
        private async Task<int> getSoLanNghe(int ma_chi_tiet_ca_thi, string filename)
        {
            HttpResponseMessage? response = null;
            if(httpClient != null)
                response = await httpClient.PostAsync($"api/Exam/AudioListendCount?ma_chi_tiet_ca_thi={ma_chi_tiet_ca_thi}&filename={filename}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return 0;
        }
        
        [JSInvokable] // Đánh dấu hàm để có thể gọi từ JavaScript
        public static Task<int> GetDapAnFromJavaScript(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        {
            // Xử lý giá trị được truyền từ JavaScript
            if(listDapAn != null)
                listDapAn[vi_tri_cau_hoi - 1] = ma_cau_tra_loi;
            ChiTietBaiThi? chiTietBaiThi = chiTietBaiThis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
            TblChiTietDeThiHoanVi? chiTietDeThiHoanVi = chiTietDeThiHoanVis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
            if(chiTietBaiThi != null && chiTietDeThiHoanVi != null)
            {
                chiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
                chiTietBaiThi.KetQua = (chiTietBaiThi.CauTraLoi == chiTietDeThiHoanVi.DapAn) ? true : false;
            }
            return Task.FromResult<int>(ma_cau_tra_loi);
        }
    }
}
