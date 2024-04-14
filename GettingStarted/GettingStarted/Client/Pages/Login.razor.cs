using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GettingStarted.Client.DAL;
using Microsoft.IdentityModel.Tokens;
using GettingStarted.Shared;
namespace GettingStarted.Client.Pages
{

    public partial class Login
    {
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        ApplicationDataService? myData { get; set; }
        SinhVien? sv { get; set; }
        UserSession? userSession { get; set; }
        private string ma_so_sinh_vien = "";
        private string password = "";

        protected override async Task OnInitializedAsync()
        {
            sv = new SinhVien();
            await base.OnInitializedAsync();
        }
        private async Task Verify()
        {
            if (ma_so_sinh_vien == password)
            {

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var response = await httpClient.PostAsync($"api/Login/Verify?ma_so_sinh_vien={ma_so_sinh_vien}", null);

                // Kiểm tra xem yêu cầu có thành công không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await response.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị boolean
                    userSession = JsonSerializer.Deserialize<UserSession>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    sv = userSession.NavigateSinhVien;
                    if (sv != null)
                    {
                        // lưu dữ liệu cho toàn cục, các razor có thể xài biến này
                        myData.ma_so_sinh_vien = sv.MaSoSinhVien;
                        myData.ma_sinh_vien = sv.MaSinhVien;
                        navManager.NavigateTo("/exam");
                    }
                }
            }

        }
    }
}
