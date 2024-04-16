using GettingStarted.Client.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GettingStarted.Client.Pages
{
    public partial class Logout
    {
        [Inject]
        AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        private async Task LogoutPage()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager.NavigateTo("/", true);
        }
    }
}
