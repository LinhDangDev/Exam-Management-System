using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IMonHocRepository
    {
        public IDataReader SelectOne(int ma_mon_hoc);
    }
}
