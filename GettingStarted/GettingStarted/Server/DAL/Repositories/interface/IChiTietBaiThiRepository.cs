using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietBaiThiRepository
    {
        public bool Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, DateTime NgayTao, int ThuTu);
        public bool Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua);
        public IDataReader SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi);
    }
}
