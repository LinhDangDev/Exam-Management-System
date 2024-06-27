using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IUserRepository
    {
        public IDataReader SelectOne(Guid userId);
        public IDataReader SelectByLoginName(string loginName);
    }
}
