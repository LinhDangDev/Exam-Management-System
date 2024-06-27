using GettingStarted.Shared.Models;
using Microsoft.JSInterop;

namespace GettingStarted.Client.Pages.Exam
{
    public partial class Exam
    {
        [JSInvokable] // Đánh dấu hàm để có thể gọi từ JavaScript
        public static Task<int> GetDapAnFromJavaScript(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        {
            // Xử lý giá trị được truyền từ JavaScript
            if (listDapAn != null)
                listDapAn.Add(ma_cau_tra_loi);
            
            ChiTietBaiThi? chiTietBaiThi = chiTietBaiThis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);

            if (chiTietBaiThi != null && listDapAnThucTe != null)
            {
                chiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
                chiTietBaiThi.KetQua = listDapAnThucTe.Contains((int)chiTietBaiThi.CauTraLoi) ? true : false;
            }
            return Task.FromResult<int>(ma_cau_tra_loi);
        }
    }
}
