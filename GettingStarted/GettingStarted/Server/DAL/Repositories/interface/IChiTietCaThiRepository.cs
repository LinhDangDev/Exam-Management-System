using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietCaThiRepository
    {
        public IDataReader SelectOne(int chi_tiet_ca_thi);
        public IDataReader SelectBy_ma_sinh_vien(long ma_sinh_vien);
        public IDataReader SelectBy_ma_ca_thi(int ma_ca_thi);
        public IDataReader SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien);
        public bool UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau);
        public bool UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau);
        public bool CongGio(int ma_chi_tiet_ca_thi, int gio_cong_them, DateTime? thoi_diem_cong, string? ly_do_cong);
    }
}
