using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietCaThiRepository
    {
        public IDataReader SelectBy_ma_sinh_vien(long ma_sinh_vien);
        public IDataReader SelectBy_ma_ca_thi(int ma_ca_thi);
        public IDataReader SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien);
    }
}
