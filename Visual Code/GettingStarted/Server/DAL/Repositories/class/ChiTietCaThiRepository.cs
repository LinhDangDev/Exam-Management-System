using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietCaThiRepository : IChiTietCaThiRepository
    {
        public IDataReader GetBy_MaSinhVien(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_SelectBy_ma_sinh_vien");
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return sql.ExcuteReader();
        }
    }
}
