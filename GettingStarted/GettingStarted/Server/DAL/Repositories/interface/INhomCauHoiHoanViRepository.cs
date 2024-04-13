using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface INhomCauHoiHoanViRepository
    {
        public IDataReader SelectOne(long ma_de_hoan_vi, int ma_nhom);
    }
}
