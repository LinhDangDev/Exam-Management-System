using Blazored.SessionStorage;
using GettingStarted.Client.Extensions;
using GettingStarted.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq.Expressions;
using System.Security.Claims;

namespace GettingStarted.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
                if(userSession == null)
                {
                    // không tìm thấy người dùng thì trả về vô danh
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Username)
                }, "JwtAuth"));
                // trả về giấy xác thực người dùng
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                // nếu không xác thực được người dùng hoặc lỗi xác thực thì trả về người vô danh
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            // cập nhật về việc người dùng login và logout - cập nhật lại sessionStorage
            // và thông báo tình trạng việc xác thực đã có sự thay đổi
            // khi log out thì tham số userSession sẽ là null
            ClaimsPrincipal claimsPrincipal;
            if(userSession != null) // khi nguời dùng log in
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Username)
                }));
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpireIn);
                await _sessionStorageService.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else // khi người dùng log out
            {
                claimsPrincipal = _anonymous;
                await _sessionStorageService.RemoveItemAsync("UserSession");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task<string> GetToken()
        {
            //để các các razor component lấy mã token
            var result = string.Empty;
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
                {
                    result = userSession.Token;
                }
            }
            catch { }
            return result;
        }
    }
}
