using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IDotThiRepository
    {
        public IDataReader GetAll();
    }
}
