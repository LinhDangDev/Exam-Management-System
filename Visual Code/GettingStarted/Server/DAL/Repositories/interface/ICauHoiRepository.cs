using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        public IDataReader GetCauHoi(int maCauHoi);
    }
}
