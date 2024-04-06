using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietCaThiRepository
    {
        public IDataReader GetBy_MaSinhVien(long ma_sinh_vien);
    }
}
