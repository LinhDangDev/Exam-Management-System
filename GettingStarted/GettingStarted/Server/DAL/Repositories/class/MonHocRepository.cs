using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class MonHocRepository : IMonHocRepository
    {
        public IDataReader SelectOne(int ma_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_SelectOne");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return sql.ExcuteReader();
        }
    }
}
