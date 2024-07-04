using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IAudioListenedRepository
    {
        public IDataReader SelectOne(int ma_chi_tiet_ca_thi, string filename);
    }
}
