using GettingStarted.Client.DAL;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using GettingStarted.Client.Authentication;
using System.Net.Http.Headers;
namespace GettingStarted.Client.Pages;

public partial class Result
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
    private List<TblChiTietDeThiHoanVi>? chiTietDeThiHoanVis { get; set; }
    private List<int>? listDapAn { get; set; }
    private double diem { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await Start();
        //if (caThi == null || caThi.TenCaThi == null)
        //{
        //    await js.InvokeVoidAsync("alert", "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại");
        //    navManager.NavigateTo("/info");
        //}
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
        var response = await httpClient.PostAsync($"api/Result/GetThongTinSinhVien?ma_sinh_vien={myData.ma_sinh_vien}", null);
        if (response.IsSuccessStatusCode)
        {
            var resultString = await response.Content.ReadAsStringAsync();
            sinhVien = JsonSerializer.Deserialize<SinhVien>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
    private async Task getThongTinCaThi()
    {
        var response = await httpClient.PostAsync($"api/Result/GetThongTinCaThi?ma_ca_thi={myData.ma_ca_thi}", null);
        if (response.IsSuccessStatusCode)
        {
            var resultString = await response.Content.ReadAsStringAsync();
            caThi = JsonSerializer.Deserialize<CaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
    private async Task getChiTietDeThiHoanVi()
    {
        // gọi lấy lại từ ExamController
        var response = await httpClient.PostAsync($"api/Exam/HandleAllQuestionExams?ma_de_thi_hoan_vi={myData.ma_de_thi_hoan_vi}", null);
        if (response.IsSuccessStatusCode)
        {
            var resultString = await response.Content.ReadAsStringAsync();
            chiTietDeThiHoanVis = JsonSerializer.Deserialize<List<TblChiTietDeThiHoanVi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
    private async Task GetListDapAn()
    {
        var jsonString = JsonSerializer.Serialize(chiTietDeThiHoanVis);
        var response = await httpClient.PostAsync("api/Result/GetListDapAn", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            var resultString = await response.Content.ReadAsStringAsync();
            listDapAn = JsonSerializer.Deserialize<List<int>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
    private void tinhDiemSo()
    {
        diem = 0;
        double diem_tung_cau = (10.0 / chiTietDeThiHoanVis.Count);
        int length = myData.listSVKhoanh.Count;
        for(int i = 0; i < length; i++)
        {
            //nếu trùng đáp án thì có điểm, còn không trùng thì không có
            if (myData.listSVKhoanh[i] == listDapAn[i])
            {
                diem += diem_tung_cau;
            }
        }
        diem = cachQuyDoiDiem(diem);
    }
    private double cachQuyDoiDiem(double diem)
    {
        diem = Math.Round(diem, 2);// lấy 2 số thập phân
        double so_phay = diem % 100;
        if(so_phay < 25)
            return Math.Floor(diem);
        if(so_phay >= 25 && so_phay < 75)
            return Math.Floor(diem) + 0.5;
        return Math.Ceiling(diem);

    }
    private async Task Start()
    {
        sinhVien = new SinhVien();
        caThi = new CaThi();
        chiTietCaThi = new ChiTietCaThi();
        chiTietDeThiHoanVis = new List<TblChiTietDeThiHoanVi>();
        await getThongTinSV();
        await getThongTinCaThi();
        await getChiTietDeThiHoanVi();
        await GetListDapAn();
        tinhDiemSo();
    }
}