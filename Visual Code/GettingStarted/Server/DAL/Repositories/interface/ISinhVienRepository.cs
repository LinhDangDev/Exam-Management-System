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
        public IDataReader GetSinhVien(long ma_sinh_vien);
    }
}
