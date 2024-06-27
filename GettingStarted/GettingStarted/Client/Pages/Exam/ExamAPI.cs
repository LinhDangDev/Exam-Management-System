using GettingStarted.Shared.Models;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace GettingStarted.Client.Pages.Exam
{
    public partial class Exam
    {
        private async Task getDeThi(long? ma_de_hoan_vi)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.PostAsync($"api/Exam/GetDeThi?ma_de_thi_hoan_vi={ma_de_hoan_vi}", null);
            if (response != null && response.IsSuccessStatusCode && myData != null)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                customDeThis = myData.customDeThis = JsonSerializer.Deserialize<List<CustomDeThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        private async Task getListDapAn(long? ma_de_hoan_vi)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.PostAsync($"api/Result/GetListDapAn?ma_de_thi_hoan_vi={ma_de_hoan_vi}", null);
            if (response != null && response.IsSuccessStatusCode && myData != null)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                listDapAnThucTe = myData.listDapAnGoc = JsonSerializer.Deserialize<List<int>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        // insert các các dòng dữ liệu chiTietBaiThi, và lấy dữ liệu của chiTietbaiThi
        private async Task InsertChiTietBaiThi()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null && chiTietCaThi != null && myData != null && myData.chiTietCaThi != null)
                response = await httpClient.PostAsync($"api/Exam/InsertChiTietBaiThi?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}&ma_de_thi_hoan_vi={myData.chiTietCaThi.MaDeThi}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietBaiThis = JsonSerializer.Deserialize<List<ChiTietBaiThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if (chiTietBaiThis != null && chiTietCaThi != null)
            {
                foreach (var item in chiTietBaiThis)
                    item.MaChiTietCaThiNavigation = chiTietCaThi;
            }
        }
        private async Task UpdateChiTietBaiThi()
        {
            var jsonString = JsonSerializer.Serialize(chiTietBaiThis);
            if (httpClient != null)
                await httpClient.PostAsync("api/Exam/UpdateChiTietBaiThi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private async Task<int> getSoLanNghe(int ma_chi_tiet_ca_thi, string filename)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.PostAsync($"api/Exam/AudioListendCount?ma_chi_tiet_ca_thi={ma_chi_tiet_ca_thi}&filename={filename}", null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return 0;
        }
    }
}
