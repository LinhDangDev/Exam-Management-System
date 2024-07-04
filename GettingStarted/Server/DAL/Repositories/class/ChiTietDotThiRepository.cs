using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietDotThiResposity : IChiTietDotThiResposity
    {
        public IDataReader SelectBy_MaDotThi(int ma_dot_thi)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_dot_thi_SelectBy_ma_dot_thi");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            return sql.ExcuteReader();
        }
        public IDataReader SelectOne(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_dot_thi_SelectOne");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return sql.ExcuteReader();
        }
    }
}
