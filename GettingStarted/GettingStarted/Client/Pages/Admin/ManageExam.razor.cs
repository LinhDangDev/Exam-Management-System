using GettingStarted.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GettingStarted.Client.Pages.Admin.DAL;
using GettingStarted.Shared.Models;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace GettingStarted.Client.Pages.Admin
{
    public partial class ManageExam
    {
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        AdminDataService? myData { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        private string? input_maCaThi { get; set; }
        private DateTime? input_Date { get; set; }
        private List<CaThi>? caThis { get; set; }
        private List<CaThi>? displayCaThis { get; set; }
        private bool showMessageBox { get; set; }
        private CaThi? showCaThiMessageBox { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Start();
            await base.OnInitializedAsync();
        }

        private async Task getAllCaThi()
        {
            if (httpClient != null)
                caThis = await httpClient.GetFromJsonAsync<List<CaThi>>("api/Admin/getAllCaThi");
            if (caThis != null && myData != null && displayCaThis != null)
                myData.caThis = displayCaThis = caThis.ToList();
        }

        private void onChangeDate(ChangeEventArgs e)
        {
            DateTime dateTime = new DateTime();
            if(e.Value != null)
            {
                DateTime.TryParse(e.Value.ToString(), out dateTime);
                UpdateDisplayCaThi(dateTime);
            }
            StateHasChanged();
        }
        private void onChangeMaCaThi(ChangeEventArgs e)
        {
            int ma_ca_thi = -1;
            if (e.Value != null)
            {
                if (e.Value.ToString() == "" && caThis != null)
                    displayCaThis = caThis.ToList();
                else
                {
                    int.TryParse(e.Value.ToString(), out ma_ca_thi);
                    UpdateDisplayCaThi(ma_ca_thi);
                }
            }
            StateHasChanged();
        }
        private void UpdateDisplayCaThi(DateTime dateTime)
        {
            if(displayCaThis != null && caThis != null)
            {
                displayCaThis.Clear();
                var item = caThis.Where(p => p.ThoiGianBatDau.Date == dateTime.Date).ToList();
                displayCaThis.AddRange(item);
            }
        }
        //Overloading
        private void UpdateDisplayCaThi(int ma_ca_thi)
        {
            if (displayCaThis != null && caThis != null)
            {
                displayCaThis.Clear();
                var item = caThis.Where(p => p.MaCaThi == ma_ca_thi).ToList();
                displayCaThis.AddRange(item);
            }
        }
        private void onClickReset()
        {
            input_Date = null;
            input_maCaThi = "";
            if(caThis != null)
                displayCaThis = caThis.ToList();
            StateHasChanged();
        }
        private void onClicCaThiChuaKichHoat()
        {
            if(caThis != null && displayCaThis != null)
            {
                displayCaThis = displayCaThis.Where(p => p.IsActivated == false).ToList();
            }
            StateHasChanged();
        }

        private async Task Start()
        {
            myData = new AdminDataService();
            caThis = new List<CaThi>();
            displayCaThis = new List<CaThi>();
            showMessageBox = false;
            showCaThiMessageBox = new CaThi();
            await getAllCaThi();
        }
        private void onClickShowMessageBox(CaThi caThi)
        {
            showMessageBox = true;
            showCaThiMessageBox = caThi;
            StateHasChanged();
        }
        private async Task UpdateTinhTrangCaThi(bool isActived)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null && showCaThiMessageBox != null)
                response = await httpClient.PostAsync($"api/Admin/UpdateTinhTrangCaThi?ma_ca_thi={showCaThiMessageBox.MaCaThi}&isActived={isActived}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                caThis = JsonSerializer.Deserialize<List<CaThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (caThis != null && myData != null && displayCaThis != null)
                    myData.caThis = displayCaThis = caThis.ToList();
            }
        }
        private async Task onClickKichHoatCaThi()
        {
            await UpdateTinhTrangCaThi(true);
            StateHasChanged();
        }
        private async Task onClickHuyKichHoatCaThi()
        {
            await UpdateTinhTrangCaThi(false);
            StateHasChanged();
        }
        private async Task onClickKetThucCaThi()
        {
            if (httpClient != null && showCaThiMessageBox != null)
                await httpClient.PostAsync($"api/Admin/DungCaThi?ma_ca_thi={showCaThiMessageBox.MaCaThi}", null);
        }
        private void onClickThoatMessageBox()
        {
            showMessageBox = false;
            StateHasChanged();
        }
    }
}
