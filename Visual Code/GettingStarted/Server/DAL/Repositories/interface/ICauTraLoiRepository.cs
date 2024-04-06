using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICauTraLoiRepository
    {
        public IDataReader GetBy_MaCauHoi(int maCauHoi);
    }
}
