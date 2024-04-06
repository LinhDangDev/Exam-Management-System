using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class CauTraLoiRepository : ICauTraLoiRepository
    {
        public IDataReader GetBy_MaCauHoi(int maCauHoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectOne");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, maCauHoi);
            return sql.ExcuteReader();
        }
    }
}
