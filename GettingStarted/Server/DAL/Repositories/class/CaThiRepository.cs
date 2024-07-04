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
        public IDataReader ca_thi_GetAll()
        {
            DatabaseReader sql = new DatabaseReader("ca_thi_GetAll");
            return sql.ExcuteReader();
        }
        public void ca_thi_Activate(int ma_ca_thi, bool IsActivated)
        {
            DatabaseReader sql = new DatabaseReader("ca_thi_Activate");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@IsActivated", SqlDbType.Bit, IsActivated);
            sql.ExcuteNonQuery();
        }
        public void ca_thi_Ketthuc(int ma_ca_thi)
        {
            DatabaseReader sql = new DatabaseReader("ca_thi_Ketthuc");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.ExcuteNonQuery();
        }
    }
}
