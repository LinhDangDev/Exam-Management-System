using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class ChiTietBaiThiRepository : IChiTietBaiThiRepository
    {
        public bool Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, DateTime NgayTao, int ThuTu)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_bai_thi_Insert");
            sql.SqlParams("@ma_chi_tiet_ca_thi", System.Data.SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeHV", System.Data.SqlDbType.BigInt, MaDeHV);
            sql.SqlParams("@MaNhom", System.Data.SqlDbType.Int, MaNhom);
            sql.SqlParams("@MaCauHoi", System.Data.SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@NgayTao", System.Data.SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@ThuTu", System.Data.SqlDbType.Int, ThuTu);
            return sql.ExcuteNonQuery() != -1;
        }
        public bool Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_bai_thi_Update");
            sql.SqlParams("@MaChiTietBaiThi", System.Data.SqlDbType.BigInt, MaChiTietBaiThi);
            sql.SqlParams("@CauTraLoi", System.Data.SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", System.Data.SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", System.Data.SqlDbType.Bit, KetQua);
            return sql.ExcuteNonQuery() != 0;
        }
        public IDataReader SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            return sql.ExcuteReader();
        }
    }
}
