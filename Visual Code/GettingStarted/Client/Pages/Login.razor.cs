using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GettingStarted.Client.DAL;
using Microsoft.IdentityModel.Tokens;
namespace GettingStarted.Client.Pages
{

    public partial class Login
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        ApplicationDataService myData { get; set; }
        private string ma_so_sinh_vien = "";
        private string password = "";

        private async Task Verify()
        {
            long ma_sinh_vien = -1;
            if (ma_so_sinh_vien == password)
            {
                var jsonString = JsonSerializer.Serialize(ma_so_sinh_vien);

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var response = await httpClient.PostAsync("api/Login/Verify", new StringContent(jsonString, Encoding.UTF8, "application/json"));

                // Kiểm tra xem yêu cầu có thành công không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await response.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị boolean
                    ma_sinh_vien = JsonSerializer.Deserialize<long>(resultString);

                    //if (ma_sinh_vien != -1)
                    //{
                    //    ma_so_sinh_vien = "successful";
                    //}
                    //else
                    //{
                    //    ma_so_sinh_vien = "NotFound";
                    //}
                }
            }
            // lưu dữ liệu cho toàn cục, các razor có thể xài biến này
            myData.ma_so_sinh_vien = ma_so_sinh_vien;
            myData.ma_sinh_vien = ma_sinh_vien;
        }
        private void NavigateToIndex()
        {
            navManager.NavigateTo("/index");
        }
        private void NavigateToExam()
        {
            navManager.NavigateTo("/exam");
        }
    }
}
