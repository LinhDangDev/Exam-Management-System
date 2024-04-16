using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietCaThiRepository : IChiTietCaThiRepository
    {
        public IDataReader SelectOne(int chi_tiet_ca_thi)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectOne");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, chi_tiet_ca_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectBy_ma_sinh_vien");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectBy_ma_ca_thi");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectBy_MaCaThi_MaSinhVien");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return sql.ExcuteReader();
        }
        public bool UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_UpdateBatDau");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            return sql.ExcuteNonQuery() != 0; // check xem có sự thay đổi không
        }
        public bool UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_UpdateKetThuc");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc);
            sql.SqlParams("@diem", SqlDbType.Float, diem);
            sql.SqlParams("@so_cau_dung", SqlDbType.Int, so_cau_dung);
            sql.SqlParams("@tong_so_cau", SqlDbType.Int, tong_so_cau);
            return sql.ExcuteNonQuery() != 0;
        }

    }
}
