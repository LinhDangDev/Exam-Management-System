using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GettingStarted.Client.DAL;
using Microsoft.IdentityModel.Tokens;
namespace GettingStarted.Client.Pages
{

    public partial class SignIn
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        ApplicationDataService myData { get; set; }
        private string mssv = "";
        private string pass = "";
        private string icon = "images/exam/eye-closed-svgrepo-com-1.png";
        private string type = "password";
        private bool flag = false;


        //private async Task PassChecked()
        //{
        //    if(!flag)
        //    {
        //        flag = true;
        //        icon = "images/exam/eye-open-svgrepo-com.png";
        //        type = "text";
        //    }
        //    else
        //    {
        //        flag = false;
        //        icon = "images/exam/eye-closed-svgrepo-com-1.png";
        //        type = "password";
        //    }
        //}

        private async Task VerifyPass()
        {
            long ma_sinh_vien = -1;
            if (mssv == pass)
            {
                var jsonString = JsonSerializer.Serialize(mssv);

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var response = await httpClient.PostAsync("api/SignIn/VerifyPass", new StringContent(jsonString, Encoding.UTF8, "application/json"));

                // Kiểm tra xem yêu cầu có thành công không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await response.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị boolean
                    ma_sinh_vien = JsonSerializer.Deserialize<long>(resultString);

                    if (ma_sinh_vien != -1)
                    {
                        mssv = "successful";
                    }
                    else
                    {
                        mssv = "NotFound";
                    }
                }
            }
            // lưu dữ liệu cho toàn cục, các razor có thể xài biến này
            myData.ma_so_sinh_vien = mssv;
            myData.ma_sinh_vien = ma_sinh_vien;
        }
        
    }
}
