using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class CauHoiRepository : ICauHoiRepository
    {
        public IDataReader SelectOne(int ma_cau_hoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectOne");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectDapAn(int ma_cau_hoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectDapAn");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return sql.ExcuteReader();
        }
    }
}
