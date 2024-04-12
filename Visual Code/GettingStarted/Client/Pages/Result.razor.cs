using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GettingStarted.Client.DAL;
using Microsoft.IdentityModel.Tokens;

namespace GettingStarted.Client.Pages;

public partial class Result
{
    [Inject]
    HttpClient httpClient { get; set; }
    [Inject]
    NavigationManager navManager { get; set; }
    
    
    
    private void NavigateToLogin()
    {
        navManager.NavigateTo("/Login");
    }
    

}