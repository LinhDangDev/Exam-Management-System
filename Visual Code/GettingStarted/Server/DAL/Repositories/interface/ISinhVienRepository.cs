using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ISinhVienRepository
    {
        public int Insert(string ho_va_ten_lot, string ten_sinh_vien, int gioi_tinh, DateTime ngay_sinh, int ma_lop,
            string dia_chi, string email, string dien_thoai, string ma_so_sinh_vien, Guid student_id);
        public bool Update(long ma_sinh_vien, string ho_va_ten_lot, string ten_sinh_vien, int gioi_tinh,
            DateTime ngay_sinh, int ma_lop, string dia_chi, string email, string dien_thoai, string ma_so_sinh_vien);
        public bool Remove(long ma_sinh_vien);
        // lấy thông tin của 1 sinh viên từ mã sinh viên
        public IDataReader GetSinhVien(long ma_sinh_vien);
        // lấy mã sinh viên từ mã số sinh viên hoặc sử dụng để check xem SV có tồn tại hay không
        public object GetMaSV_FormMSSV(string ma_so_sinh_vien);
        // lấy hết thông tin của tất cả sinh viên
        public IDataReader GetAllSinhVien();
        // cập nhật thông tin sinh viên vào hệ thống gần đây nhất
        public IDataReader LogIn(long ma_sinh_vien, DateTime last_log_in);
    }
}
