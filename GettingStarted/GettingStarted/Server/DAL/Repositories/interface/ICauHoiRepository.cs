using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        public IDataReader SelectOne(int ma_cau_hoi);
        public IDataReader SelectDapAn(int ma_cau_hoi);
    }
}
