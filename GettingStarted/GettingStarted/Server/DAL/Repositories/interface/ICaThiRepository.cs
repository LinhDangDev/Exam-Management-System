using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        public IDataReader SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi);
        public IDataReader SelectOne(int ma_ca_thi);
    }
}
