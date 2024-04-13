using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class NhomCauHoiHoanViRepository : INhomCauHoiHoanViRepository
    {
        public IDataReader SelectOne (long ma_de_hoan_vi, int ma_nhom)
        {
            DatabaseReader sql = new DatabaseReader("tbl_NhomCauHoiHoanVi_SelectOne");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return sql.ExcuteReader();
        }
    }
}
