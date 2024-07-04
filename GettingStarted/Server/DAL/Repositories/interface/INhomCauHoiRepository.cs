using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface INhomCauHoiRepository
    {
        public IDataReader SelectBy_MaDeThi(int ma_de_thi);
        public IDataReader SelectOne(int ma_nhom);
    }
}
