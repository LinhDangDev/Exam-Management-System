using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class CauHoiRepository : ICauHoiRepository
    {
        public IDataReader GetCauHoi(int maCauHoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectOne");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, maCauHoi);
            return sql.ExcuteReader();
        }
    }
}
