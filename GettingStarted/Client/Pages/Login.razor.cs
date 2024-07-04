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
        HttpClient? httpClient { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        SinhVien? sv { get; set; }
        UserSession? userSession { get; set; }
        private string? ma_so_sinh_vien = "";
        private string? password = "";

        protected override async Task OnInitializedAsync()
        {
            sv = new SinhVien();
            //nếu đã tồn tại người dùng đăng nhập trước đó, chuyển trang
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null && authenticationState != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var authState = await authenticationState;
                ma_so_sinh_vien = authState?.User.Identity?.Name;
                navManager?.NavigateTo("/info", true);
            }
            await base.OnInitializedAsync();
        }
        private async Task onClickDangNhap()
        {
            if (ma_so_sinh_vien == password && httpClient != null)
            {

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var loginResponse = await httpClient.PostAsync($"api/User/Verify?ma_so_sinh_vien={ma_so_sinh_vien}", null);

                // Kiểm tra xem yêu cầu có thành công không
                if (loginResponse.IsSuccessStatusCode && authenticationStateProvider != null && navManager != null)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await loginResponse.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị mình muốn
                    userSession = JsonSerializer.Deserialize<UserSession>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;

                    // Cập nhật trạng thái của UserSession lại, chuyển từ Anyousmous thành người có danh tính
                    await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);
                    sv = userSession?.NavigateSinhVien;
                    navManager.NavigateTo("/info", true);
                }
                else if((loginResponse.StatusCode == HttpStatusCode.Unauthorized || !loginResponse.IsSuccessStatusCode) && js != null)
                {
                    await js.InvokeVoidAsync("alert", "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết");
                    return;
                }
            }
            else
            {
                if(js != null)
                {
                    await js.InvokeVoidAsync("alert", "Username và password không trùng khớp");
                }
                return;
            }
        }
    }
}
