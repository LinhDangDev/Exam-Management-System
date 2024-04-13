using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class NhomCauHoiRepository : INhomCauHoiRepository
    {
        public IDataReader SelectBy_MaDeThi(int ma_de_thi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_NhomCauHoi_SelectBy_MaDeThi");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectOne(int ma_nhom)
        {
            DatabaseReader sql = new DatabaseReader("tbl_NhomCauHoi_SelectOne");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return sql.ExcuteReader();
        }
    }
}
