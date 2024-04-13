using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietCaThiRepository : IChiTietCaThiRepository
    {
        public IDataReader SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectBy_ma_sinh_vien");
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
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


    }
}
