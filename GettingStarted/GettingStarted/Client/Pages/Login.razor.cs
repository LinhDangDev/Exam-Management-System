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
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
namespace GettingStarted.Client.Pages
{

    public partial class Login
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        SinhVien? sv { get; set; }
        UserSession? userSession { get; set; }
        private string? ma_so_sinh_vien = "";
        private string? password = "";

        protected override async Task OnInitializedAsync()
        {
            sv = new SinhVien();
            //nếu đã tồn tại người dùng đăng nhập trước đó, chuyển trang
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var authState = await authenticationState;
                ma_so_sinh_vien = authState?.User.Identity?.Name;
                await getThongTinSinhVienByMSSV();
                await UpdateLogin();
                navManager.NavigateTo("/info");
            }
            await base.OnInitializedAsync();
        }
        private async Task<bool> Check()
        {
            bool checkSinhVien = false;
            if (ma_so_sinh_vien == password)
            {
                await getThongTinSinhVienByMSSV();
                checkSinhVien = (sv.MaSinhVien != 0) ? true : false;
            }
            return checkSinhVien;
        }
        private async Task getThongTinSinhVienByMSSV()
        {
            var loginResponse = await httpClient.PostAsync($"api/Login/Check?ma_so_sinh_vien={ma_so_sinh_vien}", null);
            if (loginResponse.IsSuccessStatusCode)
            {
                // Đọc kết quả từ phản hồi
                var resultString = await loginResponse.Content.ReadAsStringAsync();

                // Chuyển đổi kết quả từ chuỗi JSON thành giá trị mình muốn
                sv = JsonSerializer.Deserialize<SinhVien>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task UpdateLogin()
        {
            await httpClient.PostAsync($"api/Login/UpdateLogin?ma_sinh_vien={sv.MaSinhVien}&last_log_in={DateTime.Now}", null);
        }
        private async Task Authenticate()
        {
            if (await Check())
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
                    var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                    await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);
                    sv = userSession.NavigateSinhVien;
                    navManager.NavigateTo("/info", true);
                }
                else if(loginResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await js.InvokeVoidAsync("alert", "Vui lòng kiểm tra username và password");
                    return;
                }
            }
            else
            {
                await js.InvokeVoidAsync("alert", "Username và password không trùng khớp");
                return;
            }
            await UpdateLogin();
        }
    }
}
