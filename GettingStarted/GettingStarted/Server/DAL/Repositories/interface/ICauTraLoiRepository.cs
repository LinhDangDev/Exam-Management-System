using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICauTraLoiRepository
    {
        public IDataReader SelectOne(int maCauHoi);
        public IDataReader SelectBy_MaCauHoi(int ma_cau_hoi);
    }
}
