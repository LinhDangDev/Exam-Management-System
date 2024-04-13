using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        public IDataReader SelectOne(int maCauHoi);
    }
}
