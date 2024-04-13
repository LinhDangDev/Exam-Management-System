using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IDeThiHoanViRepository
    {
        public IDataReader SelectOne(long ma_de_hoan_vi);
    }
}
