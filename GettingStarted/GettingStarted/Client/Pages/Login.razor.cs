using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using GettingStarted.Client.DAL;
using GettingStarted.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using GettingStarted.Client.Authentication;
using System.Net;
using Microsoft.JSInterop;
using System.Web;
using System.Text;
using System.Xml.Linq;
namespace GettingStarted.Client.Pages
{

    public partial class Login
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        AuthenticationStateProvider authStateProvider { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        SinhVien? sv { get; set; }
        UserSession? userSession { get; set; }
        private string ma_so_sinh_vien = "";
        private string password = "";

        protected override async Task OnInitializedAsync()
        {
            sv = new SinhVien();
            await base.OnInitializedAsync();
        }
        private async Task Authenticate()
        {
            if (ma_so_sinh_vien == password)
            {

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var loginResponse = await httpClient.PostAsync($"api/Login/Verify?ma_so_sinh_vien={ma_so_sinh_vien}", null);

                // Kiểm tra xem yêu cầu có thành công không
                if (loginResponse.IsSuccessStatusCode)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await loginResponse.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị mình muốn
                    userSession = JsonSerializer.Deserialize<UserSession>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                    await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);
                    sv = userSession.NavigateSinhVien;
                    // mã hóa mã sinh viên và pass vào link
                    navManager.NavigateTo($"/exam?msv={sv.MaSinhVien}", true);
                }
                else if(loginResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await js.InvokeVoidAsync("alert", "Vui lòng kiểm tra username và password");
                    return;
                }
            }

        }
    }
}
