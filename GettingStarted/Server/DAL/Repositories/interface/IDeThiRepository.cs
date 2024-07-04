using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        public IDataReader SelectOne(int ma_de_thi);
    }
}
