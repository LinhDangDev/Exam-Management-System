using GettingStarted.Client.DAL;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;

namespace GettingStarted.Client.Pages
{
    public partial class Exam
    {
        [Inject]
        HttpClient httpClient { get; set; }
        [Inject]
        ApplicationDataService myData { get; set; }
        private SinhVien? sv { get; set; }
        protected override async Task OnInitializedAsync()
        {
            sv = new SinhVien();
            await getThongTinSVAsync();
            await base.OnInitializedAsync();
        }
        private async Task getThongTinSVAsync()
        {
            var jsonString = JsonSerializer.Serialize(myData.ma_sinh_vien);

            // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
            var response = await httpClient.PostAsync("api/Exam/GetThongTinSV", new StringContent(jsonString, Encoding.UTF8, "application/json"));

            // Kiểm tra xem yêu cầu có thành công không
            if (response.IsSuccessStatusCode)
            {
                // Đọc kết quả từ phản hồi
                var resultString = await response.Content.ReadAsStringAsync();

                // Chuyển đổi kết quả từ chuỗi JSON thành giá trị boolean
                sv = JsonSerializer.Deserialize<SinhVien>(resultString);
            }
        }
    }
}
