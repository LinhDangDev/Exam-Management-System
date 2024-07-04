using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class CauTraLoiRepository : ICauTraLoiRepository
    {
        public IDataReader SelectOne(int ma_cau_tra_loi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectOne");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauTraLoi_SelectBy_MaCauHoi");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return sql.ExcuteReader();
        }
    }
}
