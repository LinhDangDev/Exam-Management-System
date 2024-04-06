using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietDeThiHoanViRepository : IChiTietDeThiHoanViRepository
    {
        public IDataReader GetBy_MaDeHV(long maDeHV, int maNhom, int maChiTietCaThi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v3");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, maDeHV);
            sql.SqlParams("@MaNhom", SqlDbType.Int, maNhom);
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, maChiTietCaThi);
            return sql.ExcuteReader();
        }
        public IDataReader GetBy_MaDeHv(long maDeHV)
        {
            DatabaseReader sql = new DatabaseReader("tbl_ChiTietDeThiHoanVi_SelectBy_MaDeHV");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, maDeHV);
            return sql.ExcuteReader();
        }
    }
}
