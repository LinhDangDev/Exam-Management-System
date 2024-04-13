using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class CaThiRepository : ICaThiRepository
    {
        public IDataReader SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new DatabaseReader("ca_thi_SelectBy_ma_chi_tiet_dot_thi");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectOne(int ma_ca_thi)
        {
            DatabaseReader sql = new DatabaseReader("ca_thi_SelectOne");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return sql.ExcuteReader();
        }
    }
}
