using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class DotThiRepository : IDotThiRepository
    {
        public IDataReader GetAll()
        {
            DatabaseReader sql = new DatabaseReader("dot_thi_GetAll");
            return sql.ExcuteReader();
        }
        public IDataReader SelectOne(int ma_dot_thi)
        {
            DatabaseReader sql = new DatabaseReader("dot_thi_SelectOne");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return sql.ExcuteReader();
        }
    }
}
